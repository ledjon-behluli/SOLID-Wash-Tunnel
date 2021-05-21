using SOLIDWashTunnel.Programs.Steps;
using SOLIDWashTunnel.Vehicles;

namespace SOLIDWashTunnel.ConsoleApp
{
    public abstract class Car : IVehicle
    {
        public int Cleanliness { get; private set; }

        public Car(int cleanliness)
        {
            Cleanliness = cleanliness; 
        }

        public void ApplyWashStep(IWashStep step)
        {
            Cleanliness += step.CleanlinessFactor;      // With each step we increase the cleanliness a.k.a we make the vehicle cleaner.
        }
    }

    public class DirtyCar : Car
    {
        public DirtyCar()
            : base(0)       // Represents a dirty car, since cleanliness is low
        {

        }
    }

    public class CleanCar : Car
    {
        public CleanCar()
            : base(10)      // Represents a clean car, since cleanliness is high
        {

        }
    }
}
