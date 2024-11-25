using FluentValidation;
using SMS_LMS.Localization;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataModels.SMSDataModels.Validations
{
    public class SMSDataValidator : AbstractValidator<SMSDataDTO>
    {
        private readonly SMS_LMS_LocalService _localizer;
        public SMSDataValidator(SMS_LMS_LocalService localizer)
        {
            _localizer = localizer;
            //  RuleFor(x => x.MobileNo).NotEmpty().WithMessage(X => _localizer.LocalStr("Required"));
            RuleFor(x => x.MobileNo)
                        .NotEmpty().WithMessage(x => _localizer.LocalStr("Required"))
                        .NotEmpty().WithMessage(x => _localizer.LocalStr("InvalidMobileNumber")); 

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage(x => _localizer.LocalStr("Required"))
                .MaximumLength(160).WithMessage(x => _localizer.LocalStr("MaxLengthExceeded"));

           

        }
    }
}
