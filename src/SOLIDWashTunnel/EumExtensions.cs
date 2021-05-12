using System;
using System.ComponentModel;

namespace SOLIDWashTunnel.BuildingBlocks.Extensions
{
    public static class EumExtensions
    {
        public static string GetDescription<T>(this T @enum) where T : Enum
        {
            var field = @enum.GetType().GetField(@enum.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attributes != null && attributes.Length > 0 ?
                attributes[0].Description : @enum.ToString();
        }
    }
}
