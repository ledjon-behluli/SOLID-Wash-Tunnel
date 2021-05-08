using SOLIDWashTunnel.Vehicles;
using SOLIDWashTunnel.WashPrograms;

namespace SOLIDWashTunnel.Tunnels
{
    public interface IWashTunnel
    {
        void SelectProgram(IWashProgram program);
        void Wash(IVehicle vehicle);
    }
}
