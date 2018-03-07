using System.Linq;
using System.Web.Http;
using Gighub.Dtos;
using Gighub.Models;
using Microsoft.AspNet.Identity;

namespace Gighub.Controllers.Api
{   
    // make sure the logged in User
    [Authorize]
    public class AttendancesController : ApiController
    {
       
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }
        //ASP.NET Web API by default does not look for scalar parameter like
        //an integer in the request body
        //so we need to use FromBody
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a=> a.AttendeeId == userId 
            && a.GigId == dto.GigId ))
                return BadRequest("Attendance already exists.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }

    }
}
