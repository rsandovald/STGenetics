using Models.DataEntities;
using Models.DTOs;

namespace DataAccess.Repositories
{
    public interface IRepositoryAnimal
    {
        Task Add(Animal data);
        Task Delete(Guid id);
        Task<IEnumerable<Animal>> Get(AnimalGetRequest filter);
        Task<Animal> Get(Guid id);
        Task Update(Animal data);
    }
}