using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CandidateTracker.Data;
using Microsoft.AspNetCore.Mvc;
using CandidateTracker.Web.Models;
using Microsoft.Extensions.Configuration;

namespace CandidateTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            ViewBag.Page = PageType.Home;
            return View();
        }

        public IActionResult AddCandidate()
        {
            ViewBag.Page = PageType.Add;
            return View();
        }

        [HttpPost]
        public IActionResult AddCandidate(Candidate candidate)
        {
            var manager = new CandidateRepository(_connectionString);
            manager.AddCandidate(candidate);
            return Redirect("/home/pending");
        }

        public IActionResult Pending()
        {
            ViewBag.Page = PageType.Pending;
            var manager = new CandidateRepository(_connectionString);
            return View(new CandidatesViewModel { Candidates = manager.GetCandidates(Status.Pending) });
        }

        public IActionResult Details(int id)
        {
            ViewBag.Page = PageType.Pending;
            var manager = new CandidateRepository(_connectionString);
            return View(new CandidateViewModel { Candidate = manager.GetCandidate(id) });
        }

        [HttpPost]
        public void UpdateStatus(int id, Status status)
        {
            var manager = new CandidateRepository(_connectionString);
            manager.UpdateStatus(id, status);
        }

        public IActionResult GetCounts()
        {
            var manager = new CandidateRepository(_connectionString);
            return Json(manager.GetCounts());
        }

        public IActionResult Confirmed()
        {
            ViewBag.Page = PageType.Confirmed;
            var manager = new CandidateRepository(_connectionString);
            return View("Completed", new CandidatesViewModel
            {
                Candidates = manager.GetCandidates(Status.Confirmed),
                Type = "Confirmed"
            });
        }

        public IActionResult Refused()
        {
            ViewBag.Page = PageType.Refused;
            var manager = new CandidateRepository(_connectionString);
            return View("Completed", new CandidatesViewModel
            {
                Candidates = manager.GetCandidates(Status.Refused),
                Type = "Refused"
            });
        }

    }
}
