using DriversExam.Core.DTO;

namespace DriversExam.Core.Services;

public interface IQuestionsService
{
    Task<QuestionBasicInformationResponseDto> GetRandomQuestionAsync();
    
    Task<IEnumerable<QuestionBasicInformationResponseDto>> GetManyRandomQuestionsAsync(int quantity);
    
    Task<IEnumerable<QuestionBasicInformationResponseDto>> GetPaginatedQuestionsAsync(int pageSize, int pageIndex);

    Task DeleteQuestionAsync(int questionId);

    Task<int> CountQuestionsAsync();

    Task AddNewQuestionAsync(AddNewQuestionRequestDto dto);

    Task UpdateQuestionAsync(UpdateQuestionRequestDto dto);
}