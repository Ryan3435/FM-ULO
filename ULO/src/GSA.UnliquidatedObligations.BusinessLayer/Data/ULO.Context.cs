﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;

    public partial class ULODBEntities : DbContext
    {
        public ULODBEntities()
            : base("name=ULODBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<RequestForReassignment> RequestForReassignments { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UnliqudatedObjectsWorkflowQuestion> UnliqudatedObjectsWorkflowQuestions { get; set; }
        public virtual DbSet<UnliquidatedObligation> UnliquidatedObligations { get; set; }
        public virtual DbSet<UserUser> UserUsers { get; set; }
        public virtual DbSet<WorkflowDefinition> WorkflowDefinitions { get; set; }
        public virtual DbSet<WorkflowHistory> WorkflowHistories { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<AspnetUserApplicationPermissionClaim> AspnetUserApplicationPermissionClaims { get; set; }
        public virtual DbSet<AspnetUserSubjectCategoryClaim> AspnetUserSubjectCategoryClaims { get; set; }
        public virtual DbSet<ReportDefinition> ReportDefinitions { get; set; }
    
        public virtual int CreateULOAndAssignWf(Nullable<int> reviewId, Nullable<int> workflowDefinitionId, Nullable<System.DateTime> reviewDate)
        {
            var reviewIdParameter = reviewId.HasValue ?
                new ObjectParameter("reviewId", reviewId) :
                new ObjectParameter("reviewId", typeof(int));
    
            var workflowDefinitionIdParameter = workflowDefinitionId.HasValue ?
                new ObjectParameter("workflowDefinitionId", workflowDefinitionId) :
                new ObjectParameter("workflowDefinitionId", typeof(int));
    
            var reviewDateParameter = reviewDate.HasValue ?
                new ObjectParameter("reviewDate", reviewDate) :
                new ObjectParameter("reviewDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateULOAndAssignWf", reviewIdParameter, workflowDefinitionIdParameter, reviewDateParameter);
        }
    
        public virtual int GetNextLevelOwnerId(string proposedOwnerId, Nullable<int> workflowId, string nextActivityKey, ObjectParameter nextOwnerId)
        {
            var proposedOwnerIdParameter = proposedOwnerId != null ?
                new ObjectParameter("proposedOwnerId", proposedOwnerId) :
                new ObjectParameter("proposedOwnerId", typeof(string));
    
            var workflowIdParameter = workflowId.HasValue ?
                new ObjectParameter("workflowId", workflowId) :
                new ObjectParameter("workflowId", typeof(int));
    
            var nextActivityKeyParameter = nextActivityKey != null ?
                new ObjectParameter("nextActivityKey", nextActivityKey) :
                new ObjectParameter("nextActivityKey", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetNextLevelOwnerId", proposedOwnerIdParameter, workflowIdParameter, nextActivityKeyParameter, nextOwnerId);
        }
    
        public virtual int DoSomething(Nullable<int> regionId)
        {
            var regionIdParameter = regionId.HasValue ?
                new ObjectParameter("regionId", regionId) :
                new ObjectParameter("regionId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DoSomething", regionIdParameter);
        }
    }
}
