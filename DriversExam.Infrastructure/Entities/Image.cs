namespace DriversExam.Infrastructure.Entities;

public class Image: BaseEntity
{
    public byte[]? Data { get; set; }
    public String? ImageUrl { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}