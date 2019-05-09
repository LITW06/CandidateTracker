﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateTracker.Data;

namespace CandidateTracker.Web.Models
{
    public class CandidatesViewModel
    {
        public IEnumerable<Candidate> Candidates { get; set; }
        public string Type { get; set; }
    }
}
