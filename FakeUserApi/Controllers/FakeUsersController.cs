using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeUserApi.Models;
using Microsoft.Extensions.Logging;

namespace FakeUserApi.Controllers
{
    /// <summary>
    /// FakeUsersController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FakeUsersController : ControllerBase
    {
        private readonly FakeUserContext _context;
        private readonly ILogger<FakeUsersController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public FakeUsersController(FakeUserContext context, ILogger<FakeUsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/FakeUsers
        /// <summary>
        /// Get all FakeUsers
        /// </summary>
        /// <returns>All FakeUsers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FakeUser>>> GetFakeUsers()
        {
            _logger.LogInformation(MyLogEvents.TestItem, "Getting all items");
            return await _context.FakeUsers.ToListAsync();
        }

        // GET: api/FakeUsers/5
        /// <summary>
        /// Find a FakeUsers
        /// </summary>
        /// <param name="id"></param>
        /// <response code="201">If find</response>
        /// <response code="400">If not find</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FakeUser), 204)]
        public async Task<ActionResult<FakeUser>> GetFakeUser(long id)
        {

            var fakeUser = await _context.FakeUsers.FindAsync(id);

            if (fakeUser == null)
            {
                return NotFound();
            }
            _logger.LogInformation(MyLogEvents.GetItem, "Find item {Id}", fakeUser.Id);
            return fakeUser;
        }
        /// <summary>
        /// Add new FakeUsers
        /// </summary>
        /// <remarks>
        /// {
        ///     "name": "string",
        ///     "lastname": "string"
        /// }
        /// </remarks>
        /// <param name="fakeUser"></param>
        /// <returns>New FakeUsers</returns>
        [HttpPost]
        [ProducesResponseType(typeof(FakeUser), 201)]
        public async Task<ActionResult<FakeUser>> PostFakeUser(FakeUser fakeUser)
        {
            var FakeUser = _context.FakeUsers.FindAsync(fakeUser.Id);
            if (FakeUser != null)
            {
                _logger.LogInformation(MyLogEvents.GetItem, "Post item {Id}", fakeUser.Id);
                _context.FakeUsers.Update(fakeUser);
            }
            else
            {
                _logger.LogInformation(MyLogEvents.GetItem, "Add item {Id}", fakeUser.Id);
                _context.FakeUsers.Add(fakeUser);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFakeUser", new { id = fakeUser.Id }, fakeUser);
        }
    }
}
