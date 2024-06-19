using CandidateApplication.DomianModels.Enums;

namespace API.DomianModels
{
    public class Candidate
    {
        public string Id { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Nationality Nationality { get; set; }
        public Gender Gender { get; set; }
        public string CandidateId { get; set; }
        public List<QuestionResponse> Responses { get; set; }
    }

    public class QuestionResponse
    {
        public string QuestionId { get; set; }
        public string Response { get; set; }
    }

}
