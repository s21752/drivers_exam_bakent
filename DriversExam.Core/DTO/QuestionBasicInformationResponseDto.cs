namespace DriversExam.Core.DTO;

public class QuestionBasicInformationResponseDto
{
    public String Content { get; set; }
    public IEnumerable<String> AllAnswers { get; set; }
    public String CorrectAnswer { get; set; }
    public String? ImageUrl { get; set; }
    
    // needed for question deleting/updating
    public int QuestionId { get; set; }

    public QuestionBasicInformationResponseDto(string content, IEnumerable<string> allAnswers, string correctAnswer, string? imageUrl, int questionId)
    {
        Content = content;
        AllAnswers = allAnswers;
        CorrectAnswer = correctAnswer;
        ImageUrl = imageUrl;
        QuestionId = questionId;
    }
}