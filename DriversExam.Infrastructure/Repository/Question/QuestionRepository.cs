using DriversExam.Infrastructure.Context;
using DriversExam.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DriversExam.Infrastructure.Repository.Question;

public class QuestionRepository : IQuestionRepository
{
    private readonly MainContext _mainContext;

    public QuestionRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Entities.Question>> GetAllAsync()
    {
        var questions = await _mainContext.Question.ToListAsync();

        foreach (var question in questions)
        {
            await _mainContext.Entry(question).Collection(x => x.Answers).LoadAsync();
            await _mainContext.Entry(question).Reference(x => x.CorrectAnswer).LoadAsync();
            await _mainContext.Entry(question).Reference(x => x.Image).LoadAsync();
        }

        return questions;
    }

    public async Task<Entities.Question> GetByIdAsync(int id)
    {
        var question = await _mainContext.Question.SingleOrDefaultAsync(x => x.Id == id);

        if (question != null)
        {
            await _mainContext.Entry(question).Collection(x => x.Answers).LoadAsync();
            await _mainContext.Entry(question).Reference(x => x.CorrectAnswer).LoadAsync();
            await _mainContext.Entry(question).Reference(x => x.Image).LoadAsync();

            return question;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Entities.Question entity)
    {
        var alreadyExistingQuestion = await _mainContext.Question.SingleOrDefaultAsync(x => x.Content == entity.Content);

        if (alreadyExistingQuestion != null)
        {
            throw new EntityAlreadyExistingException();
        }
        
        entity.DateOfCreation = DateTime.Now;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Entities.Question entity)
    {
        var questionToUpdate = await _mainContext.Question.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (questionToUpdate == null)
        {
            throw new EntityNotFoundException();
        }
        
        questionToUpdate.Answers = entity.Answers;
        questionToUpdate.Content = entity.Content;
        questionToUpdate.Image = entity.Image;
        questionToUpdate.CorrectAnswer = entity.CorrectAnswer;
        questionToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var question = await _mainContext.Question.SingleOrDefaultAsync(x => x.Id == id);
        if (question != null)
        {
            _mainContext.Question.Remove(question);
            await _mainContext.SaveChangesAsync();
            return;
        }

        throw new EntityNotFoundException();
    }
}