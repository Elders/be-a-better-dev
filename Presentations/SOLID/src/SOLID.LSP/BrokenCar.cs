using System;
using System.Drawing;

namespace SOLID.LSP
{
    public class BrokenCar : Car
    {
        public BrokenCar(Color color)
            : base(color)
        {
        }

        public override void StartEngine()
        {
            throw new NotImplementedException();
        }
    }
}
