using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMS_LMS.Bussiness.Services.SMSDataModelServices;
using SMS_LMS.Bussiness.Services.SMSModelServices;
using SMS_LMS.Bussiness.Services.UserServices;
using SMS_LMS.Models.DataModels.ResponceModel;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using SMS_LMS.Models.DataModels.SMSModels.DTO;
using SMS_LMS.Models.General;

namespace SMS_LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ResponseDto _response;
        private readonly ISMSService _service;
        private readonly ISMSDataService _dataservice;
        private readonly IUserService _userserice;
        private readonly ILogger _logger;
        public SMSController(ISMSService service, ISMSDataService dataService, ILogger<SMSController> logger, IUserService userService)
        {
            _service = service;
            _dataservice = dataService;
            _logger = logger;
            _userserice = userService;
            _logger.LogInformation("Send SMS Controller has been started");
        }
        //[Authorize]
        [HttpPost("SaveSMSConfiguration")]
        public async Task<ResponseDto> SaveSMSConfiguration([FromBody] SMSDTO model)
        {
            var res = await _service.Add(model);
            string token = Request.Headers["Authorization"].ToString();
            _logger.LogInformation("Header : {header} - Save SMS Configuration - Response : {Response} - Token used : {token}",
                HttpContext.Request.GetDisplayUrl(), JsonConvert.SerializeObject(res), token);
            return res;
        }
        //[Authorize]
        [HttpGet("SMS")]
        public async Task<ResponseDto> SMS([FromHeader] SMSDataDTO model)
        {
            string token = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(model.MobileNo))
            {
                string parentId = (User.FindFirst("ParentId").Value).ToString();
                if (!string.IsNullOrEmpty(parentId))
                {
                    var user = _userserice.GetUserData(parentId, token);
                    var userdta = JsonConvert.DeserializeObject<UsersDto>(user.Result.ToString());
                    model.MobileNo = userdta.mobile;
                }
            }
            var res = await _service.SendSMS(model);
            _logger.LogInformation("Header : {header} - Send SMS Configuration - Response : {Response} - Token used : {token}",
                HttpContext.Request.GetDisplayUrl(), JsonConvert.SerializeObject(res), token);
            return res;
        }





        
        [HttpPost("SendSMS")]
        public async Task<ResponseDto> SendSMS(SMSDataDTO? model)
        {
            string token = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(model.MobileNo))
            {
                string parentId = (User.FindFirst("ParentId").Value).ToString();
                if (!string.IsNullOrEmpty(parentId))
                {
                    var user = _userserice.GetUserData(parentId, token);
                    var userdta = JsonConvert.DeserializeObject<UsersDto>(user.Result.ToString());
                    model.MobileNo = userdta.mobile;
                }
            }

            var res = await _service.SendSMS(model);
            _logger.LogInformation("Header : {header} - Send SMS Configuration - Response : {Response} - Token used : {token}",
                HttpContext.Request.GetDisplayUrl(), JsonConvert.SerializeObject(res), token);
            return res;

        }
        [HttpPost("SendSMS_Unathorized")]
        public async Task<ResponseDto> _SendSms([FromBody] UnathorizedSMSDataDTO model)
        {
            var res = await _service.SendSMS(model);
            _logger.LogInformation("Header : {header} - Send SMS Configuration - Response : {Response} - Token used : {token}", 
                HttpContext.Request.GetDisplayUrl(), JsonConvert.SerializeObject(res), null);
            return res;
        }
        //[Authorize]
        [HttpGet("ReadSMSConfiguration")]
        public async Task<ResponseDto> readSMSConfiguration()
        {
            var res = _service.GetData();
            string token = Request.Headers["Authorization"].ToString();
            _logger.LogInformation("Header : {header} - Read SMS Configuration - Response : {Response} - Token used : {token}",
                HttpContext.Request.GetDisplayUrl(), JsonConvert.SerializeObject(res), token);
            return res.Result;
        }
        //[Authorize]
        [HttpGet("GetAllSMSLogs")]
        public async Task<ResponseDto> GetAllSMSLogs([FromQuery] SMSSearchDto smsSearchDto)
        {
            string token = Request.Headers["Authorization"].ToString();
            var res = _dataservice.GetAll(smsSearchDto);
            _logger.LogInformation("Header : {header} - Get commission By Search Creatria  - Response : {Response} - Token used : {token}",
                HttpContext.Request.GetDisplayUrl(), JsonConvert.SerializeObject(res), token);
            return res.Result;
        }

    }
}
