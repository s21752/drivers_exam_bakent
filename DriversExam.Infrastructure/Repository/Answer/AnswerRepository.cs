using DriversExam.Infrastructure.Context;
using DriversExam.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DriversExam.Infrastructure.Repository.Answer;

public class AnswerRepository : IAnswerRepository
{
    private readonly MainContext _mainContext;

    public AnswerRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Entities.Answer>> GetAllAsync()
    {
        var answers = await _mainContext.Answer.ToListAsync();

        foreach (var answer in answers)
        {
            await _mainContext.Entry(answer).Reference(x => x.Question).LoadAsync();
        }

        return answers;
    }

    public async Task<Entities.Answer> GetByIdAsync(int id)
    {
        var answer = await _mainContext.Answer.SingleOrDefaultAsync(x =>x.Id == id);

        if (answer != null)
        {
            await _mainContext.Entry(answer).Reference(x => x.Question).LoadAsync();
            return answer;
        }

        throw new EntityNotFoundException();
    }

    // not checking if answer already exists, because many questions can have same answer
    public async Task AddAsync(Entities.Answer entity)
    {
        entity.DateOfCreation = DateTime.Now;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Entities.Answer entity)
    {
        var answerToUpdate = await _mainContext.Answer.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (answerToUpdate == null)
        {
            throw new EntityNotFoundException();
        }

        answerToUpdate.Content = entity.Content;
        answerToUpdate.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var answerToDelete = await _mainContext.Answer.SingleOrDefaultAsync(x => x.Id == id);

        if (answerToDelete != null)
        {
            _mainContext.Answer.Remove(answerToDelete);
            await _mainContext.SaveChangesAsync();
        }

        throw new EntityNotFoundException();
    }

    public async Task<Entities.Answer> CreateAndGetAsync(Entities.Answer entity)
    {
        entity.DateOfCreation = DateTime.UtcNow;
        entity.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();

        return entity;
    }
}