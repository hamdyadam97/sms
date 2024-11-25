using AutoMapper;
using SMS_LMS.Data.Repository.MasterRep;
using SMS_LMS.Models.DataBaseTablesModels.WorkFlowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.Repository.WorkFlowFolder.TransitionRep
{
    public class TransitionRepositroy : Repository<Transition>, ITransitionRepositroy
    {
        private readonly SMS_LMS_DBContext _db;
        private readonly IMapper _mapper;
        public TransitionRepositroy(SMS_LMS_DBContext db, IMapper mapper) : base(db, mapper)
        {
            _mapper = mapper;
            _db = db;
        }
    }
}
