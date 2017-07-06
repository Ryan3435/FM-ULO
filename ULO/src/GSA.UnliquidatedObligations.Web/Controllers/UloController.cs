﻿using Autofac;
using GSA.UnliquidatedObligations.BusinessLayer.Authorization;
using GSA.UnliquidatedObligations.BusinessLayer.Data;
using GSA.UnliquidatedObligations.BusinessLayer.Workflow;
using GSA.UnliquidatedObligations.Web.Models;
using GSA.UnliquidatedObligations.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GSA.UnliquidatedObligations.Web.Controllers
{
    [Authorize]
    //[ApplicationPermissionAuthorize(ApplicationPermissionNames.ApplicationUser)]
    public class UloController : BaseController
    {
        protected readonly IWorkflowManager Manager;
        private readonly ApplicationUserManager UserManager;


        public UloController(IWorkflowManager manager, ApplicationUserManager userManager, ULODBEntities db, IComponentContext componentContext)
            : base(db, componentContext)
        {
            Manager = manager;
            UserManager = userManager;
        }

        // GET: Ulo

        public async Task<ActionResult> Index(string sortCol, string sortDir, int? page, int? pageSize)
        {
            //TODO: wrire stored procedure for nested groups
            //TODO: Due dates: calculate in model or add additional column in workflow table (ExpectedActivityDurationInSeconds, nullable, DueAt = null) 
            var currentUser = await UserManager.FindByNameAsync(this.User.Identity.Name);
            var userIds = await GetUsersGroupsAsync(currentUser.Id);
            userIds.Add(currentUser.Id);
            var workflows = ApplyBrowse(
                DB.Workflows.Where(wf => userIds.Contains(wf.OwnerUserId)).Include(wf => wf.UnliquidatedObligation),
                sortCol ?? nameof(Workflow.DueAtUtc), sortDir, page, pageSize);
            return View(workflows);
        }

        [ApplicationPermissionAuthorize(ApplicationPermissionNames.CanViewOtherWorkflows)]
        [Route("Ulo/RegionWorkflows")]
        public async Task<ActionResult> RegionWorkflows(int? uloId, string pegasysDocumentNumber, string organization, int? region, int? zone, string fund, string baCode, string pegasysTitleNumber, string pegasysVendorName, string docType, string contractingOfficersName, string currentlyAssignedTo, string hasBeenAssignedTo, string awardNumber, string reasonIncludedInReview, bool? valid, string status, int? reviewId,
            string sortCol = null, string sortDir = null, int? page = null, int? pageSize = null)
        {
            //var currentUser = await UserManager.FindByNameAsync(this.User.Identity.Name);
            var user = DB.AspNetUsers.FirstOrDefault(u => u.UserName == this.User.Identity.Name);
            var claimRegionIds = user.GetApplicationPerimissionRegions(ApplicationPermissionNames.CanViewOtherWorkflows);
            var wfPredicate =
                PredicateBuilder.Create<Workflow>(
                    wf => claimRegionIds.Contains((int)wf.UnliquidatedObligation.RegionId)
                          && wf.OwnerUserId != user.Id);


            wfPredicate = wfPredicate.GenerateWorkflowPredicate(uloId, pegasysDocumentNumber, organization, region, zone, fund,
              baCode, pegasysTitleNumber, pegasysVendorName, docType, contractingOfficersName, currentlyAssignedTo, hasBeenAssignedTo, awardNumber, reasonIncludedInReview, valid, status, reviewId);


            var workflows = await ApplyBrowse(
                DB.Workflows.Where(wfPredicate).Include(wf => wf.UnliquidatedObligation).Include(wf => wf.UnliquidatedObligation.Region).Include(wf => wf.UnliquidatedObligation.Region.Zone),
                sortCol ?? nameof(Workflow.DueAtUtc), sortDir, page, pageSize).ToListAsync();

            var allSubjectCategoryClaimsValues =
                Enum.GetValues(typeof(SubjectCatagoryNames))
                    .Cast<SubjectCatagoryNames>()
                    .Select(scc => scc.GetDisplayName())
                    .OrderBy(scc => scc)
                    .ToList();

            var zones = DB.Zones.Select(z => new SelectListItem { Text = z.ZoneName, Value = z.ZoneId.ToString() }).OrderBy(z => z).ToList();
            var regions = DB.Regions.Select(r => new SelectListItem { Text = r.RegionName, Value = r.RegionId.ToString() }).OrderBy(r => r).ToList();
            var baCodes = DB.UnliquidatedObligations.Select(u => u.Prog).Distinct().OrderBy(p => p).ToList();

            var workflowDescXML = await DB.WorkflowDefinitions.Where(wfd => wfd.WorkflowDefinitionName == "ULO Workflow" && wfd.IsActive == true).Select(wfd => wfd.DescriptionXml).FirstOrDefaultAsync();
            List<string> statuses = new List<string>();
            if (workflowDescXML != null)
            {
                var workflowDesc = WorkflowDescription.DeserializeFromXml(workflowDescXML);
                statuses = workflowDesc.WebActionWorkflowActivities.OrderBy(a => a.SequenceNumber).Select(a => a.ActivityName).ToList();
            }

            return View("~/Views/Ulo/Search/Index.cshtml", new FilterViewModel(workflows, allSubjectCategoryClaimsValues, zones, regions, baCodes, statuses));
        }


        public async Task<ActionResult> Details(int uloId, int workflowId)
        {
            //TODO: check if current user is able to view
            var ulo = await DB.UnliquidatedObligations.Include(u => u.Notes).FirstOrDefaultAsync(u => u.UloId == uloId);

            var comingFromReassignmentsPage = isReassignmentReferral();

            var workflow = await FindWorkflowAsync(workflowId, checkReassignmentsGroup: comingFromReassignmentsPage);
            var workflowDesc = await FindWorkflowDescAsync(workflow);

            return View("Details/Index", new UloViewModel(ulo, workflow, workflowDesc, true));
        }

        private bool isReassignmentReferral()
        {
            return Request.UrlReferrer.LocalPath == "/RequestForReassignments";
        }

        public async Task<ActionResult> RegionWorkflowDetails(int uloId, int workflowId)
        {
            //TODO: check if current user is able to view
            var currentUser = await UserManager.FindByNameAsync(this.User.Identity.Name);
            var ulo = await DB.UnliquidatedObligations.Include(u => u.Notes).FirstOrDefaultAsync(u => u.UloId == uloId);
            var workflow = await FindWorkflowAsync(workflowId, false);
            var workflowDesc = await FindWorkflowDescAsync(workflow);
            var workflowAssignedToCurrentUser = currentUser.Id == workflow.OwnerUserId;
            return View("Details/Index", new UloViewModel(ulo, workflow, workflowDesc, workflowAssignedToCurrentUser));
        }

        private async Task<IWorkflowDescription> FindWorkflowDescAsync(Workflow wf)
            => await Manager.GetWorkflowDescriptionAsync(wf);

        //TODO: Move to Manager?
        private async Task<Workflow> FindWorkflowAsync(int workflowId, bool checkOwner = true, bool checkReassignmentsGroup = false)
        {
            var wf = await DB.Workflows.Include(q => q.AspNetUser).Include(q => q.UnliquidatedObligation).FirstOrDefaultAsync(q => q.WorkflowId == workflowId);
            if (wf != null)
            {
                if (checkOwner == false) return wf;
                var currentUser = await UserManager.FindByNameAsync(this.User.Identity.Name);
                var groupsUserBelongsTo = await GetUsersGroupsAsync(currentUser.Id, checkReassignmentsGroup);
                if (currentUser != null)
                {
                    if (wf.OwnerUserId == currentUser.Id || groupsUserBelongsTo.Contains(wf.OwnerUserId)) return wf;
                    if (wf.AspNetUser.UserType == UserTypes.Group.ToString())
                    {
                        //TODO: Write recursive then call recursive sproc to see if current user is in the group
                    }
                }
            }
            return null;
        }

        //Referred to by WebActionWorkflowActivity
        //TODO: Attributes will probably change
        [ActionName("Advance")]
        [Route("Advance/{workflowId}")]
        public async Task<ActionResult> Advance(int workflowId)
        {
            var wf = await FindWorkflowAsync(workflowId);
            if (wf == null) return HttpNotFound();
            return View(new FormAModel(wf));
        }

        //TODO: be able to open either ULO or workflow
        //TODO: Attributes will probably change
        [HttpPost]
        [SubmitButtonSelector(Name = "Advance")]
        public async Task<ActionResult> Advance(
            int workflowId,
            int uloId,
            [Bind(Include = "JustificationId,Answer,ExpectedDateForCompletion,Comments,UnliqudatedWorkflowQuestionsId")]
            AdvanceViewModel advanceModel)
        {
            var wf = await FindWorkflowAsync(workflowId);
            if (wf == null) return HttpNotFound();
            if (ModelState.IsValid)
            {
                var user = await DB.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                var question = new UnliqudatedObjectsWorkflowQuestion
                {
                    JustificationId = advanceModel.JustificationId,
                    UserId = user.Id,
                    Answer = advanceModel.Answer,
                    WorkflowId = workflowId,
                    Comments = advanceModel.Comments,
                    Pending = false,
                    UnliqudatedWorkflowQuestionsId = advanceModel.UnliqudatedWorkflowQuestionsId,
                    WorkflowRowVersion = wf.WorkflowRowVersion,
                    CreatedAtUtc = DateTime.UtcNow
                };
                wf.UnliquidatedObligation.ExpectedDateForCompletion = advanceModel.ExpectedDateForCompletion;
                return await AdvanceAsync(wf, question);
            }
            return await Details(uloId, workflowId);
        }

        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        public async Task<ActionResult> SaveQuestion(int workflowId, int uloId,
            [Bind(Include = "JustificationId,Answer,Comments,ExpectedDateForCompletion,UnliqudatedWorkflowQuestionsId")] AdvanceViewModel advanceModel)
        {
            var wf = await FindWorkflowAsync(workflowId);
            if (wf == null) return HttpNotFound();
            var user = await DB.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var question = new UnliqudatedObjectsWorkflowQuestion
            {
                JustificationId = advanceModel.JustificationId,
                UserId = user.Id,
                Answer = advanceModel.Answer,
                WorkflowId = workflowId,
                Comments = advanceModel.Comments,
                Pending = true,
                UnliqudatedWorkflowQuestionsId = advanceModel.UnliqudatedWorkflowQuestionsId,
                WorkflowRowVersion = wf.WorkflowRowVersion,
                CreatedAtUtc = DateTime.UtcNow
            };
            wf.UnliquidatedObligation.ExpectedDateForCompletion = advanceModel.ExpectedDateForCompletion;
            await Manager.SaveQuestionAsync(wf, question);
            await DB.SaveChangesAsync();
            return await Details(uloId, workflowId);
        }

        private async Task<List<string>> GetUsersGroupsAsync(string userId, bool includeReassignmentGroup = false)
        {
            if (!includeReassignmentGroup)
            {
                var reassignGroupUser = await UserManager.FindByNameAsync(Properties.Settings.Default.ReassignGroupUserName);
                return await DB.UserUsers.Where(uu => uu.ChildUserId == userId && uu.ParentUserId != reassignGroupUser.Id).Select(uu => uu.ParentUserId).ToListAsync();
            }
            else
            {
                return await DB.UserUsers.Where(uu => uu.ChildUserId == userId).Select(uu => uu.ParentUserId).ToListAsync();
            }
        }

        private async Task<ActionResult> AdvanceAsync(Workflow wf, UnliqudatedObjectsWorkflowQuestion question)
        {
            await Manager.SaveQuestionAsync(wf, question);
            var ret = await Manager.AdvanceAsync(wf, question);
            await DB.SaveChangesAsync();
            return ret;
        }
    }


}