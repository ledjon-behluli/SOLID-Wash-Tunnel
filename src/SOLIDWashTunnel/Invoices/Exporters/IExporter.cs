using System;
using System.Collections.Generic;
using System.Text;

namespace SOLIDWashTunnel.Invoices.Exporters
{
    public interface IExporter
    {
        void Export(Invoice invoice);
    }
}
