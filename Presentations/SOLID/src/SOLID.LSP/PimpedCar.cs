using System.Drawing;

namespace SOLID.LSP
{
    public class PimpedCar : Car
    {
        private int temp;

        public PimpedCar(Color color, int temp = 0)
            : base(color)
        {
            this.temp = temp;

            SetColor(color);
        }

        public void SetTemprature(int temp)
        {
            this.temp = temp;
            SetColor(Color);
        }

        private void SetColor(Color color)
        {
            if (this.temp > 20)
                Color = color;
            else
                Color = Color.Black;
        }
    }
}
