using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FilesDelete
{
    public class ServerControl : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            Deleter.Init();
            Deleter.SeachFile();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            hostControl.Stop();
            return true;
        }
    }
}
