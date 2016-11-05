using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace App1
{
    public class ApiResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
    public class ServiceManager
    {
        public async Task<ApiResult> upload(Stream image)
        {
            try
            {
                var data = ReadFully(image);
                var imageStream = new ByteArrayContent(data);
                imageStream.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = App.ImageName
                };
                MultipartFormDataContent form = new MultipartFormDataContent();
                form.Add(imageStream);

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");
                client.BaseAddress = new Uri("http://xamarinsample.xamarintr.com/");
                var result = await client.PostAsync("api/SaveImage", form);

                var response = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ApiResult>(response);
            }
            catch (Exception ex)
            {
                return new ApiResult
                {
                    Message = ex.Message,
                    Result = false
                };
            }
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}