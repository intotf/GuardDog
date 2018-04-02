using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSocket.Http;

namespace CameraApp
{
    public class IndexController : HttpController
    {
        [Route("/")]
        public ActionResult Index()
        {
            Response.Status = 301;
            Response.Headers.Add("Location ", "/index.html");
            return new EmptyResult();
        }
    }
}
