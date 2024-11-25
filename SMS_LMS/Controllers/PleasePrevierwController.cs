using Microsoft.AspNetCore.Mvc;
using SMS_LMS.Bussiness.Services.PleasePreviewModelServices;
using SMS_LMS.Models.DataModels.ResponceModel;

namespace SMS_LMS.Controllers
{
    [Route("api/pleasePreview")]
    [ApiController]
    public class PleasePrevierwController : ControllerBase
    {
        private readonly IPleasePreviewService _service;
        public PleasePrevierwController(IPleasePreviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            return _service.GetAll();
        }
    }
}
