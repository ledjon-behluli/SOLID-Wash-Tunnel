﻿using SOLIDWashTunnel.WashPrograms.WashSteps;
using System.Collections.Generic;

namespace SOLIDWashTunnel.WashPrograms
{
    public interface IWashProgram
    {
        int Id { get; }
        string Name { get; }
        IEnumerable<IWashStep> GetWashSteps();
    }

    public class FastWashProgram : IWashProgram
    {
        public int Id => 1;
        public string Name => "Fast";

        public IEnumerable<IWashStep> GetWashSteps()
        {
            return null;
        }
    }

    public class EconomicWashProgram : IWashProgram
    {
        public int Id => 2;
        public string Name => "Economic";

        public IEnumerable<IWashStep> GetWashSteps()
        {
            return null;
        }
    }

    public class AllRounderWashProgram : IWashProgram
    {
        public int Id => 3;
        public string Name => "All rounder";

        public IEnumerable<IWashStep> GetWashSteps()
        {
            return null;
        }
    }

    public class CustomWashProgram : IWashProgram
    {
        public int Id => 4;
        public string Name => "Custom";

        public IEnumerable<IWashStep> GetWashSteps()
        {
            return null;
        }
    }
}
