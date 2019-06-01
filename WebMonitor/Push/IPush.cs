using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMonitor.Push
{
    public interface IPush
    {
        Task SendAsync();
    }
}
