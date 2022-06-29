namespace DriversExam.Infrastructure.Repository.Answer;

public interface IAnswerRepository : IRepository<Entities.Answer>
{
    Task<Entities.Answer> CreateAndGetAsync(Entities.Answer entity);
}