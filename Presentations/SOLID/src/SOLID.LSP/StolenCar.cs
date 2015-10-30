using System.Drawing;

namespace SOLID.LSP
{
    public class StolenCar : Car
    {
        private bool ignitionWiresStripped;

        public StolenCar(Color color)
            : base(color)
        {
        }

        public void StripIgnitionWires()
        {
            ignitionWiresStripped = true;
        }

        public override void StartEngine()
        {
            if (ignitionWiresStripped == false)
                return;

            base.StartEngine();
        }
    }
}
