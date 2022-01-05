using SOLIDWashTunnel.ClientFacing;

namespace SOLIDWashTunnel.Customers
{
    public abstract class Car : IVehicle
    {
        public int Dirtiness { get; private set; }
        public PaintFinishType FinishType { get; }

        public Car(int dirtiness, PaintFinishType finishType)
        {
            Dirtiness = dirtiness; 
            FinishType = finishType;
        }

        public void Accept(IWashAction action)
        {
            Dirtiness -= action.CleaningFactor;      // With each wash action we decrease the dirtiness a.k.a we make the vehicle cleaner.
        }
    }

    public class DirtyMetallicCar : Car
    {
        public DirtyMetallicCar()
            : base(10, PaintFinishType.Metallic)       // Represents a dirty car (since dirtiness is high) with a metallic finish.
        {

        }
    }

    public class CleanMetallicCar : Car
    {
        public CleanMetallicCar()
            : base(0, PaintFinishType.Metallic)      // Represents a clean car (since dirtiness is low) with a metallic finish.
        {

        }
    }

    public class DirtyMatteCar : Car
    {
        public DirtyMatteCar()
            : base(10, PaintFinishType.Matte)       // Represents a dirty car (since dirtiness is high) with a matte finish.
        {

        }
    }

    public class CleanMatteCar : Car
    {
        public CleanMatteCar()
            : base(0, PaintFinishType.Matte)      // Represents a clean car (since dirtiness is low) with a matte finish.
        {

        }
    }
}
