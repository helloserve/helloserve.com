window.onload = function () {
    window.Init(function () {
        
        //TODO : write your traffic light logic here

        if (this.ElapsedTime > 2)
            this.Switch();

    },

    function () {
    
        //TODO : write your intersection logic here
    
    });
};