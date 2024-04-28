using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.Models;
using Website.View_Models;

namespace Website.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantController : ControllerBase
    {
        private readonly AppDbContext _db;

        public VariantController(AppDbContext db)
        {
            _db = db; 
        }
        [HttpGet("GetVariantInfo")]
        public async Task<ActionResult<List<VmVariant>>> GetVariantInfo()
        {
            var Variant = await (from option in _db.VariantOption 
                                 join value in _db.VariantValue on option.Id equals value.OptionId
                                 where !option.Deleted && !value.Deleted
                                 select new VmVariant
                                 {
                                     Id = option.Id,
                                     VariantOption = option.Name,
                                     Name = value.Name,
                                     ValueType = value.ValueType
                                 }).ToListAsync();
            return Variant;
        }
    }
}
