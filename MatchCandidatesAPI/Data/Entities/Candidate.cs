using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchCandidatesAPI.Data.Entities
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string SkillTags { set; get; }
    }
}
