namespace DriversExam.Core.DTO;

public class AddNewQuestionRequestDto
{
    public String Content { get; set; }
    public String CorrectAnswer { get; set; }
    public IEnumerable<String> AllAnswers { get; set; }
    public String? ImageUrl { get; set; }
}