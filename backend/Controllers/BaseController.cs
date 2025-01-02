using backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T>(DataBase dataBase) : ControllerBase where T : class
    {
        public virtual ActionResult<IEnumerable<T>> GetAll()
        {
            return Ok(dataBase.Set<T>().ToList());
        }

        public virtual ActionResult<T> GetById(int id)
        {
            var entity = dataBase.Set<T>().Find(id);
            return entity == null ? (ActionResult<T>)ListNotFoundResponse(id) : Ok(entity);
        }

        public virtual ActionResult<IEnumerable<T>> RemoveById(int id)
        {
            var entity = dataBase.Set<T>().Find(id);
            if (entity == null)
            {
                return (ActionResult<IEnumerable<T>>)ListNotFoundResponse(id);
            }
            
            dataBase.Set<T>().Remove(entity);
            dataBase.SaveChanges();
            
            return Ok(dataBase.Set<T>().ToList());
        }

        public virtual async Task<ActionResult<IEnumerable<T>>> Add(T entity)
        {
            dataBase.Set<T>().Add(entity);
            await dataBase.SaveChangesAsync();
            return Ok(dataBase.Set<T>().ToList());
        }

        protected virtual ContentResult ListNotFoundResponse(int id)
        {
            return Content($"Entity with id {id} not found");
        }
    }
}