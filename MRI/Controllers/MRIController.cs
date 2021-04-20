using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MRI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MRIController : ControllerBase
    {
        private readonly ILogger<MRIController> _logger;

        public MRIController(ILogger<MRIController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Post([FromForm] StrokeInfo info)
        {
            string jsonPath = Path.Combine(@"C:\MRI\mriresult" ,Directory.GetParent(info.Path).ToString(), "stroke.json");

            // Delete the file if it exists.
            if (System.IO.File.Exists(jsonPath))
            {
                System.IO.File.Delete(jsonPath);
            }

            System.IO.File.WriteAllBytes(
                jsonPath,
                JsonSerializer.SerializeToUtf8Bytes<StrokeInfo>(
                    info,
                    new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true
                    }));
            return new OkResult();
        }
    }
}
