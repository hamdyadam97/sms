using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS_LMS.Data.Helpers;
using SMS_LMS.Data.UnitOfWorkDef;
using SMS_LMS.Localization;
using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.ResponceModel;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using SMS_LMS.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SMS_LMS.Bussiness.Services.SMSDataModelServices
{
    public class SMSDataService : ISMSDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SMS_LMS_LocalService _LocalService;
        public SMSDataService(IUnitOfWork unitOfWork, IMapper Mapper, SMS_LMS_LocalService localService)
        {
            _unitOfWork = unitOfWork;
            _mapper = Mapper;
            _LocalService = localService;
        }

        public async Task<ResponseDto> Add(SMSDataDTO entity)
        {
            try
            {
                
                var objEntity = _mapper.Map<SMSDataTbl>(entity);
                objEntity.SendDate = DateTime.Now; 

                _unitOfWork.SMSDataRepository.Insert(objEntity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    return new ResponseDto
                    {
                        IsSuccess = true,
                        StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                        Result = entity
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                        Result = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                    Result = ex.Message
                };
            }
        }


        public ResponseDto GetAll()
        {
            try
            {
                var list = _unitOfWork.SMSDataRepository.GetAll();

                var smsDataDto = _mapper.Map<List<SMSDataTbl>>(list);
                return new ResponseDto()
                {
                    IsSuccess = true,
                    StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                    Result = smsDataDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                    Result = ex.Message
                };
            }
        }


        public async Task<ResponseDto> GetAll(SMSSearchDto searchCriteria)
        {
            try
            {
                
                List<Expression<Func<SMSDataTbl, bool>>> filterLst = new List<Expression<Func<SMSDataTbl, bool>>>();

             
                if (!string.IsNullOrEmpty(searchCriteria.Message))
                    filterLst.Add(x => x.Message.Contains(searchCriteria.Message));

                if (!string.IsNullOrEmpty(searchCriteria.MobileNo))
                    filterLst.Add(x => x.MobileNo.Contains(searchCriteria.MobileNo));

                if (searchCriteria.SendDate.HasValue && searchCriteria.SendDateEnd.HasValue)
                    filterLst.Add(x => x.SendDate.Value.Date >= searchCriteria.SendDate.Value.Date
                                        && x.SendDate.Value.Date < searchCriteria.SendDateEnd.Value.Date);

                if (!string.IsNullOrEmpty(searchCriteria.SearchTerm))
                    filterLst.Add(x => x.Message.Contains(searchCriteria.SearchTerm.ToLower())
                        || x.MobileNo.Contains(searchCriteria.SearchTerm.ToLower())
                        || x.SendDate.ToString().Contains(searchCriteria.SearchTerm.ToLower()));

               
                var combinedFilter = CombineExpressions(filterLst);

               
                var filteredData = _unitOfWork.SMSDataRepository.GetAll(
                    combinedFilter,
                    x => x.OrderByDescending(z => z.SendDate)
                );

               
                int totalCount = filteredData.Count();

              
                var paginatedData = _unitOfWork.SMSDataRepository.GetAllWithPaging(
                    combinedFilter,
                    x => x.OrderByDescending(z => z.SendDate),
                    null,
                    searchCriteria.PageInfoDto.PageIndex,
                    searchCriteria.PageInfoDto.PageSize
                ).ToList();


                //int pageCount = (int)Math.Ceiling((double)totalCount / searchCriteria.PageInfoDto.PageSize);
                //int pageCount = searchCriteria.PageInfoDto.PageSize;
                int pageCount = paginatedData.Count();



                var paginationDto = new PaginationDTO<SMSDataTbl>(
                    paginatedData,
                    totalCount,
                    searchCriteria.PageInfoDto.PageIndex,
                    pageCount
                );

                return new ResponseDto()
                {
                    IsSuccess = true,
                    StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                    Result = paginationDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                    Result = ex.Message
                };
            }
        }

        // Helper method to combine expressions
        public static Expression<Func<T, bool>> CombineExpressions<T>(List<Expression<Func<T, bool>>> expressions)
        {
            if (!expressions.Any()) return x => true; // No filter
            var combined = expressions[0];
            for (int i = 1; i < expressions.Count; i++)
            {
                combined = Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(combined.Body, expressions[i].Body),
                    combined.Parameters
                );
            }
            return combined;
        }




        [ProducesResponseType(typeof(PaginationObject<SMSDataDTO>), 200)]
        public async Task<ResponseDto> Getlist(int pageNum, int pagSize, Expression<Func<SMSDataTbl, object>> orderBy = null, string orderByDirection = "ASC")
        {
            PaginationObject<SMSDataDTO>? results = null;
            SMSDataTbl objEntity = new SMSDataTbl();
            SMSDataDTO Entity = new SMSDataDTO();
            _mapper.Map(Entity, objEntity);
            if (orderBy == null)
            {
                var res = (await _unitOfWork.SMSDataRepository.GetAllAsync()).ProjectTo<SMSDataDTO>(_mapper.ConfigurationProvider);
                results = PaginationHelper.Create(res, pageNum, pagSize);
                return new ResponseDto() { IsSuccess = true, StatusCode = ((int)ResponseMessagesEnum.OK).ToString(), Result = results };

            }
            else
            {
                if (orderByDirection == "ASC")
                {
                    var res = (await _unitOfWork.SMSDataRepository.GetAllAsync()).OrderBy(orderBy).ProjectTo<SMSDataDTO>(_mapper.ConfigurationProvider);
                    results = PaginationHelper.Create(res, pageNum, pagSize);
                    return new ResponseDto() { IsSuccess = true, StatusCode = ((int)ResponseMessagesEnum.OK).ToString(), Result = results };

                }
                else
                {
                    var res = (await _unitOfWork.SMSDataRepository.GetAllAsync()).OrderByDescending(orderBy).ProjectTo<SMSDataDTO>(_mapper.ConfigurationProvider);
                    results = PaginationHelper.Create(res, pageNum, pagSize);
                    return new ResponseDto() { IsSuccess = true, StatusCode = ((int)ResponseMessagesEnum.OK).ToString(), Result = results };

                }
            }

        }
    }
}
