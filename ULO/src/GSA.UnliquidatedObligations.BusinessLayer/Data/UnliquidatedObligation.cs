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
    
    public partial class UnliquidatedObligation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UnliquidatedObligation()
        {
            this.Notes = new HashSet<Note>();
            this.Workflows = new HashSet<Workflow>();
            this.WorkflowHistories = new HashSet<WorkflowHistory>();
        }
    
        public int UloId { get; set; }
        public Nullable<int> RegionId { get; set; }
        public string DocType { get; set; }
        public string PegasusDocumentNumber { get; set; }
        public string PegasysTitleNumber { get; set; }
        public string Fund { get; set; }
        public string Organization { get; set; }
        public string Prog { get; set; }
        public string Agreement { get; set; }
        public string RWACompletionDate { get; set; }
        public string DateOfLastActivity { get; set; }
        public string ActgPd { get; set; }
        public string TotalLine { get; set; }
        public string Commitments { get; set; }
        public string Prepayments { get; set; }
        public string UndelOrders { get; set; }
        public string Rcpt { get; set; }
        public string Accrual { get; set; }
        public string TotalDO { get; set; }
        public string TotalUDOAndDO { get; set; }
        public string PendPayments { get; set; }
        public string PymtsInTransit { get; set; }
        public string PymtsConfirmed { get; set; }
        public string Holdbacks { get; set; }
        public string PegasysContractNumber { get; set; }
        public string PegasysVendorName { get; set; }
        public string ContractingOfficersName { get; set; }
        public string AwardNbr { get; set; }
        public string BaseContractNbr { get; set; }
        public string GSAFSSOtherNumber { get; set; }
        public string AwdExpirationDate { get; set; }
        public string ReasonIncludedInReview { get; set; }
        public bool Valid { get; set; }
        public string ReviewedBy { get; set; }
        public string ExpectedDateForCompletionResolution { get; set; }
        public string RegionalSupportOrJustification { get; set; }
        public string BGPCommentsAfterReview { get; set; }
        public string RegionalResponse { get; set; }
        public string BGPFinalDetermination { get; set; }
        public string C1stReview { get; set; }
        public string C2ndReview { get; set; }
        public string DateSentToFinCntrForDeobligation { get; set; }
        public string ULOCategoryForTracker { get; set; }
        public string When_should_BGP_review_again_ { get; set; }
        public string DateBalanceRestored { get; set; }
        public string AmtToBeRecovered { get; set; }
        public string C1stFollowupDate { get; set; }
        public string C2ndFollowupDate { get; set; }
        public string NoResponse_w_initial_submittal_ { get; set; }
        public string NoResponse { get; set; }
        public string BGPMadeCallValid { get; set; }
        public string BGPMadeCallInvalid { get; set; }
        public string BGPFinalDetVariesFromRegionCO { get; set; }
        public string Column_52 { get; set; }
        public string Column_53 { get; set; }
        public string Status { get; set; }
        public string Column_55 { get; set; }
        public System.DateTime CreatedAtUTC { get; set; }
        public Nullable<decimal> DOShouldBe { get; set; }
        public Nullable<decimal> UDOShouldBe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Note> Notes { get; set; }
        public virtual Region Region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workflow> Workflows { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkflowHistory> WorkflowHistories { get; set; }
    }
}
