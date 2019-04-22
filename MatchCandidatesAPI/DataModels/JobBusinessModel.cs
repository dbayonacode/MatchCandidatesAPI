using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatchCandidatesAPI.Data;
using MatchCandidatesAPI.Data.Entities;
using MatchCandidatesAPI.ViewModels;

namespace MatchCandidatesAPI.DataModels
{
    public class JobBusinessModel
    {
        private readonly IMatchCandidatesRepository _matchCandidatesRepository;
        private readonly IMapper _mapper;

        public JobBusinessModel(IMatchCandidatesRepository matchCandidatesRepository, IMapper mapper)
        {
            _matchCandidatesRepository = matchCandidatesRepository;
            _mapper = mapper;
        }

        public async Task<List<JobViewModel>> GetAllJobs()
        {
            try
            {
                var jobs = await _matchCandidatesRepository.GetAllJobs();
                var jobsFormatted = _mapper.Map<List<Job>, List<JobViewModel>>(jobs);
                return jobsFormatted.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
