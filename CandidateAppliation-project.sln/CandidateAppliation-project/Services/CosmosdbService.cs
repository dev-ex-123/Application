using Microsoft.Azure.Cosmos;
using CandidateApplication.DomianModels;
using API.DomianModels;

namespace CandidateApplication.Services
{
    public interface ICosmosDbService
    {
        Task<Question> AddquestionAsync(Question question);
        Task<Question> GetquestionAsync(string id);
        Task<IEnumerable<Question>> GetquestionsAsync(string applicationId);
        Task<Question> UpdatequestionAsync(string id, Question question);
        Task<bool> DeletequestionAsync(string id);
        Task<Candidate> AddresponseAsync(Candidate response);
        Task<Candidate> GetresponseAsync(string id);
    }


    public class CosmosdbService : ICosmosDbService
    {
        private readonly Container _questionContainer;
        private readonly Container _responseContainer;

        public CosmosdbService(CosmosClient cosmosClient, string databaseName, string questionContainerName, string responseContainerName)
        {
            _questionContainer = cosmosClient.GetContainer(databaseName, questionContainerName);
            _responseContainer = cosmosClient.GetContainer(databaseName, responseContainerName);
        }

        public async Task<Question> AddquestionAsync(Question question)
        {
            var response = await _questionContainer.CreateItemAsync(question, new PartitionKey(question.ApplicationId));
            return response.Resource;
        }

        public async Task<Question> GetquestionAsync(string id)
        {

            var response = await _questionContainer.ReadItemAsync<Question>(id, new PartitionKey(id));
            return response.Resource;

        }

        public async Task<IEnumerable<Question>> GetquestionsAsync(string applicationId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.ApplicationId = @applicationId")
                .WithParameter("@applicationId", applicationId);
            var iterator = _questionContainer.GetItemQueryIterator<Question>(query);
            var results = new List<Question>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Question> UpdatequestionAsync(string id, Question question)
        {
            var response = await _questionContainer.UpsertItemAsync(question, new PartitionKey(id));
            return response.Resource;
        }

        public async Task<bool> DeletequestionAsync(string id)
        {

            await _questionContainer.DeleteItemAsync<Question>(id, new PartitionKey(id));
            return true;

        }

        public async Task<Candidate> AddresponseAsync(Candidate response)
        {
            var responseResult = await _responseContainer.CreateItemAsync(response, new PartitionKey(response.CandidateId));
            return responseResult.Resource;
        }

        public async Task<Candidate> GetresponseAsync(string id)
        {
            var response = await _responseContainer.ReadItemAsync<Candidate>(id, new PartitionKey(id));
            return response.Resource;

        }
    }
}

