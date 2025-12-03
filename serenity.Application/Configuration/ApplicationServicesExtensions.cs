using Microsoft.Extensions.DependencyInjection;
using serenity.Application.UseCases.AI;
using serenity.Application.UseCases.Appointments.Commands;
using serenity.Application.UseCases.Appointments.Queries;
using serenity.Application.UseCases.Auth;
using serenity.Application.UseCases.ChatMessages.Commands;
using serenity.Application.UseCases.ChatMessages.Queries;
using serenity.Application.UseCases.DailyMoods.Commands;
using serenity.Application.UseCases.DailyMoods.Queries;
using serenity.Application.UseCases.EmotionalStates.Commands;
using serenity.Application.UseCases.EmotionalStates.Queries;
using serenity.Application.UseCases.InsightsAndRecommendations.Commands;
using serenity.Application.UseCases.InsightsAndRecommendations.Queries;
using serenity.Application.UseCases.MeditationSessions.Commands;
using serenity.Application.UseCases.MeditationSessions.Queries;
using serenity.Application.UseCases.MentalWellbeingMetrics.Commands;
using serenity.Application.UseCases.MentalWellbeingMetrics.Queries;
using serenity.Application.UseCases.MoodMetrics.Commands;
using serenity.Application.UseCases.MoodMetrics.Queries;
using serenity.Application.UseCases.NoteSuggestions.Commands;
using serenity.Application.UseCases.NoteSuggestions.Queries;
using serenity.Application.UseCases.PatientNotes.Commands;
using serenity.Application.UseCases.PatientNotes.Queries;
using serenity.Application.UseCases.PatientReports.Commands;
using serenity.Application.UseCases.PatientReports.Queries;
using serenity.Application.UseCases.Patients.Commands;
using serenity.Application.UseCases.Patients.Queries;
using serenity.Application.UseCases.Prescriptions.Commands;
using serenity.Application.UseCases.Prescriptions.Queries;
using serenity.Application.UseCases.Psychologists.Commands;
using serenity.Application.UseCases.Psychologists.Queries;
using serenity.Application.UseCases.Simulation;
using serenity.Application.UseCases.StressLevelsByTime.Commands;
using serenity.Application.UseCases.StressLevelsByTime.Queries;
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

        // Appointments
        services.AddScoped<CreateAppointmentUseCase>();
        services.AddScoped<UpdateAppointmentUseCase>();
        services.AddScoped<DeleteAppointmentUseCase>();
        services.AddScoped<GetAppointmentByIdUseCase>();
        services.AddScoped<GetAllAppointmentsUseCase>();

        // Chat Messages
        services.AddScoped<CreateChatMessageUseCase>();
        services.AddScoped<UpdateChatMessageUseCase>();
        services.AddScoped<DeleteChatMessageUseCase>();
        services.AddScoped<GetChatMessageByIdUseCase>();
        services.AddScoped<GetAllChatMessagesUseCase>();

        // Daily Moods
        services.AddScoped<CreateDailyMoodUseCase>();
        services.AddScoped<UpdateDailyMoodUseCase>();
        services.AddScoped<DeleteDailyMoodUseCase>();
        services.AddScoped<GetDailyMoodByIdUseCase>();
        services.AddScoped<GetAllDailyMoodsUseCase>();

        // Emotional States
        services.AddScoped<CreateEmotionalStateUseCase>();
        services.AddScoped<UpdateEmotionalStateUseCase>();
        services.AddScoped<DeleteEmotionalStateUseCase>();
        services.AddScoped<GetEmotionalStateByIdUseCase>();
        services.AddScoped<GetAllEmotionalStatesUseCase>();

        // Insights and Recommendations
        services.AddScoped<CreateInsightsAndRecommendationUseCase>();
        services.AddScoped<UpdateInsightsAndRecommendationUseCase>();
        services.AddScoped<DeleteInsightsAndRecommendationUseCase>();
        services.AddScoped<GetInsightsAndRecommendationByIdUseCase>();
        services.AddScoped<GetAllInsightsAndRecommendationsUseCase>();

        // Meditation Sessions
        services.AddScoped<CreateMeditationSessionUseCase>();
        services.AddScoped<UpdateMeditationSessionUseCase>();
        services.AddScoped<DeleteMeditationSessionUseCase>();
        services.AddScoped<GetMeditationSessionByIdUseCase>();
        services.AddScoped<GetAllMeditationSessionsUseCase>();

        // Mental Wellbeing Metrics
        services.AddScoped<CreateMentalWellbeingMetricUseCase>();
        services.AddScoped<UpdateMentalWellbeingMetricUseCase>();
        services.AddScoped<DeleteMentalWellbeingMetricUseCase>();
        services.AddScoped<GetMentalWellbeingMetricByIdUseCase>();
        services.AddScoped<GetAllMentalWellbeingMetricsUseCase>();

        // Mood Metrics
        services.AddScoped<CreateMoodMetricUseCase>();
        services.AddScoped<UpdateMoodMetricUseCase>();
        services.AddScoped<DeleteMoodMetricUseCase>();
        services.AddScoped<GetMoodMetricByIdUseCase>();
        services.AddScoped<GetAllMoodMetricsUseCase>();

        // Note Suggestions
        services.AddScoped<CreateNoteSuggestionUseCase>();
        services.AddScoped<UpdateNoteSuggestionUseCase>();
        services.AddScoped<DeleteNoteSuggestionUseCase>();
        services.AddScoped<GetNoteSuggestionByIdUseCase>();
        services.AddScoped<GetAllNoteSuggestionsUseCase>();

        // Patient Notes
        services.AddScoped<CreatePatientNoteUseCase>();
        services.AddScoped<UpdatePatientNoteUseCase>();
        services.AddScoped<DeletePatientNoteUseCase>();
        services.AddScoped<GetPatientNoteByIdUseCase>();
        services.AddScoped<GetAllPatientNotesUseCase>();

        // Patient Reports
        services.AddScoped<CreatePatientReportUseCase>();
        services.AddScoped<UpdatePatientReportUseCase>();
        services.AddScoped<DeletePatientReportUseCase>();
        services.AddScoped<GetPatientReportByIdUseCase>();
        services.AddScoped<GetAllPatientReportsUseCase>();

        // Prescriptions
        services.AddScoped<CreatePrescriptionUseCase>();
        services.AddScoped<UpdatePrescriptionUseCase>();
        services.AddScoped<DeletePrescriptionUseCase>();
        services.AddScoped<GetPrescriptionByIdUseCase>();
        services.AddScoped<GetAllPrescriptionsUseCase>();

        // Stress Levels By Time
        services.AddScoped<CreateStressLevelsByTimeUseCase>();
        services.AddScoped<UpdateStressLevelsByTimeUseCase>();
        services.AddScoped<DeleteStressLevelsByTimeUseCase>();
        services.AddScoped<GetStressLevelsByTimeByIdUseCase>();
        services.AddScoped<GetAllStressLevelsByTimeUseCase>();

        // AI Services
        services.AddScoped<GenerateAIRecommendationsForPatientUseCase>();
        services.AddScoped<GenerateAIDiagnosisFromNotesUseCase>();
        services.AddScoped<AnalyzeWellbeingTrendsUseCase>();

        // Simulation Services
        services.AddScoped<SimulateFullDayMetricsUseCase>();
        services.AddScoped<SimulateMeditationSessionsUseCase>();
        services.AddScoped<SimulateStressLevelsUseCase>();
        services.AddScoped<SimulateWellbeingMetricsUseCase>();

        // Auth
        services.AddScoped<GoogleLoginUseCase>();

        return services;
    }
}

