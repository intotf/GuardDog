using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace CameraApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.Service<ListenerControl>();
                c.RunAsLocalSystem();
                c.SetServiceName("CameraDriveService");
                c.SetDisplayName("CameraDriveService");
                c.SetDescription("CameraDriveService");
            });
        }
    }
}
