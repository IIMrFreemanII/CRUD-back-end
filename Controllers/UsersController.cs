using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_back_end.Models;

namespace CRUD_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersContext db;
        
        public UsersController(UsersContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new User { Name = "Tom", Age = 26 });
                db.Users.Add(new User { Name = "Alice", Age = 31 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await db.Users.FindAsync(id);
            
            if (user == null)
                return NotFound();

            return new ObjectResult(user);
        }
        
        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (user==null)
            {
                ModelState.AddModelError("", "Not entered data for user");
                return BadRequest(ModelState);
            }

            if (user.Age == 99)
            {
                ModelState.AddModelError("Age", "Age mustn't equal 99");
            }

            if (user.Name == "admin")
            {
                ModelState.AddModelError("Name", "Invalid username");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 
            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }
        
        // PUT api/users/
        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            if (user==null)
            {
                return BadRequest();
            }
            
            if (db.Users.Any(x => x.Id == user.Id))
            {
                db.Update(user);
                db.SaveChanges();
                return Ok(user);
            }
            
            return NotFound();
        }
 
        // DELETE api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if(user==null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(user);
        }
    }
}