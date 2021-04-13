using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace JQueryFileUpload.Web.Controllers
{
    public class VideoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var o = new { id = "1", value = "OK" };
            return Request.CreateResponse(HttpStatusCode.OK, o);
        }
        
        [HttpPost]
        public HttpResponseMessage Post()
        {
            var httpContext = HttpContext.Current;

            // Check for any uploaded file  
            if (httpContext.Request.Files.Count > 0)
            {
                //Loop through uploaded files  
                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpContext.Request.Files[i];
                    if (httpPostedFile != null && httpPostedFile.ContentLength > 0)
                    {
                        var folderPath = HostingEnvironment.MapPath($"~/App_Data/upload/video/{DateTime.Now:yyyyMMdd}");

                        // not exists create folder
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Construct file save path  
                        var fileSavePath = Path.Combine(folderPath, httpPostedFile.FileName);

                        // Save the uploaded file  
                        httpPostedFile.SaveAs(fileSavePath);
                    }
                }
            }

            // Return status code  
            return Request.CreateResponse(HttpStatusCode.Created, new { Status = "files is uploaded" });
        }
    }
}
