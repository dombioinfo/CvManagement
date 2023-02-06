using System.Reflection;
using System.Linq;
using BlazorBaseApi;
using BlazorBaseApi.Hubs;
using BlazorBaseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using BlazorBaseModel.Db;

namespace BlazorBaseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppController : ControllerBase
    {
        protected MysqlDbContext _dbContext;
        // protected readonly IMapper? _mapper;
        //private readonly IHubContext<AppHub> _hubContext = default!;

        public AppController(
            MysqlDbContext dbContext
            // , IMapper mapper
        /*, IHubContext<AppHub> hubContext*/
        )
        {
            _dbContext = dbContext;
            // _mapper = mapper;
            // _hubContext = hubContext;
        }

/*
        [Route("{objClassName}/{id}")]
        [HttpGet]
        public IActionResult GetObject(string objClassName, int id)
        {
            dynamic? objResult;
            try
            {
                this._dbContext.Users.Include(x => x.Id == id).FirstOrDefault();
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
            var types = this._dbContext.GetType().GetProperties();

            // Check if property is really a DbSet<TEntity>
            var filteredTypes = types.Where(x => x.PropertyType.IsGenericType
                                                && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            // foreach (var typeFilter in filteredTypes)
            // {
            //     Console.WriteLine($"{typeFilter.Name}");
            //     // var dbSet = (IEnumerable<dynamic>)typeFilter.GetValue(this._dbContext);
            //     // var entities = dbSet.ToList();
            // }
            PropertyInfo propertyInfo = filteredTypes.Where(t => t.Name == objClassName).First();
            if (propertyInfo != null)
            {
                var dbSet = (IEnumerable<dynamic>)propertyInfo.GetValue(this._dbContext);
                if (dbSet != null)
                {
                    var entities = dbSet.ToList();
                    Ok(entities);
                }
            }
            return Ok();
        }

        [Route("{objClassName}")]
        [HttpPost]
        public IActionResult PostObject(string objClassName, Object objToSave)
        {
            var objectResult = GetInstance(objClassName);

            return Ok(objectResult);
        }
*/
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
