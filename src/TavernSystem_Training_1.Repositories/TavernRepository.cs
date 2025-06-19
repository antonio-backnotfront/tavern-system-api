using TavernSystem_Training_1.Models;

namespace TavernSystem_Training_1.Repositories;

public interface ITavernRepository
{
    public Task<List<Adventurer>> GetAllAdventurersAsync(CancellationToken cancellationToken);
    public Task<Adventurer?> GetAdventurerByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Race?> GetRaceByIdAsync(int id, CancellationToken cancellationToken);
    public Task<ExperienceLevel?> GetExperienceLevelByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Person?> GetPersonByIdAsync(string id, CancellationToken cancellationToken);
    
}