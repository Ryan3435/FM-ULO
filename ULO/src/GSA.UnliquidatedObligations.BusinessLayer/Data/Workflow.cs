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
    
    public partial class Workflow
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Workflow()
        {
            this.Documents = new HashSet<Document>();
            this.UnliqudatedObjectsWorkflowQuestions = new HashSet<UnliqudatedObjectsWorkflowQuestion>();
            this.WorkflowHistories = new HashSet<WorkflowHistory>();
        }
    
        public int WorkflowId { get; set; }
        public string WorkflowKey { get; set; }
        public int Version { get; set; }
        public string CurrentWorkflowActivityKey { get; set; }
        public string OwnerUserId { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public System.DateTime CurrentActivityEnteredAtUtc { get; set; }
        public int TargetUloId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnliqudatedObjectsWorkflowQuestion> UnliqudatedObjectsWorkflowQuestions { get; set; }
        public virtual UnliquidatedObligation UnliquidatedObligation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowHistory> WorkflowHistories { get; set; }
    }
}
