using CandidateApplication.Contract;
using CandidateApplication.DomianModels;

namespace CandidateApplication.Services
{
    public interface IQuestionService
    {
        Task<Question> CreatequestionAsync(QuestionDTO questionDto);
        Task<Question> GetQuestionByIdAsync(string id);
        Task<Question> UpdatequestionAsync(string id, QuestionDTO questionDto);
        Task<bool> DeleteQuestionAsync(string id);
        Task<IEnumerable<Question>> GetQuestionsasync(string applicationId);
    }

    public class QuestionService : IQuestionService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public QuestionService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<Question> CreatequestionAsync(QuestionDTO questionDto)
        {
            var question = new Question
            {
                Id = Guid.NewGuid().ToString(),
                Label = questionDto.Label,
                Type = questionDto.Type,
                IsMandatory = questionDto.IsMandatory,
                IsInternal = questionDto.IsInternal,
                Hide = questionDto.Hide,
                Options = questionDto.Options,
                ApplicationId = "ApplicationId"
            };
            return await _cosmosDbService.AddquestionAsync(question);
        }

        public async Task<Question> GetQuestionByIdAsync(string id)
        {
            return await _cosmosDbService.GetquestionAsync(id);
        }
        public async Task<IEnumerable<Question>> GetQuestionsasync(string applicationId)
        {
            return await _cosmosDbService.GetquestionsAsync(applicationId);
        }

        public async Task<Question> UpdatequestionAsync(string id, QuestionDTO questionDto)
        {
            var question = await _cosmosDbService.GetquestionAsync(id);
            if (question == null) return null;

            question.Label = questionDto.Label;
            question.Type = questionDto.Type;
            question.IsMandatory = questionDto.IsMandatory;
            question.IsInternal = questionDto.IsInternal;
            question.Hide = questionDto.Hide;
            question.Options = questionDto.Options;

            return await _cosmosDbService.UpdatequestionAsync(id, question);
        }

        public async Task<bool> DeleteQuestionAsync(string id)
        {
            return await _cosmosDbService.DeletequestionAsync(id);
        }
    }

}
