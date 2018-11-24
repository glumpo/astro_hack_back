using System.Collections.Generic;

using astro_bot_back.Models;

using Microsoft.AspNetCore.Mvc;

namespace astro_bot_back.Controllers
{
    [Route ("api/[controller]")]
    public class UsersController : Controller
    {
        public UsersController (IUsersRepository userItems)
        {
            UserItems = userItems;
        }
        public IUsersRepository UserItems { get; set; }

        public IEnumerable<UsersItem> GetAll ()
        {
            return UserItems.GetAll ();
        }

        [HttpGet ("{id}", Name = "GetUser")]
        public IActionResult GetById (string id)
        {
            var item = UserItems.Find (id);
            if (item == null)
            {
                return NotFound ();
            }
            return new ObjectResult (item);
        }

        [HttpPost]
        public IActionResult Create ([FromBody] UsersItem item)
        {
            if (item == null)
            {
                return BadRequest ();
            }
            UserItems.Add (item);
            return CreatedAtRoute ("GetUser", new { id = item.User }, item);
        }

    }
}