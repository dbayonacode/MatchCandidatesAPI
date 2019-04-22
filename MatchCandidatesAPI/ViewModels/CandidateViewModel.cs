using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchCandidatesAPI.ViewModels
{
    public class CandidateViewModel
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public List<SkillViewModel> SkillTags { set; get; }
        public decimal TotalWeight { get; set; }
    }
}
