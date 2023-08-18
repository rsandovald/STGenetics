using AutoMapper;
using DataAccess.Repositories;
using Microsoft.Identity.Client;
using Models.DataEntities;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic;
public class AnimalBL : IAnimalBL
{
    IRepositoryAnimal _repositoryAnimal;
    IMapper _mapper;
    public AnimalBL(IRepositoryAnimal repositoryAnimal, IMapper mapper)
    {
        this._repositoryAnimal = repositoryAnimal;
        _mapper = mapper;
    }    

    public async Task<AnimalAddResponse> Add(AnimalBaseDto data)
    {
        try
        {
            var listAnimal = await this._repositoryAnimal.Get (new AnimalGetRequest() { Name = data.Name }); 

            if (listAnimal != null && listAnimal.Count  ()>  0 )

                return new AnimalAddResponse()
                {
                    TransactionResult = new TransactionResultDto()
                    {
                        Code = 400,
                        Description = "BadRequest. " + $" There is an animal with the same name."
                    },
                    AnimalInserted = null
                };

            var animal = this._mapper.Map<Animal>(data);
            animal.AnimalId = Guid.NewGuid ();

            await _repositoryAnimal.Add(animal);

            return new AnimalAddResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = 200,
                    Description = "Ok"
                },
                AnimalInserted = animal
            };        

        }
        catch (Exception ex)
        {
            return new AnimalAddResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = 500,
                    Description = "Internal Error. " + ex.Message
                },
                AnimalInserted = null
            };
        }

    }
       

    public async Task<AnimalPutResponse> Update(Guid id, AnimalBaseDto data)
    {
        try
        {
            var animal = await this._repositoryAnimal.Get(id);

            if (animal == null)

                return new AnimalPutResponse()
                {
                    TransactionResult = new TransactionResultDto()
                    {
                        Code = 404,
                        Description = "Not Found"
                    },
                    AnimalUpdated = null
                };

            animal = this._mapper.Map<Animal>(data);
            animal.AnimalId = id; 
            await _repositoryAnimal.Update(animal);

            return new AnimalPutResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = 200,
                    Description = "Ok"
                },
                AnimalUpdated = animal
            };
        }
        catch (Exception ex)
        {
            return new AnimalPutResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = 500,
                    Description = "Internal Error. "  + ex.Message
                },
                AnimalUpdated = null
            };
        }

    }

    public async Task<AnimalDeleteResponse> Delete (Guid id)
    {
        try
        {
            var animal = await this._repositoryAnimal.Get(id);

            if (animal == null)

                return new AnimalDeleteResponse()
                {
                    TransactionResult = new TransactionResultDto()
                            { 
                                Code = (int) TransactionResultCodes.NotFound,
                                Description = "Not Found" 
                            }, 
                    AnimalDeleted = null
                };

            animal.AnimalId = id;
            await _repositoryAnimal.Delete(id);

            return new AnimalDeleteResponse()
            {
                TransactionResult = new TransactionResultDto()
                        {
                            Code = (int)TransactionResultCodes.Ok,
                            Description = "Ok"
                        },
                AnimalDeleted = animal
            };
        }
        catch (Exception ex)
        {
            return new AnimalDeleteResponse()
            {  
                TransactionResult = new TransactionResultDto()
                            { 
                                Code = (int) TransactionResultCodes.Error,
                                 Description = "Error. " + ex.Message
                },
                AnimalDeleted = null
            };
        }
    }

    public async Task<AnimalGetResponse> Get(AnimalGetRequest filter)
    {
        AnimalGetResponse result;

        if (isFilterEmpty(filter))
        {
            result = new AnimalGetResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = (int)TransactionResultCodes.BadRequest,
                    Description = "BadRequest." + "there is not any filter"
                },
                Animals = null
            };

            return result;
        }
        
        if (filter.AnimalId != Guid.Empty)
            return await this.Get (filter.AnimalId) ;

        var animals = await this._repositoryAnimal.Get(filter);

        if (animals != null && animals.Count() > 0)
        {
            
            result = new AnimalGetResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = (int)TransactionResultCodes.Ok,
                    Description = "Ok"
                },
                Animals = animals.ToList()
            };

            return result;
        }
        else
        {
            result = new AnimalGetResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = (int)TransactionResultCodes.NotFound,
                    Description = "NotFound"
                },
                Animals = null
            };
            return result;
        }

    }

    public async Task<AnimalGetResponse> Get(Guid animalId)
    {
        AnimalGetResponse result;
        var animal = await this._repositoryAnimal.Get(animalId);

        if (animal == null)
        {
            result = new AnimalGetResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = (int)TransactionResultCodes.NotFound,
                    Description = "NotFound"
                },
                Animals = null
            };
            return result;
        }

        List <Animal> animals = new List<Animal>();
        animals.Add (animal);

        result = new AnimalGetResponse()
        {
            TransactionResult = new TransactionResultDto()
            {
                Code = (int)TransactionResultCodes.Ok,
                Description = "Ok"
            },
            Animals = animals
        };

        return result;
    }

    public bool isFilterEmpty(AnimalGetRequest filter)
    {
        //TODO: Implement a comparable 
        if (filter.AnimalId == Guid.Empty
            && filter.Name == string.Empty
            && filter.Sex == string.Empty
            && filter.Status == -1)
            return true;

        return false; 
    }
    
}