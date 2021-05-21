using SOLIDWashTunnel.Programs.Steps;
using SOLIDWashTunnel.Tunnel;

namespace SOLIDWashTunnel.Vehicles
{
    public interface IVehicle
    {
        int Cleanliness { get; }
        void ApplyWashStep(IWashStep step);
    }

    public interface ICustomer
    {
        IUserPanel UserPanel { get; set; }
    }

}
