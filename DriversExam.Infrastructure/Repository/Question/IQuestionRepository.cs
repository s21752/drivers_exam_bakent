namespace DriversExam.Infrastructure.Repository.Question;

public interface IQuestionRepository : IRepository<Entities.Question>
{
    Task<Entities.Question> GetRandomSingleQuestionAsync();
    Task<IEnumerable<Entities.Question>> GetManyRandomQuestionsAsync(int quantity);

    Task<IEnumerable<Entities.Question>> GetPaginatedQuestionsAsync(int pageSize, int pageIndex);
    Task<int> CountAsync();
}