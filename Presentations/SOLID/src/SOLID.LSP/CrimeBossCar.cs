using System.Drawing;

namespace SOLID.LSP
{
    public class CrimeBossCar : Car
    {
        private readonly bool boobyTrapped;

        public CrimeBossCar(Color color, bool boobyTrap)
            : base(color)
        {
            this.boobyTrapped = boobyTrap;
        }

        public override void StartEngine()
        {
            if (boobyTrapped)
                throw new MetYourMakerException("Boom! You are dead!");

            base.StartEngine();
        }
    }
}
