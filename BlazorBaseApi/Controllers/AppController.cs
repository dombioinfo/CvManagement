using BlazorBaseApi.Hubs;
using BlazorBaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppController : ControllerBase
    {
        //private readonly IHubContext<AppHub> _hubContext = default!;

        // public AppController(IHubContext<AppHub> hubContext)
        // {
        //     _hubContext = hubContext;
        // }

        [Route("{objClassName}/{id}")]
        [HttpGet]
        public IActionResult GetObject(string objClassName, int id)
        {
            dynamic objResult;
            try
            {
                objResult = GetInstance(objClassName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(objResult);
        }

        [Route("{objClassName}")]
        [HttpPost]
        public IActionResult PostObject(string objClassName, Object objToSave)
        {
            var objectResult = GetInstance(objClassName);

            return Ok(objectResult);
        }

        private dynamic? GetInstance(string strFullyQualifiedName)
        {
            object? result = null;
            try
            {
                if (string.IsNullOrEmpty(strFullyQualifiedName))
                    throw new Exception("strFullyQualifiedName cannot be null");

                Type type = Type.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);


                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = asm.GetType(strFullyQualifiedName);
                    if (type != null)
                        result = Activator.CreateInstance(type);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}
