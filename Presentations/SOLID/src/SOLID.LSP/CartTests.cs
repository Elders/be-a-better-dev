using System.Drawing;
using NUnit.Framework;

namespace SOLID.LSP
{
    [TestFixture]
    public class CartTests
    {
        [Test]
        public void Make_sure_car_can_start()
        {
            var car = new Car(Color.Red);
            //var car = new BrokenCar(Color.Red);
            //var car = new CrimeBossCar(Color.Black, true);
            //var car = new Prius(Color.Red);
            //var car = new StolenCar(Color.Red);

            try
            {
                car.StartEngine();
            }
            catch (OutOfFuelException)
            {
                Assert.Fail("Car had no gas.");
            }

            Assert.IsTrue(car.IsEngineRunning);
        }

        [Test]
        public void Make_sure_car_is_painted_correctly()
        {
            var car = new Car(Color.Red);
            //var car = new PimpedCar(Color.Red);

            Assert.AreEqual(Color.Red, car.Color);
        }
    }
}
