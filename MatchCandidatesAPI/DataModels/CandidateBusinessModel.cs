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
    public class CandidateBusinessModel
    {
        private readonly IMatchCandidatesRepository _matchCandidatesRepository;
        private readonly IMapper _mapper;
        public CandidateBusinessModel(IMatchCandidatesRepository matchCandidatesRepository)
        {
            _matchCandidatesRepository = matchCandidatesRepository;
        }
        public async Task<List<CandidateViewModel>> GetMostQualifiedCandidate(int jobId, int numberCandidatesToReturn, string relevance)
        {
            try
            {
                var candidates = await _matchCandidatesRepository.GetAllCandidates();
                var jobs = await _matchCandidatesRepository.GetAllJobs();

                List<JobViewModel> jobsViewmodel = new List<JobViewModel>();
                List<CandidateViewModel> candidatesViewmodel = new List<CandidateViewModel>();

                //Get the job selected by the user
                var jobRequired = jobs.FirstOrDefault(j => j.JobId == jobId);

                //Assign weight to each skill required by the job
                if (jobRequired != null)
                {
                    var listJobOfSKills = jobRequired.Skills.Split(',').ToList();
                    List<SkillViewModel> skillsJobViewModel = new List<SkillViewModel>();
                    int counterJobWeight = listJobOfSKills.Count();


                    foreach (var skill in listJobOfSKills)
                    {
                        skillsJobViewModel.Add(new SkillViewModel()
                        {
                            Name = skill,
                            Weight = counterJobWeight
                        });

                        counterJobWeight = counterJobWeight - 1;
                    }

                    var jobViewModel = new JobViewModel()
                    {
                        JobId = jobRequired.JobId,
                        Name = jobRequired.Name,
                        Company = jobRequired.Company,
                        Skills = skillsJobViewModel

                    };


                    //Get list of candidades that match skill required by the job 
                    List<Candidate> newFilteredCandidateList = new List<Candidate>();

                    foreach (var jobSkill in jobViewModel.Skills)
                    {
                        var candidteFiltered = candidates.Where(c => c.SkillTags.Contains(jobSkill.Name)).ToList();

                        if (candidteFiltered.Count > 0)
                        {
                            foreach (var c in candidteFiltered)
                            {
                                newFilteredCandidateList.Add((Candidate)c);
                            }

                        }

                    }

                    //Remove duplicated candidates from the list
                    List<Candidate> newFilteredCandidateListDistinct = newFilteredCandidateList.Distinct().ToList();

                    foreach (var candidate in newFilteredCandidateListDistinct)
                    {
                        var listOfSKills = candidate.SkillTags.Split(',').ToList();

                        HashSet<string> newListSkillsNoDuplicates = new HashSet<string>();

                        //Leave one single instance of a skill that is duplicated in the list of skills of the candidate.
                        foreach (var i in listOfSKills)
                        {
                            newListSkillsNoDuplicates.Add(i.Trim());
                        }


                        //Assing a weight to each skill of the candidate depending on the relavence that the user(Recruiter) has determined is the most relevant.
                        //The relevance is Quality or Quantity
                        decimal counterWeight = 1;
                        decimal step;

                        switch (relevance)
                        {
                            case "quantity":
                                step = (decimal)0.2;
                                break;


                            default:
                                step = (decimal)0.5;
                                break;
                        }

                        List<SkillViewModel> skillsViewModel = new List<SkillViewModel>();

                        foreach (var skill in newListSkillsNoDuplicates)
                        {
                            skillsViewModel.Add(new SkillViewModel()
                            {
                                Name = skill,
                                Weight = counterWeight
                            });

                            counterWeight = counterWeight - (counterWeight * step);
                        }

                        candidatesViewmodel.Add(new CandidateViewModel()
                        {
                            CandidateId = candidate.CandidateId,
                            Name = candidate.Name,
                            SkillTags = skillsViewModel,
                            TotalWeight = 0

                        });


                    }

                    //Calculate total weight for a candidate skill list
                    foreach (var candidate in candidatesViewmodel)
                    {
                        decimal totalCandidateWeight = 0;

                        foreach (var skillCandidate in candidate.SkillTags)
                        {

                            var matchSkill = jobViewModel.Skills.FirstOrDefault(j => j.Name.Trim() == skillCandidate.Name);

                            if (matchSkill != null)
                            {
                                totalCandidateWeight = totalCandidateWeight + matchSkill.Weight * skillCandidate.Weight;
                            }

                        }

                        candidate.TotalWeight = totalCandidateWeight;

                    }

                    //Return the list of the most suitable candidates (1 , 5 or All candidates) for the job, order by weight - descending
                    switch (numberCandidatesToReturn)
                    {
                        case -1:
                            return candidatesViewmodel.OrderByDescending(i => i.TotalWeight).ToList();

                        default:
                            return candidatesViewmodel.OrderByDescending(i => i.TotalWeight).Take(numberCandidatesToReturn).ToList();

                    }

                }

                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
