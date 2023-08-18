using Models.DataEntities;
using Models.DTOs;

namespace BusinessLogic
{
    public interface IAnimalBL
    {
        Task<AnimalAddResponse> Add(AnimalBaseDto data);
        Task<AnimalDeleteResponse> Delete(Guid id);
        Task<AnimalPutResponse> Update(Guid id, AnimalBaseDto data);        
        Task<AnimalGetResponse> Get(AnimalGetRequest filter);
    }
}