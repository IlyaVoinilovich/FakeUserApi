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
using System.Security.Claims;

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
        private long UserId => long.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
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
        public async Task<ActionResult<IEnumerable<FakeUserView>>> GetFakeUsers()
        {
            _logger.LogInformation(MyLogEvents.TestItem, "Getting all users by user {id}",UserId);
            var _fakeUsers =await _context.FakeUsers.ToListAsync();
            return Ok(_fakeUsers);
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
        public async Task<ActionResult> GetFakeUser(long id)
        {

            var _user = await _context.FakeUsers.FindAsync(id);

            if (_user == null)
            {
                return NotFound();
            }
            _logger.LogInformation(MyLogEvents.GetItem, "Find user {Id} by user {idUser}",(id,UserId));
            return Ok(_user);
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
        public async Task<IActionResult> Authenticate(AuthenticateCommand command)
        {
            var response = _userservice.Authenticate(command);

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
        public IActionResult RefreshPassword([FromBody]RefreshPasswordCommand model)
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
