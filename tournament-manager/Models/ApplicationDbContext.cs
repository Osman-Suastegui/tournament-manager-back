using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tournament_manager.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<MatchesPlayer> MatchesPlayers { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayersTournament> PlayersTournaments { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }

    public virtual DbSet<TeamTournamentLink> TeamTournamentLinks { get; set; }

    public virtual DbSet<TeamsTournament> TeamsTournaments { get; set; }

    public virtual DbSet<TestEntity> TestEntities { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<UserTournament> UserTournaments { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=basket;Username=postgres;Password=osman");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("matches_pkey");

            entity.ToTable("matches");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaInicio)
                .HasPrecision(6)
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IsDraw)
                .HasDefaultValue(false)
                .HasColumnName("is_draw");
            entity.Property(e => e.NextMatchId).HasColumnName("next_match_id");
            entity.Property(e => e.RefereeId).HasColumnName("referee_id");
            entity.Property(e => e.Round).HasColumnName("round");
            entity.Property(e => e.Team1Id).HasColumnName("team1_id");
            entity.Property(e => e.Team2Id).HasColumnName("team2_id");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");
            entity.Property(e => e.WinnerId).HasColumnName("winner_id");

            entity.HasOne(d => d.NextMatch).WithMany(p => p.InverseNextMatch)
                .HasForeignKey(d => d.NextMatchId)
                .HasConstraintName("fk7tafcn2nqamuh4kq2fbhju8yc");

            entity.HasOne(d => d.Referee).WithMany(p => p.Matches)
                .HasForeignKey(d => d.RefereeId)
                .HasConstraintName("fk9uo1neoqxrcrfh2t3et2xeyud");

            entity.HasOne(d => d.Team1).WithMany(p => p.MatchTeam1s)
                .HasForeignKey(d => d.Team1Id)
                .HasConstraintName("fk3ioil1py4fu8omd77sivakcwi");

            entity.HasOne(d => d.Team2).WithMany(p => p.MatchTeam2s)
                .HasForeignKey(d => d.Team2Id)
                .HasConstraintName("fkdkphr8xw4l2dgywsnbdbe04d7");

            entity.HasOne(d => d.Tournament).WithMany(p => p.Matches)
                .HasForeignKey(d => d.TournamentId)
                .HasConstraintName("fkeeniokyjgo5k6rmhjujatn27i");

            entity.HasOne(d => d.Winner).WithMany(p => p.MatchWinners)
                .HasForeignKey(d => d.WinnerId)
                .HasConstraintName("fkbeel88lh2ksupphafotnqy7ry");
        });

        modelBuilder.Entity<MatchesPlayer>(entity =>
        {
            entity.HasKey(e => e.Clave).HasName("matches_players_pkey");

            entity.ToTable("matches_players");

            entity.Property(e => e.Clave).HasColumnName("clave");
            entity.Property(e => e.Anotaciones).HasColumnName("anotaciones");
            entity.Property(e => e.Asistencias).HasColumnName("asistencias");
            entity.Property(e => e.ClavePartido).HasColumnName("clave_partido");
            entity.Property(e => e.EnBanca).HasColumnName("en_banca");
            entity.Property(e => e.Equipo)
                .HasMaxLength(255)
                .HasColumnName("equipo");
            entity.Property(e => e.Faltas).HasColumnName("faltas");
            entity.Property(e => e.Jugador).HasColumnName("jugador");
            entity.Property(e => e.Posicion)
                .HasMaxLength(255)
                .HasColumnName("posicion");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.ClavePartidoNavigation).WithMany(p => p.MatchesPlayers)
                .HasForeignKey(d => d.ClavePartido)
                .HasConstraintName("fk6nm2eva9wxyh4yv3dsxpe8498");

            entity.HasOne(d => d.JugadorNavigation).WithMany(p => p.MatchesPlayers)
                .HasForeignKey(d => d.Jugador)
                .HasConstraintName("fkay6weeqj318hm4ptkwlyu5hsg");

            entity.HasOne(d => d.Team).WithMany(p => p.MatchesPlayers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("fk78fi67rgdwv05w21k1mov5ai1");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("players_pkey");

            entity.ToTable("players");

            entity.HasIndex(e => e.UserId, "uk_r2bdiqerficgwok3omt8rwlxb").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Player)
                .HasForeignKey<Player>(d => d.UserId)
                .HasConstraintName("fkmahprftgklmbaqwejvtq3dug4");
        });

        modelBuilder.Entity<PlayersTournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("players_tournaments_pkey");

            entity.ToTable("players_tournaments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.TeamTournamentId).HasColumnName("team_tournament_id");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayersTournaments)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("fksg8puqejlcpvgbea040pb8hk3");

            entity.HasOne(d => d.TeamTournament).WithMany(p => p.PlayersTournaments)
                .HasForeignKey(d => d.TeamTournamentId)
                .HasConstraintName("fkjlw8gkjbf8swje5r771vh155t");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teams_pkey");

            entity.ToTable("teams");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminEquipo).HasColumnName("admin_equipo");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("logo");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");

            entity.HasOne(d => d.AdminEquipoNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.AdminEquipo)
                .HasConstraintName("fkdyofc6dpm7j9uc3d3g4i7e752");
        });

        modelBuilder.Entity<TeamPlayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_players_pkey");

            entity.ToTable("team_players");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.Position)
                .HasMaxLength(255)
                .HasColumnName("position");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Player).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("fket8tm42jed7jrtxyrpur5d3ct");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("fk3bhsykltbdhsmmb61l2ml12h");
        });

        modelBuilder.Entity<TeamTournamentLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("team_tournament_link_pkey");

            entity.ToTable("team_tournament_link");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.TeamTournamentId).HasColumnName("team_tournament_id");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");

            entity.HasOne(d => d.TeamTournament).WithMany(p => p.TeamTournamentLinks)
                .HasForeignKey(d => d.TeamTournamentId)
                .HasConstraintName("fk891mgsf0qbt9q0cqhdlb9wfil");
        });

        modelBuilder.Entity<TeamsTournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teams_tournaments_pkey");

            entity.ToTable("teams_tournaments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamsTournaments)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("fkn2bkst9dp1wdy7heg8nt2ao10");

            entity.HasOne(d => d.Tournament).WithMany(p => p.TeamsTournaments)
                .HasForeignKey(d => d.TournamentId)
                .HasConstraintName("fk24s810yw359703h577ys1iiw4");
        });

        modelBuilder.Entity<TestEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_entity_pkey");

            entity.ToTable("test_entity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeveloper).HasColumnName("is_developer");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tournaments_pkey");

            entity.ToTable("tournaments");

            entity.HasIndex(e => e.Name, "uk_toqueen5yq82q1kyuo8x9u7rh").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadEnfrentamientosRegular).HasColumnName("cantidad_enfrentamientos_regular");
            entity.Property(e => e.CantidadEquipos).HasColumnName("cantidad_equipos");
            entity.Property(e => e.CantidadPlayoffs).HasColumnName("cantidad_playoffs");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .HasColumnName("estado");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Rules)
                .HasMaxLength(255)
                .HasColumnName("rules");
            entity.Property(e => e.Sport)
                .HasMaxLength(255)
                .HasColumnName("sport");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TournamentType).HasColumnName("tournament_type");
        });

        modelBuilder.Entity<UserTournament>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_tournament_pkey");

            entity.ToTable("user_tournament");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .HasColumnName("role");
            entity.Property(e => e.TournamentId).HasColumnName("tournament_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Tournament).WithMany(p => p.UserTournaments)
                .HasForeignKey(d => d.TournamentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkfxknrygroocyqvc6i90bucacb");

            entity.HasOne(d => d.User).WithMany(p => p.UserTournaments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fktkeoayemdydgh2u27i2ay7r1u");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "uk_kfsp0s1tflm1cwlj8idhqsad0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(255)
                .HasColumnName("usuario");
        });
        modelBuilder.HasSequence("team_tournament_link_seq").IncrementsBy(50);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
