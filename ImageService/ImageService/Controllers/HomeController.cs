using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageService.Controllers
{
    public class ApiResult
    {
        public bool Result { get; set; } = false;
        public string Message { get; set; }
    }
    public class HomeController : ApiController
    {
        [HttpPost]
        [Route("api/SaveImage")]
        public async Task<ApiResult> PostUserImage()
        {
            ApiResult result = new ApiResult();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1;

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            result.Message = "Please Upload image of type .jpg,.gif,.png.";
                        }
                        //else if (postedFile.ContentLength > MaxContentLength)
                        //{
                        //    var message = string.Format("Please Upload a file upto 1 mb.");
                        //    return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                        //}
                        else
                        {
                            var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            result.Result = true;
                            result.Message = "Image Updated Successfully.";
                        }
                    }
                }
                else
                {
                    result.Message = "Please Upload image.";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}