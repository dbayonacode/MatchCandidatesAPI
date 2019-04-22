using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchCandidatesAPI.Data.Entities;

namespace MatchCandidatesAPI.Data
{
    public interface IMatchCandidatesRepository
    {
        Task<List<Candidate>> GetAllCandidates();
        Task<List<Job>> GetAllJobs();
    }
}
