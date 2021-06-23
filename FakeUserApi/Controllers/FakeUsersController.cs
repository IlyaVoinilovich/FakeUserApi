using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeUserApi.Models;
using Microsoft.Extensions.Logging;
using FakeUserApi.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

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
        private readonly IFakeUserService _userservice;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="userservice"></param>
        public FakeUsersController(FakeUserContext context, ILogger<FakeUsersController> logger, IFakeUserService userservice)
        {
            _context = context;
            _logger = logger;
            _userservice = userservice;
        }

        // GET: api/FakeUsers
        /// <summary>
        /// Get all FakeUsers
        /// </summary>
        /// <returns>All FakeUsers</returns>
        [Authorize]
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
        [Authorize]
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
        /// Create FakeUser
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
            var hashedpass= BCrypt.Net.BCrypt.HashPassword(fakeUser.HashPass);
            fakeUser.HashPass = hashedpass;
            var FakeUser = await _context.FakeUsers.FindAsync(fakeUser.Id);
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
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = _userservice.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return  Ok(response);
        }
        /// <summary>
        /// RefreshPassword
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("RefreshPassword")]
        public IActionResult RefreshPassword([FromBody]RefreshPasswordRequest model)
        {
            var account = _context.FakeUsers.SingleOrDefault(x => x.Email == model.Email);

            if (account == null)
            {
                return null;
            }

            return Ok(account.Id);
        }
    }
}
