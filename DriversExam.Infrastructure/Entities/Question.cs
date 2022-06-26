namespace DriversExam.Infrastructure.Entities;

public class Question : BaseEntity
{
    public String Content { get; set; }
    public int CorrectAnswerId { get; set; }
    public Answer CorrectAnswer { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    public Image? Image { get; set; }
    public int? ImageId { get; set; }
    
}