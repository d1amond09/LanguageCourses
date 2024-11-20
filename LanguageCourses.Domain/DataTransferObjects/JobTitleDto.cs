namespace LanguageJobTitles.Domain.DataTransferObjects;

public record JobTitleDto
{
    public Guid JobTitleId { get; init; }

    public string? Name { get; init; }

    public decimal Salary { get; init; }

    public string? Responsibilities { get; init; }

    public string? Requirements { get; init; }
}

public record JobTitleForManipulationDto
{
    public string? Name { get; init; }
    public decimal Salary { get; init; }
    public string? Responsibilities { get; init; }
    public string? Requirements { get; init; }
}

public record JobTitleForUpdateDto : JobTitleForManipulationDto;
public record JobTitleForCreationDto : JobTitleForManipulationDto;
