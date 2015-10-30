using System.Drawing;

namespace SOLID.LSP
{
    public class Car
    {
        private bool hasFuel = true;

        public Car(Color color)
        {
            Color = color;
        }

        public Color Color { get; protected set; }

        public bool IsEngineRunning { get; private set; }

        public virtual void StartEngine()
        {
            if (hasFuel)
                IsEngineRunning = true;
            else
                throw new OutOfFuelException();
        }

        public virtual void StopEngine()
        {
            IsEngineRunning = false;
        }
    }
}
