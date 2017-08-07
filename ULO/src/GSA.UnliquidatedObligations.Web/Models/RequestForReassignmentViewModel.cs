﻿using GSA.UnliquidatedObligations.BusinessLayer.Data;
using GSA.UnliquidatedObligations.BusinessLayer.Workflow;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GSA.UnliquidatedObligations.Web.Models
{
    public class RequestForReassignmentViewModel
    {
        public int? RequestForReassignmentId { get; set; } 
        public int WorkflowId { get; set; }
        public int RegionId { get; set; }
        public string SuggestedReviewerId { get; set; }

        public IList<SelectListItem> Users { get; set; }

        public string JustificationKey { get; set; }
        public string Comments { get; set; }
        public IList<SelectListItem> Justifications { get; set; }

        public RequestForReassignmentViewModel()
        { }

        public RequestForReassignmentViewModel(string suggestedReviewerId, string justificationKey, int? requestForReassignmentId, string comments, int workflowId, int regionId, List<AspNetUser> users, IList<Justification> justifications)
        {
            RequestForReassignmentId = requestForReassignmentId;
            SuggestedReviewerId = suggestedReviewerId;
            JustificationKey = justificationKey;
            Users = PortalHelpers.CreateSelectList(users);
            Comments = comments;
            Justifications = PortalHelpers.CreateSelectList(justifications);
            WorkflowId = workflowId;
            RegionId = regionId;
        }
    }
}
