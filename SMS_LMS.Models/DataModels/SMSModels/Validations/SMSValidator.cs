using FluentValidation;
using SMS_LMS.Localization;
using SMS_LMS.Models.DataModels.SMSModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataModels.SMSModels.Validations
{
    public class SMSValidator : AbstractValidator<SMSDTO>
    {
        private readonly SMS_LMS_LocalService _localizer;
        public SMSValidator(SMS_LMS_LocalService localizer)
        {
            _localizer = localizer;
            RuleFor(x => x.user).NotEmpty().WithMessage(X => _localizer.LocalStr("Required"));
            RuleFor(x => x.pass).NotEmpty().WithMessage(X => _localizer.LocalStr("Required"));
            RuleFor(x => x.sURL).NotEmpty().WithMessage(X => _localizer.LocalStr("Required"));
            RuleFor(x => x.sid).NotEmpty().WithMessage(X => _localizer.LocalStr("Required"));
        }
    }
}
