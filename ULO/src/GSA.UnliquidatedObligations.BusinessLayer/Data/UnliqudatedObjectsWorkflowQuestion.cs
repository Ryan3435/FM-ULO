//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GSA.UnliquidatedObligations.BusinessLayer.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UnliqudatedObjectsWorkflowQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnliqudatedObjectsWorkflowQuestion()
        {
            this.RequestForReassignments = new HashSet<RequestForReassignment>();
        }
    
        public int UnliqudatedWorkflowQuestionsId { get; set; }
        public int WorkflowId { get; set; }
        public string UserId { get; set; }
        public string Answer { get; set; }
        public string JustificationKey { get; set; }
        public string Comments { get; set; }
        public bool Pending { get; set; }
        public byte[] WorkflowRowVersion { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestForReassignment> RequestForReassignments { get; set; }
        public virtual Workflow Workflow { get; set; }
    }
}
