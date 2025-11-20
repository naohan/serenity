using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using serenity.Infrastructure;

namespace serenity.Infrastructure.Data;

public partial class SerenityDbContext : DbContext
{
    public SerenityDbContext()
    {
    }

    public SerenityDbContext(DbContextOptions<SerenityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<DailyMood> DailyMoods { get; set; }

    public virtual DbSet<EmotionalState> EmotionalStates { get; set; }

    public virtual DbSet<InsightsAndRecommendation> InsightsAndRecommendations { get; set; }

    public virtual DbSet<MeditationSession> MeditationSessions { get; set; }

    public virtual DbSet<MentalWellbeingMetric> MentalWellbeingMetrics { get; set; }

    public virtual DbSet<MoodMetric> MoodMetrics { get; set; }

    public virtual DbSet<NoteSuggestion> NoteSuggestions { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientNote> PatientNotes { get; set; }

    public virtual DbSet<PatientReport> PatientReports { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Psychologist> Psychologists { get; set; }

    public virtual DbSet<StressLevelsByTime> StressLevelsByTimes { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    {
        if (!optionsBuilder.IsConfigured)
        {
            const string fallbackConnection = "Host=localhost;Port=5432;Database=serenity;Username=postgres;Password=;";
            optionsBuilder.UseNpgsql(fallbackConnection);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PsychologistId, "psychologist_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointment_date");
            entity.Property(e => e.AppointmentTime)
                .HasColumnType("time")
                .HasColumnName("appointment_time");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Duration)
                .HasColumnType("integer")
                .HasColumnName("duration");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.PsychologistId)
                .HasColumnType("integer")
                .HasColumnName("psychologist_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pendiente'")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("appointments_ibfk_1");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PsychologistId)
                .HasConstraintName("appointments_ibfk_2");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chat_messages");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PsychologistId, "psychologist_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.IsFromPsychologist).HasColumnName("is_from_psychologist");
            entity.Property(e => e.MessageText)
                .HasColumnType("text")
                .HasColumnName("message_text");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.PsychologistId)
                .HasColumnType("integer")
                .HasColumnName("psychologist_id");
            entity.Property(e => e.ReadAt)
                .HasColumnType("timestamp")
                .HasColumnName("read_at");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("sent_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("chat_messages_ibfk_1");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.PsychologistId)
                .HasConstraintName("chat_messages_ibfk_2");
        });

        modelBuilder.Entity<DailyMood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("daily_moods");

            entity.HasIndex(e => new { e.PatientId, e.Date }, "patient_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Mood)
                .HasColumnType("smallint")
                .HasColumnName("mood");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.DailyMoods)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("daily_moods_ibfk_1");
        });

        modelBuilder.Entity<EmotionalState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("emotional_states");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EmotionalState1)
                .HasMaxLength(50)
                .HasColumnName("emotional_state");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.Value)
                .HasPrecision(5, 2)
                .HasColumnName("value");

            entity.HasOne(d => d.Patient).WithMany(p => p.EmotionalStates)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("emotional_states_ibfk_1");
        });

        modelBuilder.Entity<InsightsAndRecommendation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("insights_and_recommendations");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PsychologistId, "psychologist_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .HasColumnName("priority");
            entity.Property(e => e.PsychologistId)
                .HasColumnType("integer")
                .HasColumnName("psychologist_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.InsightsAndRecommendations)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("insights_and_recommendations_ibfk_1");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.InsightsAndRecommendations)
                .HasForeignKey(d => d.PsychologistId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("insights_and_recommendations_ibfk_2");
        });

        modelBuilder.Entity<MeditationSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("meditation_sessions");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DurationMinutes)
                .HasColumnType("integer")
                .HasColumnName("duration_minutes");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.SessionDate).HasColumnName("session_date");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");

            entity.HasOne(d => d.Patient).WithMany(p => p.MeditationSessions)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("meditation_sessions_ibfk_1");
        });

        modelBuilder.Entity<MentalWellbeingMetric>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mental_wellbeing_metrics");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.ConcentrationLevel)
                .HasColumnType("smallint")
                .HasColumnName("concentration_level");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.EnergyLevel)
                .HasColumnType("smallint")
                .HasColumnName("energy_level");
            entity.Property(e => e.MeditationMinutes)
                .HasDefaultValueSql("0")
                .HasColumnType("integer")
                .HasColumnName("meditation_minutes");
            entity.Property(e => e.MeditationSessions)
                .HasDefaultValueSql("0")
                .HasColumnType("integer")
                .HasColumnName("meditation_sessions");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.SatisfactionLevel)
                .HasColumnType("smallint")
                .HasColumnName("satisfaction_level");
            entity.Property(e => e.SleepDuration)
                .HasPrecision(4, 2)
                .HasColumnName("sleep_duration");
            entity.Property(e => e.SleepQuality)
                .HasMaxLength(50)
                .HasColumnName("sleep_quality");
            entity.Property(e => e.StressLevel)
                .HasColumnType("smallint")
                .HasColumnName("stress_level");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.MentalWellbeingMetrics)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("mental_wellbeing_metrics_ibfk_1");
        });

        modelBuilder.Entity<MoodMetric>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mood_metrics");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.AnxiousPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("anxious_percentage");
            entity.Property(e => e.CalmPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("calm_percentage");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.HappyPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("happy_percentage");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.SadPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("sad_percentage");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.MoodMetrics)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("mood_metrics_ibfk_1");
        });

        modelBuilder.Entity<NoteSuggestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("note_suggestions");

            entity.HasIndex(e => e.NoteId, "note_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.NoteId)
                .HasColumnType("integer")
                .HasColumnName("note_id");
            entity.Property(e => e.Suggestion)
                .HasColumnType("text")
                .HasColumnName("suggestion");

            entity.HasOne(d => d.Note).WithMany(p => p.NoteSuggestions)
                .HasForeignKey(d => d.NoteId)
                .HasConstraintName("note_suggestions_ibfk_1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patients");

            entity.HasIndex(e => e.PsychologistId, "psychologist_id");

            entity.HasIndex(e => e.UserId, "user_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.Age)
                .HasColumnType("integer")
                .HasColumnName("age");
            entity.Property(e => e.AvatarUrl)
                .HasColumnType("text")
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Diagnosis)
                .HasColumnType("text")
                .HasColumnName("diagnosis");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PsychologistId)
                .HasColumnType("integer")
                .HasColumnName("psychologist_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Activo'")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("integer")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.Patients)
                .HasForeignKey(d => d.PsychologistId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("patients_ibfk_2");

            entity.HasOne(d => d.User).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("patients_ibfk_1");
        });

        modelBuilder.Entity<PatientNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patient_notes");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PsychologistId, "psychologist_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.AiDiagnosis)
                .HasColumnType("text")
                .HasColumnName("ai_diagnosis");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Mood)
                .HasColumnType("smallint")
                .HasColumnName("mood");
            entity.Property(e => e.NeedsFollowUp)
                .HasDefaultValueSql("0")
                .HasColumnName("needs_follow_up");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.PsychologistId)
                .HasColumnType("integer")
                .HasColumnName("psychologist_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientNotes)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("patient_notes_ibfk_1");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.PatientNotes)
                .HasForeignKey(d => d.PsychologistId)
                .HasConstraintName("patient_notes_ibfk_2");
        });

        modelBuilder.Entity<PatientReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patient_reports");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PsychologistId, "psychologist_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.AnxietyLevel)
                .HasMaxLength(20)
                .HasColumnName("anxiety_level");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Diagnosis)
                .HasColumnType("text")
                .HasColumnName("diagnosis");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.PsychologistId)
                .HasColumnType("integer")
                .HasColumnName("psychologist_id");
            entity.Property(e => e.Recommendations)
                .HasColumnType("text")
                .HasColumnName("recommendations");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientReports)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("patient_reports_ibfk_1");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.PatientReports)
                .HasForeignKey(d => d.PsychologistId)
                .HasConstraintName("patient_reports_ibfk_2");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("prescriptions");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PsychologistId, "psychologist_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Dosage)
                .HasMaxLength(100)
                .HasColumnName("dosage");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Frequency)
                .HasMaxLength(100)
                .HasColumnName("frequency");
            entity.Property(e => e.Instructions)
                .HasColumnType("text")
                .HasColumnName("instructions");
            entity.Property(e => e.MedicationName)
                .HasMaxLength(255)
                .HasColumnName("medication_name");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.PsychologistId)
                .HasColumnType("integer")
                .HasColumnName("psychologist_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Activa'")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("prescriptions_ibfk_1");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PsychologistId)
                .HasConstraintName("prescriptions_ibfk_2");
        });

        modelBuilder.Entity<Psychologist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("psychologists");

            entity.HasIndex(e => e.CollegeNumber, "college_number").IsUnique();

            entity.HasIndex(e => e.UserId, "user_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CollegeNumber)
                .HasMaxLength(50)
                .HasColumnName("college_number");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Specialization)
                .HasColumnType("text")
                .HasColumnName("specialization");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("integer")
                .HasColumnName("user_id");
            entity.Property(e => e.WeeklyScore)
                .HasDefaultValueSql("0")
                .HasColumnType("integer")
                .HasColumnName("weekly_score");

            entity.HasOne(d => d.User).WithOne(p => p.Psychologist)
                .HasForeignKey<Psychologist>(d => d.UserId)
                .HasConstraintName("psychologists_ibfk_1");
        });

        modelBuilder.Entity<StressLevelsByTime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("stress_levels_by_time");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.PatientId)
                .HasColumnType("integer")
                .HasColumnName("patient_id");
            entity.Property(e => e.StressLevel)
                .HasColumnType("smallint")
                .HasColumnName("stress_level");
            entity.Property(e => e.TimeOfDay)
                .HasColumnType("time")
                .HasColumnName("time_of_day");

            entity.HasOne(d => d.Patient).WithMany(p => p.StressLevelsByTimes)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("stress_levels_by_time_ibfk_1");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("trainers");

            entity.HasIndex(e => e.UserId, "user_id").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.Bio)
                .HasColumnType("text")
                .HasColumnName("bio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Specialization)
                .HasColumnType("text")
                .HasColumnName("specialization");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasColumnType("integer")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Trainer)
                .HasForeignKey<Trainer>(d => d.UserId)
                .HasConstraintName("trainers_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("integer")
                .HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .HasColumnType("text")
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("1")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasColumnType("smallint")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
