using System;
using System.Collections.Generic;

namespace SOLIDWashTunnel.Programs.Steps
{

    public interface IWashStepFactory
    {
        IWashStep Create(WashStepType type);
    }

    public class WashStepFactory : IWashStepFactory
    {
        private readonly Lazy<IDictionary<WashStepType, Func<IWashStep>>> _washStepsMap;

        public WashStepFactory(IDictionary<WashStepType, Func<IWashStep>> washSteps)
        {
            _washStepsMap = new Lazy<IDictionary<WashStepType, Func<IWashStep>>>(washSteps);
        }

        public IWashStep Create(WashStepType type)
        {
            if (!_washStepsMap.Value.TryGetValue(type, out Func<IWashStep> _func))
            {
                throw new NotSupportedException($"Wash step type {type} is not supported!");
            }

            return _func.Invoke();
        }
    }
}
