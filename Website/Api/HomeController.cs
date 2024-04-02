using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.Models;
using Website.View_Models;

namespace Website.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<ActionResult<List<VmMenu>>> GetAllMenu()
        {
            var menu = await _db.Menu.OrderBy(x=> x.Order).Select(x => new VmMenu
            {
                Id = x.Id,
                Title = x.Title,
                Url = x.Url,
            }).ToListAsync();
            return menu;
        }
    }
}
