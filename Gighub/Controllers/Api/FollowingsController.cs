using System.Linq;
using System.Web.Http;
using Gighub.Dtos;
using Gighub.Models;
using Microsoft.AspNet.Identity;

namespace Gighub.Controllers.Api
{
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FolloweeId == userId
                                             && f.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("Following already exists.");
            }

            var following = new Following() 
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
                
            };

            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();

        }
        // GET: Followings
        
    }
}