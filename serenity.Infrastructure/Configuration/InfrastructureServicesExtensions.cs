using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using serenity.Application.Interfaces;
using serenity.Infrastructure.Adapters.Repositories;
using serenity.Infrastructure.Adapters.Services;
using serenity.Infrastructure.Data;

namespace serenity.Infrastructure.Configuration;

/// <summary>
/// Service registration helpers for the Infrastructure layer.
/// </summary>
public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SerenityDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
            }

            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        // Repositories
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        services.AddScoped<IDailyMoodRepository, DailyMoodRepository>();
        services.AddScoped<IEmotionalStateRepository, EmotionalStateRepository>();
        services.AddScoped<IInsightsAndRecommendationRepository, InsightsAndRecommendationRepository>();
        services.AddScoped<IMeditationSessionRepository, MeditationSessionRepository>();
        services.AddScoped<IMentalWellbeingMetricRepository, MentalWellbeingMetricRepository>();
        services.AddScoped<IMoodMetricRepository, MoodMetricRepository>();
        services.AddScoped<INoteSuggestionRepository, NoteSuggestionRepository>();
        services.AddScoped<IPatientNoteRepository, PatientNoteRepository>();
        services.AddScoped<IPatientReportRepository, PatientReportRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IPsychologistRepository, PsychologistRepository>();
        services.AddScoped<IStressLevelsByTimeRepository, StressLevelsByTimeRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();

        // Configure Google Authentication
        services.Configure<GoogleAuthSettings>(configuration.GetSection("Authentication:Google"));
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();

        // Configure HttpClient for HTTP services
        services.AddHttpClient();

        // Register Assistant Service (using Groq by default)
        services.AddScoped<IAssistantService, GroqAssistantService>();

        return services;
    }
}

