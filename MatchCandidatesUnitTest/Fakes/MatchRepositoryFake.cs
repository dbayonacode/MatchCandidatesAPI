using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MatchCandidatesAPI.Data;
using MatchCandidatesAPI.Data.Entities;


namespace MatchCandidatesUnitTest.Fakes
{
    class MatchRepositoryFake : IMatchCandidatesRepository
    {
        public async Task<List<Candidate>> GetAllCandidates()
        {
            List<Candidate> candidateFake = new List<Candidate>();
            candidateFake.Add(
                new Candidate()
                {
                    CandidateId = 27,
                    Name = "Racquel Rice",
                    SkillTags = "mobile, aws, maturity, pharmaceutical, communication, swift"
                }
            );

            candidateFake.Add(
                new Candidate()
                {
                    CandidateId = 27,
                    Name = "Faustino Alers",
                    SkillTags = "java, objective-c, cooking, reliable, branding"
                }
            );

            return await Task.Run(() => candidateFake);
        }

        public async Task<List<Job>> GetAllJobs()
        {
            List<Job> jobFake = new List<Job>();
            jobFake.Add(
                new Job()
                {
                    JobId = 1,
                    Name = "Mobile Developer",
                    Company = "Uberise",
                    Skills = "mobile, java, swift, objective-c, iOS, xcode, fastlane, aws, kotlin, hockey-app"
                }
            );

            return await Task.Run(() => jobFake);
        }
    }
}
