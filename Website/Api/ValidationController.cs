using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.Helper;
using PosWebsite.Models;
using System.ComponentModel.Design;
using Website.Helper;
using static System.Net.WebRequestMethods;

namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly AppDbContext _db;
        protected string companyId = "COM-2";
        private string baseurl = "";
        private readonly IConfiguration _configuration;
        private Helper.SMSBody SMSBody = new SMSBody();

        public ValidationController(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            baseurl = configuration["baseURL"];
            _configuration = configuration;
        }
        [Route("SendOTP")]
        [HttpGet]
        public async Task<ActionResult<int>> SendOTP(string userName, string appId)
        {
            EmailService email = new();
            var oldata = await _db.Otp.Where(x => x.Email == userName && x.CompanyId == appId && !x.Used && x.CreatedDate.AddSeconds(30) < AppFunction.BDDateTime()).ToListAsync();
            foreach (var item in oldata)
            {
                var update = item;
                update.Used = true;
                _db.Entry(item).CurrentValues.SetValues(update);
                _db.SaveChanges();
            }

            var data = await _db.Otp.FirstOrDefaultAsync(x => x.Email == userName && x.CompanyId == appId && !x.Used);
            var code = 0;
            if (data == null)
            {
                code = AppFunction.Generate_4_digitRandomNo();
                var otp = new Otp
                {
                    Code = code,
                    CreatedDate = AppFunction.BDDateTime(),
                    Email = userName,
                    SentCount = 1,
                    Used = false,
                    CompanyId = appId,
                };
                _db.Otp.Add(otp);
                await _db.SaveChangesAsync();
                var sendOTP = string.Format(SMSBody.sendOTP, code, "");
                await email.SendMailAsync(userName, sendOTP, _db, appId);
                return 1;
            }

            return 0;
        }
        [Route("Validate")]
        [HttpGet]
        public async Task<int> ValidateOTP(string userName, int otp, string appId)
        {
            var result = 0;
            var data = _db.Otp.FirstOrDefault(x => x.Email.ToLower() == userName.ToLower() && x.CompanyId == companyId && !x.Used && x.Code == otp);
            if (data is not null)
            {
                var update = data;
                update.Used = true;
                _db.Entry(data).CurrentValues.SetValues(update);
                await _db.SaveChangesAsync();
                result = 1;
            }
            return result;
        }
    }
}
