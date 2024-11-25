using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SMS_LMS.Models.DataModels.ResponceModel;
using SMS_LMS.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public ResponseDto GetUserData(string userId, string token)
        {
            var gatewayUrl = _config.GetSection("ApiSettings:Gateway").Value;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(gatewayUrl + "/UserMangement/GetUserbyId/" + userId);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, token);

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var result = "";
            var response = httpResponse.GetResponseStream();
            if (response == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    StatusCode = ResponseMessagesEnum.NoContent.ToString()
                };
            }
            using (var streamReader = new StreamReader(response))
            {
                result = streamReader.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(result))
            {
                ResponseDto messageResp = JsonConvert.DeserializeObject<ResponseDto>(result);
                return messageResp;
            }
            return new ResponseDto()
            {
                IsSuccess = false,
                StatusCode = ResponseMessagesEnum.NoContent.ToString(),
            };
        }
    }
}
