using Microsoft.Extensions.DependencyInjection;
using serenity.Application.UseCases.Auth;
using serenity.Application.UseCases.Patients.Commands;
using serenity.Application.UseCases.Patients.Queries;
using serenity.Application.UseCases.Psychologists.Commands;
using serenity.Application.UseCases.Psychologists.Queries;
using serenity.Application.UseCases.Trainers.Commands;
using serenity.Application.UseCases.Trainers.Queries;
using serenity.Application.UseCases.Users.Commands;
using serenity.Application.UseCases.Users.Queries;

namespace serenity.Application.Configuration;

/// <summary>
/// Dependency registration for the application layer.
/// </summary>
public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddSerenityApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<UpdateUserUseCase>();
        services.AddScoped<DeleteUserUseCase>();
        services.AddScoped<GetUserByIdUseCase>();
        services.AddScoped<GetAllUsersUseCase>();

        services.AddScoped<CreatePatientUseCase>();
        services.AddScoped<UpdatePatientUseCase>();
        services.AddScoped<DeletePatientUseCase>();
        services.AddScoped<GetPatientByIdUseCase>();
        services.AddScoped<GetAllPatientsUseCase>();

        services.AddScoped<CreatePsychologistUseCase>();
        services.AddScoped<UpdatePsychologistUseCase>();
        services.AddScoped<DeletePsychologistUseCase>();
        services.AddScoped<GetPsychologistByIdUseCase>();
        services.AddScoped<GetAllPsychologistsUseCase>();

        services.AddScoped<CreateTrainerUseCase>();
        services.AddScoped<UpdateTrainerUseCase>();
        services.AddScoped<DeleteTrainerUseCase>();
        services.AddScoped<GetTrainerByIdUseCase>();
        services.AddScoped<GetAllTrainersUseCase>();

        return services;
    }
}

