using System;
using System.Drawing;

namespace SOLID.LSP
{
    public class Prius : Car
    {
        private int acceleration;

        public Prius(Color color)
            : base(color)
        {
            acceleration = 0;
        }

        /// <summary>
        /// Sets the acceleration of the car depending on how hard the accelerator pedal is pressed.
        /// </summary>
        /// <param name="acceleration">This value should be between 0 and 100</param>
        public void SetAcceleration(int acceleration)
        {
            if (acceleration < 0 || acceleration > 100)
                throw new ArgumentException("Acceleration value should be between 0 and 100 percent.");

            this.acceleration = acceleration;

            if (acceleration >= 50)
                base.StartEngine();
            else
                base.StopEngine();
        }

        public override void StartEngine()
        {

        }

        public override void StopEngine()
        {

        }
    }
}
