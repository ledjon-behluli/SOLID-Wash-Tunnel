using System.Collections.Generic;

namespace SOLIDWashTunnel.Control
{
    public interface IMemory
    {
        bool TryGet<T>(string key, out T signal) where T : ISignal;
        void SetOrOverride(string key, ISignal signal);
        void Flush();
    }

    public class RandomAccessMemory : IMemory
    {
        private Dictionary<string, ISignal> _items;

        public RandomAccessMemory()
        {
            _items = new Dictionary<string, ISignal>();
        }

        public bool TryGet<T>(string key, out T signal) where T : ISignal
        {
            bool success = _items.TryGetValue(key, out ISignal _signal);
            signal = success ? (T)_signal : default;

            return success;
        }

        public void SetOrOverride(string key, ISignal signal)
        {
            if (!_items.TryAdd(key, signal))
            {
                _items[key] = signal;
            }
        }

        public void Flush()
        {
            _items.Clear();
        }
    }
}
