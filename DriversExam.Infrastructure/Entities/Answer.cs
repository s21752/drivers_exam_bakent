namespace DriversExam.Infrastructure.Entities;

public class Answer : BaseEntity
{
    public String Content { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}