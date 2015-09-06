using FindMyBeaconReal.Logic;
using FindMyBeaconReal.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phelp_Server.Controllers
{
    public abstract class MyCrudController<T> : Controller
    {
        public abstract Task<IEnumerable<T>> GetAllCall();
        public abstract Task<T> GetByIdCall(int id);
        public abstract Task<T> CreateCall(T obj);
        public abstract Task<T> UpdateCall(int id, T obj);
        public abstract Task<IActionResult> DeleteCall(int id);

        [HttpGet]
        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await GetAllCall();
            return result;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var obj = await GetByIdCall(id);
            if (obj == null) return HttpNotFound();
            return new ObjectResult(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T obj)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(obj);
            }
            else
            {
                obj = await CreateCall(obj);
                return new ObjectResult(obj);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] T obj)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(obj);
            }
            else
            {
                var exists = await UpdateCall(id, obj);
                if (exists == null) return HttpNotFound();
                return new HttpStatusCodeResult(204);

            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteCall(id);
        }
    }
}