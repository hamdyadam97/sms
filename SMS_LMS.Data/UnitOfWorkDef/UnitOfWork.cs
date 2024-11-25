using AutoMapper;
using SMS_LMS.Data.Repository.PleasePreviewModelRep;
using SMS_LMS.Data.Repository.SMSDataModelRep;
using SMS_LMS.Data.Repository.SMSModelRep;
using SMS_LMS.Data.Repository.SystemNotificationRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.StageRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.StatusRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.StepRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.TransitionRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.UserAssignmentRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.WorkFlowActionsRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.WorkflowHistoryRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.WorkflowInstanceRep;
using SMS_LMS.Data.Repository.WorkFlowFolder.WorkflowRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.UnitOfWorkDef
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SMS_LMS_DBContext _db;
        private readonly IMapper _mapper;

        public IPleasePreviewModelRepository PleasePreviewModel { get; private set; }

        public INotificationRepository Notification { get; private set; }

        public IUserAssignmentRepository UserAssignment { get; private set; }

        public IStepRepository Step { get; private set; }

        public ITransitionRepositroy Transition { get; private set; }

        public IStageRepository Stage { get; private set; }

        public IStatusRepository Status { get; private set; }

        public IWorkFlowActionsRepository WorkFlowActions { get; private set; }

        public IWorkflowHistoryRepository WorkflowHistory { get; private set; }

        public IWorkflowRepository Workflow { get; private set; }

        public IWorkflowInstanceRepository WorkflowInstance { get; private set; }

        public ISMSDataModelRepository SMSDataRepository { get; private set; }

        public ISMSModelRepository SMSRepository { get; private set; }

        public UnitOfWork(SMS_LMS_DBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            PleasePreviewModel = new PleasePreviewModelRepository(_db, _mapper);
            Notification = new NotificationRepository(_db, _mapper);
            UserAssignment = new UserAssignmentRepository(_db, _mapper);
            Step = new StepRepository(_db, _mapper);
            Transition = new TransitionRepositroy(_db, _mapper);
            Stage = new StageRepository(_db, _mapper);
            Status = new StatusRepository(_db, _mapper);
            WorkFlowActions = new WorkFlowActionsRepository(_db, _mapper);
            WorkflowHistory = new WorkflowHistoryRepository(_db, _mapper);
            Workflow = new WorkflowRepository(_db, _mapper);
            WorkflowInstance = new WorkflowInstanceRepository(_db, _mapper);
            SMSDataRepository=new SMSDataModelRepository(_db, _mapper);
            SMSRepository= new SMSModelRepository(_db, _mapper);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task<int> SaveChangeAsync()
        {
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
