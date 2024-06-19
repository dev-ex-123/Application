using CandidateApplication.DomianModels.Enums;

namespace CandidateApplication.Contract
{
    public class QuestionDTO
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public QuestionType Type { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsInternal { get; set; }
        public bool Hide { get; set; }
        public List<string> Options { get; set; }
    }



}
