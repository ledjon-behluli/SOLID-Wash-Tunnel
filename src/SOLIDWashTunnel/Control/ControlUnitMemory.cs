using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Control
{
    public interface IControlUnitMemory
    {
        T Get<T>(string key) where T : IControlUnitSignal;
        void Set(string key, IControlUnitSignal signal);
    }

    public class ControlUnitMemory : IControlUnitMemory
    {
        private Dictionary<string, IControlUnitSignal> _items;

        public ControlUnitMemory()
        {
            _items = new Dictionary<string, IControlUnitSignal>();
        }

        public T Get<T>(string key) where T : IControlUnitSignal
        {
            if (_items.TryGetValue(key, out IControlUnitSignal signal))
                return (T)signal;

            throw new ArgumentException($"An item with the key = '{key}', was not found in memory.");
        }

        public void Set(string key, IControlUnitSignal signal)
        {
            if (!_items.TryAdd(key, signal))
            {
                _items[key] = signal;
            }
        }
    }
}
