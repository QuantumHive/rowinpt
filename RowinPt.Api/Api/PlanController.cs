using AlperAslanApps.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RowinPt.Contract.Commands.Plan;
using RowinPt.Contract.Models;
using RowinPt.Contract.Queries.Plan;
using System;
using System.Collections.Generic;

namespace RowinPt.Api.Api
{
    [Authorize]
    [Route("plan")]
    public class PlanController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandHandler<PlanNewScheduleItemForCustomerCommand> _planHandler;
        private readonly IUserContext _userContext;

        public PlanController(
            IQueryProcessor queryProcessor,
            ICommandHandler<PlanNewScheduleItemForCustomerCommand> planHandler,
            IUserContext userContext)
        {
            _queryProcessor = queryProcessor;
            _planHandler = planHandler;
            _userContext = userContext;
        }

        [HttpGet]
        public PlanOverview GetPlanOverview()
        {
            var currentUserId = _userContext.GetId();
            var query = new GetPlanOverviewForUserQuery(currentUserId);
            return _queryProcessor.Process(query);
        }

        [HttpGet("dates")]
        public IEnumerable<PlanDate> GetPlanDates(Guid courseId, Guid locationId)
        {
            var query = new GetPlanDatesForCourseQuery(courseId, locationId);
            return _queryProcessor.Process(query);
        }

        [HttpGet("times")]
        public IEnumerable<PlanTime> GetPlanTimes(Guid courseId, Guid locationId, DateTime date)
        {
            var query = new GetPlanTimesForCoursePlanDateQuery(locationId, courseId, date);
            return _queryProcessor.Process(query);
        }

        [HttpPost("{scheduleItemId}")]
        public IActionResult PlanSchedule(Guid scheduleItemId)
        {
            var userId = _userContext.GetId();
            var command = new PlanNewScheduleItemForCustomerCommand(userId, scheduleItemId);
            _planHandler.Handle(command);
            return NoContent();
        }
    }
}
