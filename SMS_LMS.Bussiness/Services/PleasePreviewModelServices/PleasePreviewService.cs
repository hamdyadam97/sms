using AutoMapper;
using SMS_LMS.Data.UnitOfWorkDef;
using SMS_LMS.Localization;
using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.PleasePreDataModels.DTO;
using SMS_LMS.Models.DataModels.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.Services.PleasePreviewModelServices
{
    public class PleasePreviewService : IPleasePreviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SMS_LMS_LocalService _LocalService;
        public PleasePreviewService(IUnitOfWork unitOfWork, IMapper Mapper, SMS_LMS_LocalService localService)
        {
            _unitOfWork = unitOfWork;
            _mapper = Mapper;
            _LocalService = localService;
        }

        public ResponseDto Add(PleasePreviewDTO Entity)
        {
            try
            {
                PleasePreview pleasePreview = new PleasePreview();
                _mapper.Map(Entity, pleasePreview);
                _unitOfWork.PleasePreviewModel.Insert(pleasePreview);
                return new ResponseDto()
                {
                    IsSuccess = true,
                    Message = _LocalService.LocalStr("SuccessMessage"),
                    Result = pleasePreview
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        public ResponseDto GetAll()
        {
            try
            {
                var list = _unitOfWork.PleasePreviewModel.GetAll();
                var pleasePreviewList = _mapper.Map<List<PleasePreviewDTO>>(list);
                return new ResponseDto()
                {
                    IsSuccess = true,
                    Message = _LocalService.LocalStr("SuccessMessage"),
                    Result = pleasePreviewList
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }
}
