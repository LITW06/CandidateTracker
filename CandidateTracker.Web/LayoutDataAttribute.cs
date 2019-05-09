using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace CandidateTracker.Web
{
    public class LayoutDataAttribute : ActionFilterAttribute
    {
        private string _connectionString;

        public LayoutDataAttribute(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var repo = new CandidateRepository(_connectionString);
            var controller = (Controller) filterContext.Controller;
            controller.ViewBag.CandidateCounts = repo.GetCounts();
            base.OnActionExecuting(filterContext);
        }
    }
}
