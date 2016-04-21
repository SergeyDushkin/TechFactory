using System;

namespace TF.Data.Business.Workflow
{
    /// <summary>
    /// table which remains ability of changing statuses in workflow, ie NEW > (OPEN || CANCELED) > CLOSED
    /// ROOT NODE doesn't have record in this table
    /// </summary>
    public class WorkflowMap
    {
        public Guid Id { get; set; }

        /// <summary>
        /// reference to the workflow status
        /// </summary>
        public Guid StatusId { get; set; }

        /// <summary>
        /// reference to the parent status
        /// </summary>
        public Guid ParentStatusId { get; set; }
    }
}
