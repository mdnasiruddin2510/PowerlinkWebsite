using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.Models;

namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        protected readonly AppDbContext _db;
        public BannerController( AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetBanner")]
        public async Task<List<string>> GetBanner(string page, string position,string appId)
        {
            return await _db.Banner.Where(x => !x.Deleted
                                        && x.Page == page &&  x.Position == position)
                                    .Select(s => s.Url).ToListAsync();
        }
    }
}
