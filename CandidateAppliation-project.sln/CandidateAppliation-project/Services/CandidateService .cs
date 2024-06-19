using API.Contract;
using API.DomianModels;
using CandidateApplication.DomianModels;

namespace CandidateApplication.Services
{
    public interface ICandidateResponseService
    {
        Task<Candidate> SubmitdataAsync(CandidateDTO responseDto);
        Task<Candidate> GetresponseByIdAsync(string id);
    }


    public class CandidateResponseService : ICandidateResponseService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public CandidateResponseService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<Candidate> SubmitdataAsync(CandidateDTO responseDto)
        {
            var response = new Candidate
            {
                Id = Guid.NewGuid().ToString(),
                
                CandidateId = responseDto.CandidateId,
                FirstName=responseDto.FirstName,
                Gender=responseDto.Gender,
                LastName=responseDto.LastName,
                Nationality=responseDto.Nationality,
                PhoneNumber=responseDto.PhoneNumber,
                ProgramTitle=responseDto.ProgramTitle,
                ProgramDescription=responseDto.ProgramDescription,
                Email=responseDto.Email,
                Responses = responseDto.Responses.Select(r => new QuestionResponse
                {
                    QuestionId = r.QuestionId,
                    Response = r.Response
                }).ToList()
            };
            return await _cosmosDbService.AddresponseAsync(response);
        }

        public async Task<Candidate> GetresponseByIdAsync(string id)
        {
            return await _cosmosDbService.GetresponseAsync(id);
        }
    }

}
