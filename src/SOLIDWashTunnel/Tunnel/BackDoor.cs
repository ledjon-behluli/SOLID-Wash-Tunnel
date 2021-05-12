using System;

namespace SOLIDWashTunnel.Tunnel
{
    public interface IBackDoor
    {
        void Open();
    }

    public class BackDoor : IBackDoor
    {
        private bool isOpen;

        public void Open()
        {
            if (isOpen)
            {
                Console.WriteLine("Back door is already open, ignoring request.");
                return;
            }

            isOpen = true;
            Console.WriteLine("Back door has opended.");
        }
    }
}
