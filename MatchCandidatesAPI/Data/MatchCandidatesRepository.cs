using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MatchCandidatesAPI.Data.Entities;

namespace MatchCandidatesAPI.Data
{
    public class MatchCandidatesRepository : IMatchCandidatesRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Uri _baseUri;

        public MatchCandidatesRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _baseUri = new Uri("http://private-anon-2e3c4f5ad2-jobadder1.apiary-proxy.com/");
        }
        public async Task<List<Candidate>> GetAllCandidates()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = _baseUri;
                var response = await client.GetAsync("candidates");

                var result = await response.Content
                    .ReadAsAsync<List<Candidate>>();
                return result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Job>> GetAllJobs()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = _baseUri;
                var response = await client.GetAsync("jobs");

                var result = await response.Content
                    .ReadAsAsync<List<Job>>();
                return result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
