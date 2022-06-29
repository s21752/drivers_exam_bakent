namespace DriversExam.Infrastructure.Repository.Image;

public interface IImageRepository : IRepository<Entities.Image>
{
    Task<Entities.Image> CreateAndGetAsync(Entities.Image entity);
}