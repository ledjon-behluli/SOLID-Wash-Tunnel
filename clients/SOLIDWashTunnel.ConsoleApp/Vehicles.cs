using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.ConsoleApp
{
    public abstract class Car : IVehicle
    {
        public int Dirtiness { get; private set; }

        public Car(int dirtiness)
        {
            Dirtiness = dirtiness; 
        }

        public void Accept(IWashAction action)
        {
            Dirtiness -= action.CleaningFactor;      // With each wash action we decrease the dirtiness a.k.a we make the vehicle cleaner.
        }
    }

    public class DirtyCar : Car
    {
        public DirtyCar()
            : base(10)       // Represents a dirty car, since dirtiness is high
        {

        }
    }

    public class CleanCar : Car
    {
        public CleanCar()
            : base(0)      // Represents a clean car, since dirtiness is low
        {

        }
    }
}
