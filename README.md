# MatchCandidatesAPI

This project was generated with Microsoft Visual Studio 2017. It is a web API created with ASP.NET Core 2.1 

The API has two end points:

api/jobs: Get the list of jobs from JobAdder API

api/candidates: Get the most qualified candidate(s) for a job

To test the endpoints swagger, an open-source framework has been added to the project. 
It provides a UI for describing, producing, consuming, and visualizing the RESTful endpoints.

## Run Swagger

Run the project (F5 key)

Then the Swagger UI can be found at https://localhost:44327/swagger

## Run

Use one of the following methods to run your program.

Choose the F5 key.

On the toolbar, choose the IIS Express Debugging button

## Running unit tests

Open Test Explorer by choosing Test > Windows > Test Explorer from the top menu bar.

Run your unit tests by clicking Run All.

After the tests have completed, a green check mark indicates that a test passed. A red "x" icon indicates that a test failed.

## Logic to match a candidate to a job.

In order to match a candidate to a job an algorithm was created, the algorithm:

1.	Assigns a weight to each of the job’s skills sorted by relevance

2.	Iterates through the list of skills required by the job and get a distinct list of candidates that have at least one of the skills required.

3.	Select candidates matching at least one of the job skills

4.	Recruiter is given two choices of algorithms:

-  Quality:  A higher score for candidate skills matching the most relevant job skills

- Quantity: A higher score for candidate skills matching the bigger amount of relevant job skills, scored by candidate’s skill strengths.

5.	In Quality algorithm, the top skill of the candidate is scored to 1 and the following skills will get 50% less of the weight of the previous skill.

In Quantity algorithm, the 50% value changes to 20%. 

In Quality algorithm we boost the top skills of the candidate while promoting the number of matching skills in the quantity algorithm.

6.	The candidate final score for a job is the sum of each candidate skills weight multiplied by the job skill weight that match.

7.	Return the final list of candidate(s) sorted by descending weight.
