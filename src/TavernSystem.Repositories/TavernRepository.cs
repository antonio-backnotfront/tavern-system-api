using System.Data;
using Microsoft.Data.SqlClient;
using TavernSystem_Training_1.Models;
using TavernSystem_Training_1.Models.DTO;

namespace TavernSystem_Training_1.Repositories;

public class TavernRepository : ITavernRepository
{
    private readonly string _connectionString;

    public TavernRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Adventurer>> GetAllAdventurersAsync(CancellationToken cancellationToken)
    {
        string query = "SELECT Id, Nickname, RaceId, ExperienceId, PersonId FROM Adventurer";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = new SqlCommand(query, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        List<Adventurer> adventurers = new List<Adventurer>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var adventurer = new Adventurer();
            adventurer.Id = reader.GetInt32(0);
            adventurer.Nickname = reader.GetString(1);
            adventurer.RaceId = reader.GetInt32(2);
            adventurer.ExperienceId = reader.GetInt32(3);
            adventurer.PersonId = reader.GetString(4);
            adventurers.Add(adventurer);
        }

        return adventurers;
    }

    public async Task<Adventurer?> GetAdventurerByIdAsync(int id, CancellationToken cancellationToken)
    {
        string query = "SELECT Id, Nickname, RaceId, ExperienceId, PersonId FROM Adventurer WHERE Id = @Id";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var adventurer = new Adventurer();
            adventurer.Id = reader.GetInt32(0);
            adventurer.Nickname = reader.GetString(1);
            adventurer.RaceId = reader.GetInt32(2);
            adventurer.ExperienceId = reader.GetInt32(3);
            adventurer.PersonId = reader.GetString(4);
            return adventurer;
        }

        return null;
    }

    public async Task<Adventurer?> GetAdventurerByPersonIdAsync(string id, CancellationToken cancellationToken)
    {
        string query = "SELECT Id, Nickname, RaceId, ExperienceId, PersonId FROM Adventurer WHERE PersonId = @Id";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var adventurer = new Adventurer();
            adventurer.Id = reader.GetInt32(0);
            adventurer.Nickname = reader.GetString(1);
            adventurer.RaceId = reader.GetInt32(2);
            adventurer.ExperienceId = reader.GetInt32(3);
            adventurer.PersonId = reader.GetString(4);
            return adventurer;
        }

        return null;
    }

    public async Task<Adventurer?> GetAdventurerByNicknameAsync(string nickname, CancellationToken cancellationToken)
    {
        string query = "SELECT Id, Nickname, RaceId, ExperienceId, PersonId FROM Adventurer WHERE Nickname = @Nickname";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Nickname", nickname);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var adventurer = new Adventurer();
            adventurer.Id = reader.GetInt32(0);
            adventurer.Nickname = reader.GetString(1);
            adventurer.RaceId = reader.GetInt32(2);
            adventurer.ExperienceId = reader.GetInt32(3);
            adventurer.PersonId = reader.GetString(4);
            return adventurer;
        }

        return null;
    }

    public async Task<Race?> GetRaceByIdAsync(int id, CancellationToken cancellationToken)
    {
        string query = "SELECT Id, Name FROM Race WHERE Id = @Id";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var race = new Race();
            race.Id = reader.GetInt32(0);
            race.Name = reader.GetString(1);
            return race;
        }

        return null;
    }

    public async Task<ExperienceLevel?> GetExperienceLevelByIdAsync(int id, CancellationToken cancellationToken)
    {
        string query = "SELECT Id, Name FROM ExperienceLevel WHERE Id = @Id";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var experience = new ExperienceLevel();
            experience.Id = reader.GetInt32(0);
            experience.Name = reader.GetString(1);
            return experience;
        }

        return null;
    }

    public async Task<Person?> GetPersonByIdAsync(string id, CancellationToken cancellationToken)
    {
        string query = "SELECT Id, FirstName, MiddleName, LastName, HasBounty FROM Person WHERE Id = @Id";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (await reader.ReadAsync(cancellationToken))
        {
            var person = new Person();
            person.Id = reader.GetString(0);
            person.FirstName = reader.GetString(1);
            person.MiddleName = reader.IsDBNull(2) ? null : reader.GetString(2);
            person.LastName = reader.GetString(3);
            person.HasBounty = reader.GetBoolean(4);
            return person;
        }

        return null;
    }

    public async Task<int> CreateAdventurerAsync(CreateAdventurerDto dto, CancellationToken cancellationToken)
    {
        string query = @"INSERT INTO Adventurer(Id, Nickname, RaceId, ExperienceId, PersonId) 
            OUTPUT INSERTED.Id
            VALUES((select ISNULL(Max(Id),0) + 1 from Adventurer), @Nickname, @RaceId, @ExperienceId, @PersonId)
            ";
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await using var transaction = connection.BeginTransaction();
        try
        {
            var command = new SqlCommand(query, connection, transaction);

            command.Parameters.AddWithValue("@Nickname", dto.Nickname);
            command.Parameters.AddWithValue("@RaceId", dto.RaceId);
            command.Parameters.AddWithValue("@ExperienceId", dto.ExperienceLevelId);
            command.Parameters.AddWithValue("@PersonId", dto.PersonDataId);
            Console.WriteLine($"hello");
            var id = (int?)await command.ExecuteScalarAsync(cancellationToken);
            if (id == null) throw new Exception("Couldn't insert and retrieve new id");
            await transaction.CommitAsync(cancellationToken);
            return id.Value;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}