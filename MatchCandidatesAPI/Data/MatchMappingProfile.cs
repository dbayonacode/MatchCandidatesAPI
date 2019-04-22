using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatchCandidatesAPI.Data.Entities;
using MatchCandidatesAPI.ViewModels;

namespace MatchCandidatesAPI.Data
{
    public class MatchMappingProfile : Profile
    {
        public MatchMappingProfile()
        {
            CreateMap<Job, JobViewModel>()
                .ForMember(m => m.Skills, r => r.MapFrom(s => getSkills(s.Skills)));
        }

        private List<SkillViewModel> getSkills(string skills)
        {
            if (string.IsNullOrEmpty(skills))
            {
                return null;
            }

            var skillsViewModel = skills.Split(',').ToList();

            List<SkillViewModel> skillListViewModel = new List<SkillViewModel>();

            foreach (var s in skillsViewModel)
            {
                skillListViewModel.Add(
                    new SkillViewModel()
                    {
                        Name = s,
                        Weight = 0
                    }

                );

            }
            return skillListViewModel;
        }
    }
}
