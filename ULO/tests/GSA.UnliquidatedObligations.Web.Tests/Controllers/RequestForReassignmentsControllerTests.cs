﻿using GSA.UnliquidatedObligations.Web.Controllers;
using GSA.UnliquidatedObligations.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevolutionaryStuff.Core.Caching;
using System.Web.Mvc;

namespace GSA.UnliquidatedObligations.Web.Tests.Controllers
{
    //TODO: Need to add more tests
    [TestClass]
    public class RequestForReassignmentsControllerTests : ControllerTests
    {

        private RequestForReassignmentsController Controller;
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            Controller = new RequestForReassignmentsController(WorkflowManager, ApplicationUserManager, DbContext, ComponentContext, Cache.DataCacher)
            {
                ControllerContext = ControllerContext
            };
        }

        [TestMethod]
        public void Details_returns_view_with_correct_information()
        {
            var view = Controller.Details(RequestedForreassignmentId, WorkflowId, 1) as PartialViewResult;
            var returnedModel = (RequestForReassignmentViewModel)view.Model;
            Assert.IsInstanceOfType(returnedModel, typeof(RequestForReassignmentViewModel));
        }
    }
}
