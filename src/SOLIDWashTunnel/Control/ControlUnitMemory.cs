using System.Collections.Generic;

namespace SOLIDWashTunnel.Control
{
    public interface IControlUnitMemory
    {
        bool TryGet<T>(string key, out T signal) where T : IControlUnitSignal;
        void SetOrOverride(string key, IControlUnitSignal signal);
    }

    public class ControlUnitMemory : IControlUnitMemory
    {
        private Dictionary<string, IControlUnitSignal> _items;

        public ControlUnitMemory()
        {
            _items = new Dictionary<string, IControlUnitSignal>();
        }

        public bool TryGet<T>(string key, out T signal) where T : IControlUnitSignal
        {
            bool success = _items.TryGetValue(key, out IControlUnitSignal _signal);
            signal = success ? (T)_signal : default;

            return success;
        }

        public void SetOrOverride(string key, IControlUnitSignal signal)
        {
            if (!_items.TryAdd(key, signal))
            {
                _items[key] = signal;
            }
        }
    }
}
