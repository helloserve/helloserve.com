window.Init = function(trafficlightCallback, intersectionCallback)
{
    //get the canvas and related stuff
    var canvas = document.getElementById("board");
    var context = canvas.getContext("2d");

    //make a new game
    intersection = new Intersection(canvas, context);

    //load it up and launch
    intersection.Load(trafficlightCallback, intersectionCallback);
    intersection.MainLoop((new Date()).getTime());
}

window.requestAnimFrame = (function (callback) {
    return window.requestAnimationFrame ||
        window.webkitRequestAnimationFrame ||
        window.mozRequestAnimationFrame ||
        window.oRequestAnimationFrame ||
        window.msRequestAnimationFrame ||
        function (callback) {
            window.setTimeout(callback, 1000 / 60);
        };
})();

/*
main object
*/
var Intersection = function (cvs, ctx) {
    this.canvas = cvs;
    this.context = ctx;

    this.lastFrameTime = (new Date()).getTime();
    this.totalFrameTime = 0;
    this.totalFrames = 0;
    this.fps = 0;

    this.IntersectionCallback = null;

    this.parts = [];
    this.cars = [];

    this.spawnTime = 0;

    /*
    Load method which is called only once
    */
    this.Load = function (trafficlightCallback, intersectionCallback) {

        this.IntersectionCallback = intersectionCallback;

        var trafficLight = new TrafficLight(new Vector(this.canvas.width / 2 + 100 + 25, this.canvas.height / 2 - 100 - 25), Math.PI, TrafficLightState.Red, trafficlightCallback);
        var part = new IntersectionPart(IntersectionDirection.North,
                                        new Rect(this.canvas.width / 2 + 5, this.canvas.height / 2 - 100 - 30, 70, 10),
                                        [new Vector(this.canvas.width / 2 + 22, 20), new Vector(this.canvas.width / 2 + 58, 20)],
                                        trafficLight);
        this.parts.push(part);

        trafficLight = new TrafficLight(new Vector(this.canvas.width / 2 + 100 + 25, this.canvas.height / 2 + 100 + 25), Math.PI * 1.5, TrafficLightState.Green, trafficlightCallback);
        part = new IntersectionPart(IntersectionDirection.East,
                                    new Rect(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 + 5, 10, 70),
                                    [new Vector(this.canvas.width - 20, this.canvas.height / 2 + 22), new Vector(this.canvas.width - 20, this.canvas.height / 2 + 58)],
                                    trafficLight);
        this.parts.push(part);

        trafficLight = new TrafficLight(new Vector(this.canvas.width / 2 - 100 - 25, this.canvas.height / 2 + 100 + 25), 0, TrafficLightState.Red, trafficlightCallback);
        part = new IntersectionPart(IntersectionDirection.South,
                                    new Rect(this.canvas.width / 2 - 75, this.canvas.height / 2 + 100 + 20, 70, 10),
                                    [new Vector(this.canvas.width / 2 - 22, this.canvas.height - 20), new Vector(this.canvas.width / 2 - 58, this.canvas.height - 20)],
                                    trafficLight);
        this.parts.push(part);

        trafficLight = new TrafficLight(new Vector(this.canvas.width / 2 - 100 - 25, this.canvas.height / 2 - 100 - 25), Math.PI * 0.5, TrafficLightState.Green, trafficlightCallback);
        part = new IntersectionPart(IntersectionDirection.West,
                                    new Rect(this.canvas.width / 2 - 100 - 30, this.canvas.height / 2 - 75, 10, 70),
                                    [new Vector(20, this.canvas.height / 2 - 22), new Vector(20, this.canvas.height / 2 - 58)],
                                    trafficLight);
        this.parts.push(part);

        var car = new Car(new Vector(250, 250), 0, new Normal(0, -1), Color.Random(), Math.random());
        this.cars.push(car);
        var car = new Car(new Vector(250, 290), 0, new Normal(0, -1), Color.Random(), Math.random());
        this.cars.push(car);
    };
    /*
    main program loop
    */
    this.MainLoop = function (time) {

        var elapsed = (time - this.lastFrameTime) / 1000;

        this.Update(elapsed);
        this.Draw(elapsed);

        this.lastFrameTime = time;
        this.totalFrameTime += elapsed;
        this.totalFrames++;

        if (this.totalFrameTime >= 1) {
            this.fps = this.totalFrames;
            this.totalFrames = 0;
            this.totalFrameTime -= 1;
        }

        requestAnimFrame(function () {

            var currDate = new Date();
            var currTime = currDate.getTime();

            intersection.MainLoop(currTime);
        });
    };
    /*
    The Update() where all processing, AI and other calculations are to be done
    */
    this.Update = function (elapsed) {
        for (var i = 0; i < this.parts.length; i++) {
            this.parts[i].Update(elapsed, this.cars);
        }

        if (this.IntersectionCallback != null)
            this.IntersectionCallback();

        //update all the cars
        for (var i = this.cars.length - 1; i >= 0; i--) {
            this.cars[i].Update(elapsed, this.cars);
            if ((this.cars[i].Position.X < -100) || (this.cars[i].Position.X > this.canvas.Width + 100) || (this.cars[i].Position.Y < -100) || (this.cars[i].Position.Y > this.canvas.Height + 100)) {
                this.cars.splice(i, 1);
            }
        }

        this.spawnTime += elapsed;

        //return;

        //spawn a new car
        if (this.cars.length < 100 && this.spawnTime > 0.25) {
            //pick an intersection part
            var intersection = this.parts[Math.round(Math.random() * (this.parts.length - 1))];
            var car = intersection.SpawnCar();
            if (car != null) {
                this.cars.push(car);
                this.spawnTime = 0;
            }
        }
    };
    /*
    The main Draw() method where all other objects are drawn from
    */
    this.Draw = function (elapsed) {
        //have to reset the transformation every time
        this.context.setTransform(1, 0, 0, 1, 0, 0);

        this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);

        this.context.beginPath();
        this.context.moveTo(5, 5);
        this.context.lineTo(5, this.canvas.height - 5);
        this.context.lineTo(this.canvas.width - 5, this.canvas.height - 5);
        this.context.lineTo(this.canvas.width - 5, 5);
        this.context.lineTo(5, 5);
        this.context.strokeStyle = '#000000';
        this.context.stroke();

        //have to reset the transformation every time
        this.context.setTransform(1, 0, 0, 1, 0, 0);

        this.context.font = '10pts Calibri';
        this.context.fillStyle = '#8080FF';
        this.context.fillText(this.fps, 20, 20);
        this.context.fillText(this.cars.length, 20, 30);

        //have to reset the transformation every time
        this.context.setTransform(1, 0, 0, 1, 0, 0);
        //draw the intersection
        this.context.fillStyle = '#707070';
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 100, 0, 200, this.canvas.height);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(0, this.canvas.height / 2 - 100, this.canvas.width, 200);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 100 - 20, this.canvas.height / 2 - 100 - 20, 20, 20);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 100 - 20, this.canvas.height / 2 + 100 + 20, 20, -20);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 - 100 - 20, -20, 20);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 + 100 + 20, -20, -20);
        this.context.fill();

        //rounded corners for the intersection
        this.context.fillStyle = '#FFFFFF';
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 - 100 - 20, this.canvas.height / 2 - 100 - 20, 20, 0, 90, false);
        this.context.fill();
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 - 100 - 20, this.canvas.height / 2 + 100 + 20, 20, 0, 90, false);
        this.context.fill();
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 - 100 - 20, 20, 0, 90, false);
        this.context.fill();
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 + 100 + 20, 20, 0, 90, false);
        this.context.fill();

        //the yellow lines
        this.context.fillStyle = '#FFDD00';
        this.context.strokeStyle = '#FFDD00';
        this.context.lineWidth = 4;
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 100 + 16, 0, 4, this.canvas.height / 2 - 100 - 20);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(0, this.canvas.height / 2 - 100 + 16, this.canvas.width / 2 - 100 - 20, 4);
        this.context.fill();
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 - 100 - 20, this.canvas.height / 2 - 100 - 20, 38, 0, Math.PI / 2, false);
        this.context.stroke();

        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 - 16, 0, -4, this.canvas.height / 2 - 100 - 20);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 - 100 + 16, this.canvas.width / 2 - 100 - 20, 4);
        this.context.fill();
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 - 100 - 20, 38, Math.PI / 2, Math.PI, false);
        this.context.stroke();

        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 100 + 16, this.canvas.height / 2 + 100 + 20, 4, this.canvas.height / 2 - 100 - 20);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(0, this.canvas.height / 2 + 100 - 16, this.canvas.width / 2 - 100 - 20, -4);
        this.context.fill();
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 - 100 - 20, this.canvas.height / 2 + 100 + 20, 38, Math.PI * 1.5, 0, false);
        this.context.stroke();

        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 - 16, this.canvas.height / 2 + 100 + 20, -4, this.canvas.height / 2 - 100 - 20);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 + 100 - 16, this.canvas.width / 2 - 100 - 20, -4);
        this.context.fill();
        this.context.beginPath();
        this.context.arc(this.canvas.width / 2 + 100 + 20, this.canvas.height / 2 + 100 + 20, 38, Math.PI, Math.PI * 1.5, false);
        this.context.stroke();

        //draw the white lines
        this.context.fillStyle = '#EEEEEE';
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 2, 0, 4, this.canvas.height / 2 - 100 - 5);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 2, this.canvas.height / 2 + 100 + 5, 4, this.canvas.height / 2 - 100 - 5);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(0, this.canvas.height / 2 - 2, this.canvas.width / 2 - 100 - 5, 4);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 + 5, this.canvas.height / 2 - 2, this.canvas.width / 2 - 100 - 5, 4);
        this.context.fill();

        //draw the intersection lines
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2, this.canvas.height / 2 - 100 - 15, 80, 10);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2, this.canvas.height / 2 + 100 + 15, -80, -10);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 - 100 - 15, this.canvas.height / 2, 10, -80);
        this.context.fill();
        this.context.beginPath();
        this.context.rect(this.canvas.width / 2 + 100 + 15, this.canvas.height / 2, -10, 80);
        this.context.fill();

        for (var i = 0; i < this.parts.length; i++) {
            this.parts[i].Draw(this.context);
        }

        //draw all the cars
        for (var i = 0; i < this.cars.length; i++) {
            this.cars[i].Draw(this.context);
        }
    };
};
