using System.Reflection;
using System.Linq;
using BlazorBaseApi;
using BlazorBaseApi.Hubs;
using BlazorBaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppController : ControllerBase
    {
        private MysqlDbContext _dbContext;
        //private readonly IHubContext<AppHub> _hubContext = default!;

        public AppController(MysqlDbContext dbContext/*, IHubContext<AppHub> hubContext*/)
        {
            _dbContext = dbContext;
            // _hubContext = hubContext;
        }

        [Route("{objClassName}/{id}")]
        [HttpGet]
        public IActionResult GetObject(string objClassName, int id)
        {
            dynamic? objResult;
            try
            {
                this._dbContext.WeatherForecast.Include(x => x.Id == 1).FirstOrDefault();
                objResult = GetInstance(objClassName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Ok(objResult);
        }

        [Route("{objClassName}")]
        [HttpGet]
        public IActionResult GetObjectList(string objClassName)
        {
            //var entityType = this._dbContext.Model.GetEntityTypes();
            var objResultList = new List<Object>();
            var type = Type.GetType("BlazorBaseApi.MysqlDbContext");
            if (type != null)
            {
                List<FieldInfo> list = this._dbContext.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic).ToList();
                IEntityType entityType = this?_dbContext.Model.FindEntityType(typeof(TEntity));
                FieldInfo? field = list.Where(f => f.Name.Contains(objClassName)).FirstOrDefault();
                if (field != null)
                {
                    MethodInfo? method = this._dbContext.GetType().GetMethod(objClassName, new Type[] { });
                    if (method != null)
                    {
                        var result = method.Invoke(this._dbContext, null);
                    }
                }
            }
            // try
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         objResultList.Add(GetInstance(objClassName));
            //     }
            // }
            // catch (Exception e)
            // {
            //     throw new Exception(e.Message);
            // }
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

                Type? type = Type.GetType(strFullyQualifiedName);
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
