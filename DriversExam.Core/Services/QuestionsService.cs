using DriversExam.Core.DTO;
using DriversExam.Core.Extensions;
using DriversExam.Infrastructure.Entities;
using DriversExam.Infrastructure.Exceptions;
using DriversExam.Infrastructure.Repository.Answer;
using DriversExam.Infrastructure.Repository.Image;
using DriversExam.Infrastructure.Repository.Question;

namespace DriversExam.Core.Services;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IImageRepository _imageRepository;

    public QuestionsService(IQuestionRepository questionRepository, IAnswerRepository answerRepository, IImageRepository imageRepository)
    {
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _imageRepository = imageRepository;
    }

    public async Task<QuestionBasicInformationResponseDto> GetRandomQuestionAsync()
    {
        var questions = (await _questionRepository.GetAllAsync()).ToArray();

        var random = new Random();

        var randomQuestion = questions.ElementAtOrDefault(random.Next(0, questions.Length));

        if (randomQuestion == null)
        {
            throw new EntityNotFoundException();
        }

        return randomQuestion.ParseToQuestionResponseDto();
    }

    public async Task<IEnumerable<QuestionBasicInformationResponseDto>> GetNumberOfQuestionsAsync(int? quantity)
    {
        var questions = (await _questionRepository.GetAllAsync()).ToArray();

        var random = new Random();

        // if not all questions are to be returned, shuffle them first, to return random questions each time
        var questionsToReturn = quantity == null ? questions : questions.OrderBy(order => random.Next()).ToArray();

        return questionsToReturn.Take(quantity ?? questions.Length).Select(x => x.ParseToQuestionResponseDto());
    }

    public async Task DeleteQuestionAsync(int questionId)
    {
        await _questionRepository.DeleteByIdAsync(questionId);
    }

    public async Task AddNewQuestionAsync(AddNewQuestionRequestDto dto)
    {
        var correctAnswer = await _answerRepository.CreateAndGetAsync(new Answer()
        {
            Content = dto.CorrectAnswer
        });

        var answers = dto.AllAnswers.Where(x=>x != dto.CorrectAnswer).Select(async x => await _answerRepository.CreateAndGetAsync(new Answer()
        {
            Content = x
        }));

        var image = dto.ImageUrl == null
            ? null
            : await _imageRepository.CreateAndGetAsync(new Image()
            {
                ImageUrl = dto.ImageUrl
            });

        await _questionRepository.AddAsync(new Question()
        {
            Content = dto.Content,
            Answers = await Task.WhenAll(answers),
            CorrectAnswer = correctAnswer,
            Image = image
        });
    }

    public async Task UpdateQuestionAsync(UpdateQuestionRequestDto dto)
    {
        var questionToUpdate = await _questionRepository.GetByIdAsync(dto.Id);
        
        var newCorrectAnswer = dto.CorrectAnswer == null ? null : await _answerRepository.CreateAndGetAsync(new Answer()
        {
            Content = dto.CorrectAnswer
        });
        
        var newImage = dto.ImageUrl == null ? null : await _imageRepository.CreateAndGetAsync(new Image()
        {
            ImageUrl = dto.ImageUrl
        });

        var newAnswers = dto.Answers == null
            ? null
            : await Task.WhenAll(dto.Answers.Select(async x => await _answerRepository.CreateAndGetAsync(new Answer()
            {
                Content = x
            })));

        questionToUpdate.Answers = newAnswers?.ToList() ?? questionToUpdate.Answers;
        questionToUpdate.Content = dto.Content ?? questionToUpdate.Content;
        questionToUpdate.Image = newImage ?? questionToUpdate.Image;
        questionToUpdate.CorrectAnswer = newCorrectAnswer ?? questionToUpdate.CorrectAnswer;

        await _questionRepository.UpdateAsync(questionToUpdate);
    }
}