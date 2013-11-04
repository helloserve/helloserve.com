var IntersectionDirection = {
    North : 0,
    East : 1,
    South : 2,
    West : 3    
};

var IntersectionPart = function (direction, trigger, spawnPoints, trafficLight) {
    this.Direction = direction;
    this.SpawnPoints = spawnPoints;
    this.Trigger = trigger;
    this.Triggered = true;
    this.TriggeredTime = 0;
    this.TrafficLight = trafficLight;

    this.flashTime = 0;
    this.flash = false;

    this.GetAngle = function () {
        if (this.Direction == IntersectionDirection.North)
            return Math.PI;
        if (this.Direction == IntersectionDirection.East)
            return Math.PI * 1.5;
        if (this.Direction == IntersectionDirection.South)
            return 0;
        if (this.Direction == IntersectionDirection.West)
            return Math.PI * 0.5;
    };

    this.GetDirection = function () {
        if (this.Direction == IntersectionDirection.North)
            return new Normal(0, 1);
        if (this.Direction == IntersectionDirection.East)
            return new Normal(-1, 0);
        if (this.Direction == IntersectionDirection.South)
            return new Normal(0, -1);
        if (this.Direction == IntersectionDirection.West)
            return new Normal(1, 0);
    };

    this.SpawnCar = function () {
        var count = this.SpawnPoints.length;
        var point = this.SpawnPoints[Math.round(Math.random(this.SpawnPoints.length - 1))];

        return new Car(point, this.GetAngle(), this.GetDirection(), Color.Random(), Math.random());
    };

    this.Update = function (elapsed, cars) {
        this.flashTime += elapsed;
        if (this.flashTime >= 0.5) {
            this.flash = !this.flash;
            this.flashTime = 0;
        }

        if (this.TrafficLight != null)
            this.TrafficLight.Update(elapsed);

        for (var i = 0; i < cars.length; i++) {
            
        }
    };

    this.Draw = function (context) {
        this.TrafficLight.Draw(context);

        context.setTransform(1, 0, 0, 1, 0, 0);

        //draw the trigger plate
        if (this.Trigger != null) {
            if (this.Triggered) {
                if (this.flash)
                    context.fillStyle = '#555555';
                else
                    context.fillStyle = '#707070';
                context.beginPath();
                context.rect(this.Trigger.X, this.Trigger.Y, this.Trigger.Width, this.Trigger.Height);
                context.fill();
            }

            context.strokeStyle = '#404040';
            context.lineWidth = 2;
            context.beginPath();
            context.rect(this.Trigger.X, this.Trigger.Y, this.Trigger.Width, this.Trigger.Height);
            context.stroke();
        }
    };
}

var TrafficLightState = {
    Red : "red",
    Orange : "orange",
    Green : "green"
};

var TrafficLight = function (pos, angle, state, callback) {
    this.ElapsedTime = 0;
    this.State = state;
    this.Callback = callback;
    this.Position = pos;
    this.Angle = angle;

    // Update method of the traffic light - execute's your code if it's been assigned
    this.Update = function (elapsed) {
        this.ElapsedTime += elapsed;

        if (this.Callback == null)
            return;

        this.Callback();
    };

    this.Switch = function () {
        if (this.State == TrafficLightState.Red)
            this.State = TrafficLightState.Green;
        else if (this.State == TrafficLightState.Orange)
            this.State = TrafficLightState.Red;
        else if (this.State == TrafficLightState.Green)
            this.State = TrafficLightState.Orange;

        this.ElapsedTime = 0;
    }

    //internal method to get the color
    this.getColor = function (value) {
        if (typeof (value) == "number") {
            if (value == 0)
                return '#800000';
            if (value == 1)
                return '#AA4000';
            if (value == 2)
                return '#008000';
        }
        else {
            if (value == TrafficLightState.Red)
                return '#FF0000';
            if (value == TrafficLightState.Orange)
                return '#FF9900';
            if (value == TrafficLightState.Green)
                return '#80FF80';
        }
    };
    // Draw method of the traffic light
    this.Draw = function (context) {
        //have to reset the transformation every time
        context.setTransform(1, 0, 0, 1, 0, 0);

        context.translate(this.Position.X, this.Position.Y);
        context.rotate(this.Angle);
        //draw the three circles
        var radius = 10;
        var i = 0;
        context.strokeStyle = '#404040';
        context.lineWidth = 5;
        for (i = 0; i < 3; i++) {
            context.beginPath();
            context.arc(0, (radius * 2 * i) + 5, radius, 0, 2 * Math.PI, false);
            context.stroke();
        }
        i = 0;
        for (i = 0; i < 3; i++) {
            context.beginPath();
            context.arc(0, (radius * 2 * i) + 5, radius, 0, 2 * Math.PI, false);
            context.fillStyle = this.getColor(i);
            context.fill();
        }

        //draw the light that's on
        if (this.State == TrafficLightState.Red)
            i = 0;
        if (this.State == TrafficLightState.Orange)
            i = 1;
        if (this.State == TrafficLightState.Green)
            i = 2;

        context.beginPath();
        context.arc(0, (radius * 2 * i) + 5, radius, 0, 2 * Math.PI, false);
        context.fillStyle = this.getColor(this.State);
        context.fill();
    };
};

var Car = function (pos, angle, dir, col, rnd) {
    this.Angle = angle;
    this.Position = pos;
    this.Direction = dir;
    this.Color = col;
    this.BoundingBox = new BoundingBox(0, 0, 0, 0);

    this.MaxSpeed = 10 + (5 * (rnd - 0.5));
    this.Speed = this.MaxSpeed;
    this.SpeedFactor = 8.0 + (4.0 * rnd);

    //the update method performed by each car
    this.Update = function (elapsed, cars) {
        //recalc the bounding box
        var min = new Vector(-15, -25);
        var max = new Vector(15, 20);
        var rot = new rotationMatrix(this.Angle);
        min = min.Transform(rot);
        min = min.Add(this.Position);
        max = max.Transform(rot);
        max = max.Add(this.Position);
        this.BoundingBox = new BoundingBox(Math.min(min.X, max.X), Math.min(min.Y, max.Y), Math.max(min.X, max.X), Math.max(min.Y, max.Y));

        //are there any cars ahead of me?
        var inFront = null;
        var inFrontDist = -1;

        for (var i = 0; i < cars.length; i++) {
            if (cars[i] == this)
                continue;

            var res = cars[i].Position.Subtract(this.Position);
            var dist = res.Length();
            var resN = res.Normal();
            var dot = resN.Dot(this.Direction);
            if (dist < 60 && dot > 0.9) {
                inFront = cars[i];
                inFrontDist = dist;
            }
        }

        if (inFront == null)
            inFrontDist = 60.0 + this.SpeedFactor;

        //adjust the speed
        var ratio = inFrontDist / this.SpeedFactor;
        this.Speed *= ratio / 6.0;

        if (this.Speed > this.MaxSpeed)
            this.Speed = this.MaxSpeed;
        if (this.Speed < 0.001)
            this.Speed = 0.001;

        if (this.Speed < 0.5 && inFront == null)
            this.Speed = 0.5;
        
        //move the car forward
        this.Position = this.Position.Add(this.Direction.Multiply(this.Speed * 0.5));
    };

    this.Draw = function (context) {
        //have to reset the transformation every time
        context.setTransform(1, 0, 0, 1, 0, 0);

        context.translate(this.Position.X, this.Position.Y);
        context.rotate(this.Angle);
        //        context.fillStyle = 'green';
        //        context.fillRect(-10, -15, 20, 30);
        //        context.fillStyle = '#000000';
        //        context.fillRect(-7, -10, 14, 5);

        context.beginPath();
        context.moveTo(-15, -20);
        context.quadraticCurveTo(0, -30, 15, -20);
        context.lineTo(13, 7);
        context.lineTo(15, 15);
        context.arcTo(15, 20, 10, 20, 10);
        context.lineTo(-10, 20);
        context.arcTo(-15, 20, -15, -15, 10);
        //context.quadraticCurveTo(0, 20, -10, 10);
        context.lineTo(-13, 7);
        context.lineTo(-15, -20);
        context.closePath();
        context.fillStyle = this.Color.Value();
        context.fill();

        context.beginPath();
        context.moveTo(-11, -9);
        context.quadraticCurveTo(0, -15, 11, -9);
        context.lineTo(11, 13);
        context.arcTo(11, 15, 9, 15, 7);
        context.lineTo(-9, 15);
        context.arcTo(-11, 15, -11, 13, 7);
        //context.lineTo(-6, 10);
        context.lineTo(-11, -9);
        context.closePath();
        context.fillStyle = '#333333';
        context.fill();

        context.beginPath();
        context.moveTo(-11, -9);
        context.quadraticCurveTo(0, -15, 11, -9);
        context.lineTo(9, -7);
        context.quadraticCurveTo(0, -9, -9, -7);
        context.closePath();
        context.fillStyle = '#777777';
        context.fill();

        //have to reset the transformation every time
        context.setTransform(1, 0, 0, 1, 0, 0);

        context.beginPath();
        context.moveTo(this.BoundingBox.Min.X, this.BoundingBox.Min.Y);
        context.lineTo(this.BoundingBox.Min.X, this.BoundingBox.Max.Y);
        context.lineTo(this.BoundingBox.Max.X, this.BoundingBox.Max.Y);
        context.lineTo(this.BoundingBox.Max.X, this.BoundingBox.Min.Y);
        context.lineTo(this.BoundingBox.Min.X, this.BoundingBox.Min.Y);
        context.strokeStyle = '#FF0000';
        context.lineWidth = 1;
        context.stroke();
    };
};

/*
A basic 2 dimensional vector
*/
var Vector = function (x, y) {
    this.X = x;
    this.Y = y;
    this.Length = function () {
        if (this.X == 0 && this.Y == 0)
            return 0;

        return Math.sqrt((this.X * this.X) + (this.Y * this.Y));
    };
    this.Dot = function (v) {
        return new Vector(v.X + this.X, v.Y + this.Y);
    };
    this.Transform = function (matrix) {
        var rx = matrix.m11 * this.X + matrix.m12 * this.Y;
        var ry = matrix.m21 * this.X + matrix.m22 * this.Y;
        return new Vector(rx, ry);
    };
    this.Add = function (v) {
        var rx = this.X + v.X;
        var ry = this.Y + v.Y;
        return new Vector(rx, ry);
    };
    this.Subtract = function (v) {
        var rx = this.X - v.X;
        var ry = this.Y - v.Y;
        return new Vector(rx, ry);
    };
    this.Multiply = function (scalar) {
        var rx = this.X * scalar;
        var ry = this.Y * scalar;
        return new Vector(rx, ry);
    };
    this.Normal = function () {
        if (this.X == 0 && this.Y == 0)
            return new Normal(1, 1);

        var rx = this.X / this.Length();
        var ry = this.Y / this.Length();
        return new Normal(rx, ry);
    };
};

/*
A special normal vector object (Z = 0)
*/
var Normal = function (x, y) {
    this.X = x;
    this.Y = y;
    this.Z = 0;
    this.Dot = function (n) {
        return (n.X * this.X) + (n.Y * this.Y);
    };
    this.Transform = function (matrix) {
        var rx = matrix.m11 * this.X + matrix.m12 * this.Y + matrix.m13 * this.Z;
        var ry = matrix.m21 * this.X + matrix.m22 * this.Y + matrix.m23 * this.Z;
        var rz = matrix.m31 * this.X + matrix.m32 * this.Y + matrix.m33 * this.Z;
        return new Normal(rx, ry, rz);
    };
    this.Add = function (n) {
        var rx = this.X + n.X;
        var ry = this.Y + n.Y;
        return new Vector(rx, ry, 0);
    };
    this.Multiply = function (scalar) {
        var rx = this.X * scalar;
        var ry = this.Y * scalar;
        return new Vector(rx, ry);
    };
};

/*
basic 2D rotation matrix
*/
var rotationMatrix = function (angle) {
    this.m11 = Math.cos(angle);
    this.m12 = Math.sin(angle) * -1.0;
    this.m13 = 0;

    this.m21 = Math.sin(angle);
    this.m22 = Math.cos(angle);
    this.m23 = 0;

    this.m31 = 0;
    this.m32 = 0;
    this.m33 = 1;
};

var BoundingBox = function (minX, minY, maxX, maxY) {
    this.Min = new Vector(minX, minY);
    this.Max = new Vector(maxX, maxY);

    this.Intersects = function (boundingBox) {
        return ((this.Max.X > boundingBox.Min.X && this.Max.Y > boundingBox.Min.Y) ||
                (boundingBox.Max.X > this.Min.X && boundingBox.Max.Y > this.Min.Y));
    };
}

/*
A rectangle object
*/
var Rect = function(x, y, width, height) {
    this.X = x;
    this.Y = y;
    this.Width = width;
    this.Height = height;
};


/*
A color holder
*/
var ColorVal = function(r, g, b) {
    this.R = r;
    this.G = g;
    this.B = b;
    this.Value = function() { return '#' + this.R.toString(16) + this.G.toString(16) + this.B.toString(16); };
};

var Color = {
    Random : function() { 
        var r = Math.round(Math.random() * 255);
        var g = Math.round(Math.random() * 255);
        var b = Math.round(Math.random() * 255);
        var avg = (r + g + b) / 3;
        if (avg < 128)
        {
            r = Math.min(r * 2, 255);
            g = Math.min(g * 2, 255);
            b = Math.min(b * 2, 255);
        }
        return new ColorVal(r, g, b); 
    }
};