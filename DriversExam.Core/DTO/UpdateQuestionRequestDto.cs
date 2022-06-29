namespace DriversExam.Core.DTO;

public class UpdateQuestionRequestDto
{
    public int Id { get; set; }
    public String? Content { get; set; }
    public String? CorrectAnswer { get; set; }
    public String? ImageUrl { get; set; }
    public IEnumerable<String>? Answers { get; set; }

    public UpdateQuestionRequestDto(int id, string? content, string? correctAnswer, string? imageUrl, IEnumerable<string>? answers)
    {
        Id = id;
        Content = content;
        CorrectAnswer = correctAnswer;
        ImageUrl = imageUrl;
        Answers = answers;
    }
}