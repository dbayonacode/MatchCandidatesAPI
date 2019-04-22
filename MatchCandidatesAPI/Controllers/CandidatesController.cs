using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchCandidatesAPI.Data;
using MatchCandidatesAPI.DataModels;
using MatchCandidatesAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchCandidatesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly CandidateBusinessModel _candidateViewModel;

        public CandidatesController(IMatchCandidatesRepository matchCandidatesRepository)
        {
            _candidateViewModel = new CandidateBusinessModel(matchCandidatesRepository);
        }

        [HttpGet("{jobId}/{numberCandidatesToReturn}/{relevance}")]
        public async Task<ActionResult<List<CandidateViewModel>>> Get(int jobId, int numberCandidatesToReturn, string relevance)
        {
            try
            {
                var candidates = await _candidateViewModel.GetMostQualifiedCandidate(jobId, numberCandidatesToReturn, relevance);

                if (candidates != null && candidates.Count > 0)
                    return Ok(candidates);

                return BadRequest("Failed to get the list of jobs");
            }
            catch (Exception)
            {
                return BadRequest("Failed to get the list of jobs");
            }
        }
    }
}