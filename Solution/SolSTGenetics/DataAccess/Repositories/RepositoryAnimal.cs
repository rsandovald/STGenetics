using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.DataEntities;
using Models.DTOs;

namespace DataAccess.Repositories
{
    public class RepositoryAnimal : IRepositoryAnimal
    {
        private readonly string connectionString;

        public RepositoryAnimal(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Add(Animal data)
        {
            using var connection = new SqlConnection(connectionString);
            _ = await connection.ExecuteAsync(@"INSERT INTO dbo.Animals
                                                   (AnimalId
                                                   ,Name
                                                   ,Breed
                                                   ,BirthDate
                                                   ,Sex
                                                   ,Price
                                                   ,Status)
                                             VALUES (@AnimalId, 
                                                   @Name,
                                                   @Breed, 
                                                   @BirthDate,
                                                   @Sex, 
                                                   @Price, 
                                                   @Status);", data);
        }

        public async Task Delete(Guid id)
        {
            using var connection = new SqlConnection(connectionString);
            _ = await connection.ExecuteAsync(@$"DELETE FROM dbo.Animals
                                                 WHERE AnimalId = @AnimalId",
                                                 new { AnimalId = id });
        }

        public async Task<IEnumerable<Animal>> Get(AnimalGetRequest filter)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Animal>(
                "dbo.AnimalGet",
                new
                {
                    Name = string.IsNullOrEmpty(filter.Name) ? "%%" : filter.Name,
                    Sex = string.IsNullOrEmpty(filter.Sex) ? "%%" : filter.Sex,
                    Status = filter.Status
                },

                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Animal> Get(Guid id)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<Animal>(
                @"SELECT AnimalId
                  ,Name
                  ,Breed
                  ,BirthDate
                  ,Sex
                  ,Price
                  ,Status
              FROM dbo.Animals
              WHERE
              AnimalId = @AnimalId ",
                new { AnimalId = id });
        }

        public async Task Update(Animal data)
        {
            using var connection = new SqlConnection(connectionString);
            _ = await connection.ExecuteAsync(@"UPDATE dbo.Animals
                                               SET Name = @Name
                                                  ,Breed = @Breed
                                                  ,BirthDate = @BirthDate
                                                  ,Sex = @Sex
                                                  ,Price = @Price
                                                  ,Status = @Status
                                            WHERE AnimalId = @AnimalId
                                            ", data);
        }
    }
}
