using DriversExam.Core.DTO;

namespace DriversExam.Core.Services;

public interface IQuestionsService
{
    Task<QuestionBasicInformationResponseDto> GetRandomQuestionAsync();
    
    // if quantity == null, return all questions info
    Task<IEnumerable<QuestionBasicInformationResponseDto>> GetNumberOfQuestionsAsync(int? quantity);

    Task DeleteQuestionAsync(int questionId);

    Task AddNewQuestionAsync(AddNewQuestionRequestDto dto);

    Task UpdateQuestionAsync(UpdateQuestionRequestDto dto);
}