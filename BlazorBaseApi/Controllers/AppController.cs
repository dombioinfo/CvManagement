using System.Reflection;
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
        [HttpGet]
        public IActionResult GetObjectList(string objClassName)
        {
            List<Object> objResultList = new List<object>();
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    objResultList.Add(GetInstance(objClassName));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Ok(objResultList);
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

                IEnumerable<Assembly> assemblyList = AppDomain.CurrentDomain.GetAssemblies();
                strFullyQualifiedName = string.Concat("BlazorBaseModel", ".", strFullyQualifiedName);
                foreach (var asm in assemblyList)
                {
                    type = asm.GetType(strFullyQualifiedName);
                    if (type != null)
                    {
                        result = Activator.CreateInstance(type);
                        break;
                    }
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
