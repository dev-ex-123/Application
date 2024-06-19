using CandidateApplication.DomianModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Contract
{
    public class CandidateDTO
    {
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }

        public string CandidateId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Nationality Nationality { get; set; }
        public Gender Gender { get; set; }
        public List<QuestionResponseDTO> Responses { get; set; }
    }

    public class QuestionResponseDTO
    {
        public string QuestionId { get; set; }
        public string Response { get; set; }
    }

}
