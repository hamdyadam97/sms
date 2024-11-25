using AutoMapper;
using Microsoft.Extensions.Configuration;
using SMS_LMS.Bussiness.Services.UserServices;
using SMS_LMS.Data.UnitOfWorkDef;
using SMS_LMS.Localization;
using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.ResponceModel;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using SMS_LMS.Models.DataModels.SMSModels.DTO;
using SMS_LMS.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.Services.SMSModelServices
{
    public class SMSService : ISMSService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SMS_LMS_LocalService _LocalService;
        private readonly IUserService _userServices;
        private readonly IConfiguration _configuration;
        public SMSService(IUnitOfWork unitOfWork, IMapper Mapper, SMS_LMS_LocalService localService,
            IConfiguration configuration,

            IUserService userServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = Mapper;
            _LocalService = localService;
            _userServices = userServices;
            _configuration = configuration;
        }

        public async Task<ResponseDto> Add(SMSDTO entity)
        {
            try
            {
                await DeletePrevConfig();

              
                var objEntity = _mapper.Map<SMSConfigTbl>(entity);

                
                _unitOfWork.SMSRepository.Insert(objEntity);

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


        public async Task<ResponseDto> DeletePrevConfig()
        {

            _unitOfWork.SMSRepository.DeleteAllRows();
            if (await _unitOfWork.SaveChangeAsync() > 0)
            {
                return new ResponseDto()
                {
                    IsSuccess = true,
                    StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                    Result = null
                };
            }
            else
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                    Result = null

                };
            }
        }




        public ResponseDto GetAll()
        {
            try
            {
                var list = _unitOfWork.SMSRepository.GetAll();
                var SMSConfiglist = _mapper.Map<List<SMSConfigTbl>>(list);
                return new ResponseDto()
                {
                    IsSuccess = true,
                    StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                    Result = SMSConfiglist
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

        public async Task<ResponseDto> GetData()
        {
            try
            {
                var data = _unitOfWork.SMSRepository.GetFirstOrDefault();

                if (data != null)
                {
                    
                    var smsDTO = _mapper.Map<SMSDTO>(data);

                    return new ResponseDto
                    {
                        IsSuccess = true,
                        StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                        Result = smsDTO
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        IsSuccess = false,
                        StatusCode = ((int)ResponseMessagesEnum.NoContent).ToString(),
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


        public class RootObject
        {
            public Response1 Response { get; set; }
            public string ErrorMessage { get; set; }
            public int Status { get; set; }
        }
        public class Response1
        {
            public int batch_id { get; set; }
            public string status { get; set; }
            public int value { get; set; }

        }
        public async Task<ResponseDto> SendSMS(SMSDataDTO smsDataDto)
        {
            string baseURL = _configuration["SMSMisr:baseURL"];
            string username = _configuration["SMSMisr:username"];
            string password = _configuration["SMSMisr:password"];
            string sender = _configuration["SMSMisr:sender"];


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            

            
            string country = "002"; // Country code prefix
            string mobileNo = smsDataDto.MobileNo; // Use MobileNo from dto
            string message = smsDataDto.Message;

            // Construct the full URL for the SMS API
            //string requestUrl = $"{baseURL}?accesskey={username}&sid={sender}&mno={country}{mobileNo}&type=1&text={Uri.EscapeDataString(message)}&respformat=json";
            string requestUrl = $"{baseURL}?environment=1&username={Uri.EscapeDataString(username)}&password={Uri.EscapeDataString(password)}&language=a&sender={Uri.EscapeDataString(sender)}&mobile={Uri.EscapeDataString(mobileNo)}&message={Uri.EscapeDataString(message)}&DelayUntil=X";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        // Parse response if needed and determine the success of the operation
                        if (responseContent.Contains("Response"))
                        {
                            return new ResponseDto()
                            {
                                IsSuccess = true,
                                StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                                Result = responseContent
                            };
                        }
                        else
                        {
                            return new ResponseDto()
                            {
                                IsSuccess = false,
                                StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                                Result = responseContent
                            };
                        }
                    }
                    else
                    {
                        return new ResponseDto()
                        {
                            IsSuccess = false,
                            StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                            Result = $"HTTP Error: {response.StatusCode}"
                        };
                    }
                }
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


        public async Task<ResponseDto> SendSMS(UnathorizedSMSDataDTO smsDataDto)
        {
            string country = "002";
            string baseURL = _configuration["SMSMisr:baseURL"];
            string username = _configuration["SMSMisr:username"];
            string password = _configuration["SMSMisr:password"];
            string sender = _configuration["SMSMisr:sender"];
            string mobileNo = "01092263283";// Ensure correct property name
            string message = smsDataDto.Message;

            // Construct the final URL dynamically
            string fURL = $"{baseURL}?accesskey={username}&sid={sender}&mno={country}{mobileNo}&type=1&text={message}&respformat=json";

            try
            {
                using (WebClient client = new WebClient())
                {
                    // Call the SMS service
                    string responseString = await client.DownloadStringTaskAsync(fURL);

                    // Deserialize the response
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(responseString);

                    // Check the response and return accordingly
                    if (responseString.Contains("Response"))
                    {
                        return new ResponseDto
                        {
                            IsSuccess = true,
                            StatusCode = ((int)ResponseMessagesEnum.OK).ToString(),
                            Result = responseString
                        };
                    }
                    else
                    {
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                            Result = responseString
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a proper error response
                return new ResponseDto
                {
                    IsSuccess = false,
                    StatusCode = ((int)ResponseMessagesEnum.InternalError).ToString(),
                    Result = $"Error occurred: {ex.Message}"
                };
            }
        }


        private async Task<ResponseDto> AddSMSDataService(SMSDataDTO entity)
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


        private async Task<ResponseDto> AddSMSDataService(UnathorizedSMSDataDTO entity)
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

    }
}
