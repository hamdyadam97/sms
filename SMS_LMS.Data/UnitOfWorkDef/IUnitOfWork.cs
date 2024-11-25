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
    public interface IUnitOfWork : IDisposable
    {
        IPleasePreviewModelRepository PleasePreviewModel { get; }

        //system notification
        

        // add all work flow repositories here
        IUserAssignmentRepository UserAssignment { get; }
        IStepRepository Step { get; }
        ITransitionRepositroy Transition { get; }
        IStageRepository Stage { get; }
        IStatusRepository Status { get; }
        IWorkFlowActionsRepository WorkFlowActions { get; }
        IWorkflowHistoryRepository WorkflowHistory { get; }
        IWorkflowRepository Workflow { get; }
        IWorkflowInstanceRepository WorkflowInstance { get; }
        ISMSDataModelRepository SMSDataRepository {  get; }
        ISMSModelRepository SMSRepository { get; }
        void Save();
        Task<int> SaveChangeAsync();
    }
}
