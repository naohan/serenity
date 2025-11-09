using serenity.Application.DTOs;
using serenity.Infrastructure;

namespace serenity.Application.UseCases.Trainers;

internal static class TrainerMappingExtensions
{
    public static TrainerDto ToDto(this Trainer trainer)
    {
        return new TrainerDto
        {
            Id = trainer.Id,
            UserId = trainer.UserId,
            Bio = trainer.Bio,
            Specialization = trainer.Specialization,
            CreatedAt = trainer.CreatedAt,
            UpdatedAt = trainer.UpdatedAt
        };
    }
}

