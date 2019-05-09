using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CandidateTracker.Data
{
    public class CandidateRepository
    {
        private string _connectionString;

        public CandidateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Candidate> GetCandidates(Status status)
        {
            using (var context = new CandidateTrackerContext(_connectionString))
            {
                return context.Candidates.Where(c => c.Status == status).ToList();
            }
        }

        public void AddCandidate(Candidate candidate)
        {
            using (var context = new CandidateTrackerContext(_connectionString))
            {
                context.Candidates.Add(candidate);
                context.SaveChanges();
            }
        }

        public void UpdateStatus(int candidateId, Status status)
        {
            using (var context = new CandidateTrackerContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand("UPDATE Candidates SET Status = @status WHERE Id = @id", 
                    new SqlParameter("@status", status),
                    new SqlParameter("@id", candidateId));
            }
        }

        public CandidateCounts GetCounts()
        {
            using (var context = new CandidateTrackerContext(_connectionString))
            {
                return new CandidateCounts
                {
                    Confirmed = context.Candidates.Count(c => c.Status == Status.Confirmed),
                    Pending = context.Candidates.Count(c => c.Status == Status.Pending),
                    Refused = context.Candidates.Count(c => c.Status == Status.Refused),
                };

            }
        }

        public Candidate GetCandidate(int id)
        {
            using (var context = new CandidateTrackerContext(_connectionString))
            {
                return context.Candidates.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
