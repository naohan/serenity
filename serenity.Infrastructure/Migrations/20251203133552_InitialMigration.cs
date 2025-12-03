using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace serenity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<short>(type: "smallint", nullable: false),
                    avatar_url = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "psychologists",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    college_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    specialization = table.Column<string>(type: "text", nullable: true),
                    weekly_score = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "psychologists_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    specialization = table.Column<string>(type: "text", nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "trainers_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    psychologist_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    avatar_url = table.Column<string>(type: "text", nullable: true),
                    age = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "'Activo'"),
                    diagnosis = table.Column<string>(type: "text", nullable: true),
                    diseases = table.Column<string>(type: "text", nullable: true),
                    medical_advice = table.Column<string>(type: "text", nullable: true),
                    initial_objective = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "patients_ibfk_1",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "patients_ibfk_2",
                        column: x => x.psychologist_id,
                        principalTable: "psychologists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    psychologist_id = table.Column<int>(type: "integer", nullable: false),
                    appointment_date = table.Column<DateOnly>(type: "date", nullable: false),
                    appointment_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "'Pendiente'"),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "appointments_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "appointments_ibfk_2",
                        column: x => x.psychologist_id,
                        principalTable: "psychologists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chat_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    psychologist_id = table.Column<int>(type: "integer", nullable: false),
                    message_text = table.Column<string>(type: "text", nullable: false),
                    is_from_psychologist = table.Column<bool>(type: "boolean", nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    read_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "chat_messages_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "chat_messages_ibfk_2",
                        column: x => x.psychologist_id,
                        principalTable: "psychologists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "daily_moods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    mood = table.Column<short>(type: "smallint", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "daily_moods_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emotional_states",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    emotional_state = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    value = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "emotional_states_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "insights_and_recommendations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    psychologist_id = table.Column<int>(type: "integer", nullable: true),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "insights_and_recommendations_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "insights_and_recommendations_ibfk_2",
                        column: x => x.psychologist_id,
                        principalTable: "psychologists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "meditation_sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    session_date = table.Column<DateOnly>(type: "date", nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "meditation_sessions_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mental_wellbeing_metrics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    stress_level = table.Column<short>(type: "smallint", nullable: true),
                    energy_level = table.Column<short>(type: "smallint", nullable: true),
                    concentration_level = table.Column<short>(type: "smallint", nullable: true),
                    satisfaction_level = table.Column<short>(type: "smallint", nullable: true),
                    sleep_duration = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: true),
                    sleep_quality = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    meditation_minutes = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
                    meditation_sessions = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "mental_wellbeing_metrics_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mood_metrics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    happy_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    calm_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    sad_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    anxious_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "mood_metrics_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patient_notes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    psychologist_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    mood = table.Column<short>(type: "smallint", nullable: false),
                    ai_diagnosis = table.Column<string>(type: "text", nullable: true),
                    needs_follow_up = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "0"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "patient_notes_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "patient_notes_ibfk_2",
                        column: x => x.psychologist_id,
                        principalTable: "psychologists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patient_reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    psychologist_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    diagnosis = table.Column<string>(type: "text", nullable: false),
                    anxiety_level = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    recommendations = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "patient_reports_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "patient_reports_ibfk_2",
                        column: x => x.psychologist_id,
                        principalTable: "psychologists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    psychologist_id = table.Column<int>(type: "integer", nullable: false),
                    medication_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dosage = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    frequency = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    instructions = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "'Activa'"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "prescriptions_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "prescriptions_ibfk_2",
                        column: x => x.psychologist_id,
                        principalTable: "psychologists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stress_levels_by_time",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    time_of_day = table.Column<TimeOnly>(type: "time", nullable: false),
                    stress_level = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "stress_levels_by_time_ibfk_1",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "note_suggestions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    note_id = table.Column<int>(type: "integer", nullable: false),
                    suggestion = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "note_suggestions_ibfk_1",
                        column: x => x.note_id,
                        principalTable: "patient_notes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "patient_id",
                table: "appointments",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "psychologist_id",
                table: "appointments",
                column: "psychologist_id");

            migrationBuilder.CreateIndex(
                name: "patient_id1",
                table: "chat_messages",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "psychologist_id1",
                table: "chat_messages",
                column: "psychologist_id");

            migrationBuilder.CreateIndex(
                name: "patient_id2",
                table: "daily_moods",
                columns: new[] { "patient_id", "date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "patient_id3",
                table: "emotional_states",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "patient_id4",
                table: "insights_and_recommendations",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "psychologist_id2",
                table: "insights_and_recommendations",
                column: "psychologist_id");

            migrationBuilder.CreateIndex(
                name: "patient_id5",
                table: "meditation_sessions",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "patient_id6",
                table: "mental_wellbeing_metrics",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "patient_id7",
                table: "mood_metrics",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "note_id",
                table: "note_suggestions",
                column: "note_id");

            migrationBuilder.CreateIndex(
                name: "patient_id8",
                table: "patient_notes",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "psychologist_id4",
                table: "patient_notes",
                column: "psychologist_id");

            migrationBuilder.CreateIndex(
                name: "patient_id9",
                table: "patient_reports",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "psychologist_id5",
                table: "patient_reports",
                column: "psychologist_id");

            migrationBuilder.CreateIndex(
                name: "psychologist_id3",
                table: "patients",
                column: "psychologist_id");

            migrationBuilder.CreateIndex(
                name: "user_id",
                table: "patients",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "patient_id10",
                table: "prescriptions",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "psychologist_id6",
                table: "prescriptions",
                column: "psychologist_id");

            migrationBuilder.CreateIndex(
                name: "college_number",
                table: "psychologists",
                column: "college_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_id1",
                table: "psychologists",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "patient_id11",
                table: "stress_levels_by_time",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "user_id2",
                table: "trainers",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "chat_messages");

            migrationBuilder.DropTable(
                name: "daily_moods");

            migrationBuilder.DropTable(
                name: "emotional_states");

            migrationBuilder.DropTable(
                name: "insights_and_recommendations");

            migrationBuilder.DropTable(
                name: "meditation_sessions");

            migrationBuilder.DropTable(
                name: "mental_wellbeing_metrics");

            migrationBuilder.DropTable(
                name: "mood_metrics");

            migrationBuilder.DropTable(
                name: "note_suggestions");

            migrationBuilder.DropTable(
                name: "patient_reports");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "stress_levels_by_time");

            migrationBuilder.DropTable(
                name: "trainers");

            migrationBuilder.DropTable(
                name: "patient_notes");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "psychologists");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
