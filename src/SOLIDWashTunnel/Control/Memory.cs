﻿using System.Collections.Generic;

namespace SOLIDWashTunnel.Control
{
    public interface IMemory
    {
        bool TryGet<T>(string key, out T signal) where T : ISignal;
        void SetOrOverride(string key, ISignal signal);
    }

    public class Memory : IMemory
    {
        private Dictionary<string, ISignal> _items;

        public Memory()
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
    }
}