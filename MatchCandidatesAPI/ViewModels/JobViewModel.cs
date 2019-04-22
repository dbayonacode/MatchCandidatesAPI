using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchCandidatesAPI.ViewModels
{
    public class JobViewModel
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public List<SkillViewModel> Skills { get; set; }
    }
}
