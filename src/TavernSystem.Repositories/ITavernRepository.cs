using TavernSystem_Training_1.Models;
using TavernSystem_Training_1.Models.DTO;

namespace TavernSystem_Training_1.Repositories;

public interface ITavernRepository
{
    public Task<List<Adventurer>> GetAllAdventurersAsync(CancellationToken cancellationToken);
    public Task<Adventurer?> GetAdventurerByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Adventurer?> GetAdventurerByPersonIdAsync(string id, CancellationToken cancellationToken);
    public Task<Adventurer?> GetAdventurerByNicknameAsync(string nickname, CancellationToken cancellationToken);
    public Task<Race?> GetRaceByIdAsync(int id, CancellationToken cancellationToken);
    public Task<ExperienceLevel?> GetExperienceLevelByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Person?> GetPersonByIdAsync(string id, CancellationToken cancellationToken);
    public Task<int> CreateAdventurerAsync(CreateAdventurerDto dto, CancellationToken cancellationToken);
    
}