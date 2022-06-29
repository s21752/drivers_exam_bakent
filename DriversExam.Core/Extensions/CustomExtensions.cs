using DriversExam.Core.DTO;
using DriversExam.Infrastructure.Entities;

namespace DriversExam.Core.Extensions;

public static class CustomExtensions
{
    public static QuestionBasicInformationResponseDto ParseToQuestionResponseDto(this Question question)
    {
        return new QuestionBasicInformationResponseDto(
            question.Content,
            // making sure that the correct answer is among all the other answers, and that all answers are distinct
            question.Answers.Select(x=>x.Content).Append(question.CorrectAnswer.Content).Distinct(),
            question.CorrectAnswer.Content,
            question.Image?.ImageUrl,
            question.Id);
    }
}