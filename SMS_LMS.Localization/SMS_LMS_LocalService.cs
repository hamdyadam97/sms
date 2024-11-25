using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace SMS_LMS.Localization
{
    public class SMS_LMS_LocalService
    {
        private readonly IStringLocalizer _localizer;
        public SMS_LMS_LocalService(IStringLocalizerFactory factor)
        {
            var type = typeof(SMS_LMS_LocalService);
            var assembly = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factor.Create("SMS_LMS" + "_LocalService", assembly.Name);
        }

        public LocalizedString LocalStr(string key)
        {
            return _localizer[key];
        }

        public LocalizedString LocalStr(string key, string parameter)
        {
            return _localizer[key, parameter];
        }
    }
}
