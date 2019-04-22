using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatchCandidatesAPI.Data;
using MatchCandidatesAPI.DataModels;
using MatchCandidatesAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchCandidatesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobBusinessModel _jobViewModel;
        private readonly IMapper _mapper;
        public JobsController(IMatchCandidatesRepository matchCandidatesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _jobViewModel = new JobBusinessModel(matchCandidatesRepository, _mapper);
        }

        [HttpGet]
        public async Task<ActionResult<List<JobViewModel>>> Get()
        {
            try
            {
                var jobs = await _jobViewModel.GetAllJobs();
                if (jobs != null)
                    return Ok(jobs);

                return BadRequest("Failed to get the list of jobs");

            }
            catch (Exception)
            {
                return BadRequest("Failed to get the list of jobs");
            }

        }

    }
}