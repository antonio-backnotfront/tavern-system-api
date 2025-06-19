using TavernSystem_Training_1.Models.DTO;

namespace TavernSystem_Training_1.Application;

public interface ITavernService
{
    public Task<List<GetAdventurersDto>> GetAllAdventurersAsync(CancellationToken cancellationToken);
    public Task<GetAdventurerDto?> GetAdventurerByIdAsync(int id, CancellationToken cancellationToken);
    public Task<CreateAdventurerDto?> CreateAdventurerAsync(CreateAdventurerDto dto, CancellationToken cancellationToken);

    public Task<List<GetAdventurersDto>> GetAllAdventurersOrderByNicknameAsync(CancellationToken cancellationToken);

    public Task<List<GetAdventurersDto>> GetAllAdventurersOrderByRaceIdAsync(CancellationToken cancellationToken);
}