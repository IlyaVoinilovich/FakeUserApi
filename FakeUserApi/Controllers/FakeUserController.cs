using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeUserApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using FakeUserApi;

namespace FakeUserApi.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class FakeUsersController : ControllerBase
        {
            private readonly FakeUserContext _context;
            private readonly ILogger _logger;
        public FakeUsersController(FakeUserContext context)
            {
                _context = context;
            }

            // GET: api/FakeUsers
            [HttpGet]
            public async Task<ActionResult<IEnumerable<FakeUser>>> GetFakeUsers()
            {
                 _logger.Log(LogLevel.Information, MyLogEvents.TestItem, "Getting all items");
                 _logger.LogInformation(MyLogEvents.TestItem, "Getting all items");
                 return await _context.FakeUsers.ToListAsync();
            }

            // GET: api/FakeUsers/5
            [HttpGet("{id}")]
            public async Task<ActionResult<FakeUser>> GetFakeUser([FromQuery]long id)
            {
                _logger.LogInformation(MyLogEvents.GetItem, "Getting item {Id}", id);
                var fakeUser = await _context.FakeUsers.FindAsync(id);

                if (fakeUser == null)
                {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                return NotFound();
                }
                return fakeUser;
        }

            // POST: api/FakeUsers
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            [HttpPost]
            public async Task<ActionResult<FakeUser>> PostFakeUser([FromBody]FakeUser fakeUser)
            {
                _context.FakeUsers.Add(fakeUser);
                await _context.SaveChangesAsync();
                _logger.LogInformation(MyLogEvents.GetItem, "Post item {Id}", fakeUser.Id);
                return CreatedAtAction("GetFakeUser", new { id = fakeUser.Id }, fakeUser);
            }
        }
    }
