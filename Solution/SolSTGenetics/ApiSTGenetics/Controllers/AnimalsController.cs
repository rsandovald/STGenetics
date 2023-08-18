using BusinessLogic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Models.DataEntities;
using Models.DTOs;
using System.Net.Http.Headers;

namespace ApiSTGenetics.Controllers; 

[Route("api/animals")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalBL _animalBl;

    public AnimalsController(IAnimalBL animalBl)
    {
        this._animalBl = animalBl;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AnimalBaseDto animalDto)
    {
        var result = await this._animalBl.Add(animalDto);
        return StatusCode(result.TransactionResult.Code, result);

    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult> Put(Guid id, [FromBody] AnimalBaseDto animalDto)
    {
        var result = await this._animalBl.Update (id, animalDto);
        return StatusCode (result.TransactionResult.Code, result);
    }


    [HttpDelete("{id:Guid}")]
    public async Task <ActionResult <AnimalDeleteResponse> >  Delete(Guid id)
    {
        var result = await this._animalBl.Delete(id);
        return StatusCode(result.TransactionResult.Code, result);   
    }

    [HttpGet]
    public async Task<ActionResult<AnimalGetResponse>> Get([FromQuery] AnimalGetRequest filter)
    {
        var result = await this._animalBl.Get(filter); 
        return StatusCode(result.TransactionResult.Code, result);
    }


}
