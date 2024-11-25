using FluentValidation;
using SMS_LMS.Localization;
using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.PleasePreDataModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataModels.PleasePreDataModels.Validations
{
    public class PleasePreviewValidator : AbstractValidator<PleasePreviewDTO>
    {
        private readonly SMS_LMS_LocalService _localizer;
        public PleasePreviewValidator(SMS_LMS_LocalService localizer)
        {
            _localizer = localizer;
            RuleFor(x => x.Name).NotEmpty().WithMessage(X => _localizer.LocalStr("Required"));
        }
    }
}
