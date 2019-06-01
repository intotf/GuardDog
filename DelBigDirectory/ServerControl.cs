using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace DelBigDirectory
{
    public class ServerControl : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            try
            {
                Deleter.Init();
                Deleter.DelAllFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            hostControl.Stop();
            return true;
        }
    }
}
