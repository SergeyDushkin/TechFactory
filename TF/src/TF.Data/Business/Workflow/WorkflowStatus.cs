using System;

namespace TF.Data.Business.Workflow
{
    /// <summary>
    /// table to store statuses in workflow 
    /// </summary>
    public class WorkflowStatus
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
