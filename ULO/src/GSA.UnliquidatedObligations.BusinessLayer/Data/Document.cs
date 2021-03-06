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
    
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            this.Attachments = new HashSet<Attachment>();
            this.DocumentDocumentTypes = new HashSet<DocumentDocumentType>();
        }
    
        public int DocumentId { get; set; }
        public int WorkflowId { get; set; }
        public string UploadedByUserId { get; set; }
        public System.DateTime CreatedAtUtc { get; set; }
        public string DocumentName { get; set; }
        public string DeletedByUserId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentDocumentType> DocumentDocumentTypes { get; set; }
        public virtual Workflow Workflow { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
