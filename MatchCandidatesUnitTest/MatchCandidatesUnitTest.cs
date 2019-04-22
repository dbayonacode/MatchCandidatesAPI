using System.Collections.Generic;
using System.Threading.Tasks;
using MatchCandidatesAPI.Controllers;
using MatchCandidatesAPI.ViewModels;
using MatchCandidatesUnitTest.Fakes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatchCandidatesUnitTest
{

    [TestClass]
    public class MatchCandidatesUnitTest
    {
        private CandidatesController _controller;
        private MatchRepositoryFake _service;

        public MatchCandidatesUnitTest()
        {
            _service = new MatchRepositoryFake();
            _controller = new CandidatesController(_service);
        }
        [TestMethod]
        public async Task TestCandidateControllerResponse()
        {
            int jobId = 1;
            int numberCandidatesToReturn = 1;
            string relevance = "quantity";
            int resultExpected = 1;


            var okResult = await _controller.Get(jobId, numberCandidatesToReturn, relevance);
            var listOfMostQualifiedCandidates = ((ObjectResult)((ActionResult<List<CandidateViewModel>>)(okResult)).Result).Value as List<CandidateViewModel>;

            Assert.AreEqual(resultExpected, listOfMostQualifiedCandidates.Count);
        }

        [TestMethod]
        public async Task TestQuantityLogic()
        {
            int jobId = 1;
            int numberCandidatesToReturn = 1;
            string relevance = "quantity";
            string resultExpected = "Racquel Rice";


            var okResult = await _controller.Get(jobId, numberCandidatesToReturn, relevance);
            var listOfMostQualifiedCandidates = ((ObjectResult)((ActionResult<List<CandidateViewModel>>)(okResult)).Result).Value as List<CandidateViewModel>;

            Assert.AreEqual(resultExpected, listOfMostQualifiedCandidates[0].Name);
        }


        [TestMethod]
        public async Task TestQualityLogic()
        {
            int jobId = 1;
            int numberCandidatesToReturn = 1;
            string relevance = "quality";
            string resultExpected = "Faustino Alers";

            var okResult = await _controller.Get(jobId, numberCandidatesToReturn, relevance);
            var listOfMostQualifiedCandidates = ((ObjectResult)((ActionResult<List<CandidateViewModel>>)(okResult)).Result).Value as List<CandidateViewModel>;

            Assert.AreEqual(resultExpected, listOfMostQualifiedCandidates[0].Name);
        }
    }

}
