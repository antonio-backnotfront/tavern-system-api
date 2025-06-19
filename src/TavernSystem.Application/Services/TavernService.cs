using System.Text.RegularExpressions;
using TavernSystem_Training_1.Application.Exceptions;
using TavernSystem_Training_1.Models;
using TavernSystem_Training_1.Models.DTO;
using TavernSystem_Training_1.Repositories;

namespace TavernSystem_Training_1.Application;

public class TavernService : ITavernService
{
    ITavernRepository _repository;

    public TavernService(ITavernRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAdventurersDto>> GetAllAdventurersAsync(CancellationToken cancellationToken)
    {
        return (await _repository.GetAllAdventurersAsync(cancellationToken)).Select(adv => new GetAdventurersDto
        {
            Id = adv.Id,
            Nickname = adv.Nickname,
        }).ToList();
    }

    public async Task<List<GetAdventurersDto>> GetAllAdventurersOrderByNicknameAsync(
        CancellationToken cancellationToken)
    {
        return (await _repository.GetAllAdventurersAsync(cancellationToken))
            .OrderBy(d => d.Nickname)
            .Select(adv => new GetAdventurersDto
            {
                Id = adv.Id,
                Nickname = adv.Nickname,
            })
            .ToList();
    }

    public async Task<List<GetAdventurersDto>> GetAllAdventurersOrderByRaceIdAsync(CancellationToken cancellationToken)
    {
        return (await _repository.GetAllAdventurersAsync(cancellationToken))
            .OrderBy(d => d.RaceId)
            .Select(adv => new GetAdventurersDto
            {
                Id = adv.Id,
                Nickname = adv.Nickname,
            })
            .ToList();
    }

    public async Task<GetAdventurerDto?> GetAdventurerByIdAsync(int id, CancellationToken cancellationToken)
    {
        var adventurer = await _repository.GetAdventurerByIdAsync(id, cancellationToken);
        if (adventurer == null) return null;
        var experienceLevel = await _repository.GetExperienceLevelByIdAsync(adventurer.ExperienceId, cancellationToken);
        var race = await _repository.GetRaceByIdAsync(adventurer.RaceId, cancellationToken);
        var person = await _repository.GetPersonByIdAsync(adventurer.PersonId, cancellationToken);

        return new GetAdventurerDto()
        {
            Id = adventurer.Id,
            Nickname = adventurer.Nickname,
            Race = race.Name,
            ExperienceLevel = experienceLevel.Name,
            PersonData = new GetPersonDto()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                HasBounty = person.HasBounty,
            }
        };
    }

    public async Task<CreateAdventurerDto?> CreateAdventurerAsync(CreateAdventurerDto dto,
        CancellationToken cancellationToken)
    {
        // validate personDataId
        // string personDataIdRegex =
        //     "^[A-Z]{2}(000[1-9]{1}|00[1-9]{1}[0-9]{1}|0[1-9]{1}[0-9]{2}|[1-9]{1}[0-9]{3})(0[1-9]{1}|1[0-2]{1})(0[1-9]{1}|1[0-9]{1}|2[0-8]{1})[0-9]{4}[A-Z]{2}$";
        // if (!Regex.IsMatch(dto.PersonDataId, personDataIdRegex))
        //     throw new InvalidPersonDataException("Person data ID is not valid.");

        // validate if person exists
        Person? person = await _repository.GetPersonByIdAsync(dto.PersonDataId, cancellationToken);
        if (person == null) throw new NotExistsException($"Person with ID {dto.PersonDataId} does not exist.");

        // validate if nickname is unique
        if (await _repository.GetAdventurerByNicknameAsync(dto.Nickname, cancellationToken) != null)
            throw new AlreadyExistsException($"Nickname {dto.Nickname} already exists.");

        // validate if person has bounty
        if (person.HasBounty) throw new HasBountyException("Person has bounty.");

        // validate if such adventurer already exists
        var adventurer = await _repository.GetAdventurerByPersonIdAsync(dto.PersonDataId, cancellationToken);
        if (adventurer != null) throw new AlreadyExistsException("Adventurer with this PersonDataId already exists.");

        // validate if experience and race exist
        ExperienceLevel? experienceLevel =
            await _repository.GetExperienceLevelByIdAsync(dto.ExperienceLevelId, cancellationToken);
        if (experienceLevel == null) throw new NotExistsException("Experience level doesn't exist.");
        Race? race = await _repository.GetRaceByIdAsync(dto.RaceId, cancellationToken);
        if (race == null) throw new NotExistsException("Race doesn't exist.");


        CreateAdventurerDto returnDto = new CreateAdventurerDto()
        {
            Id = await _repository.CreateAdventurerAsync(dto, cancellationToken),
            Nickname = dto.Nickname,
            RaceId = dto.RaceId,
            ExperienceLevelId = dto.ExperienceLevelId,
            PersonDataId = dto.PersonDataId,
        };
        return returnDto;
    }
}