using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Towditor.Web.EFModel
{
    public partial class TOWDContext : DbContext
    {
        public TOWDContext()
        {
        }

        public TOWDContext(DbContextOptions<TOWDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BitmapPixels> BitmapPixels { get; set; }
        public virtual DbSet<BitmapSequences> BitmapSequences { get; set; }
        public virtual DbSet<Bitmaps> Bitmaps { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Creatures> Creatures { get; set; }
        public virtual DbSet<Fonts> Fonts { get; set; }
        public virtual DbSet<GlyphPixels> GlyphPixels { get; set; }
        public virtual DbSet<Glyphs> Glyphs { get; set; }
        public virtual DbSet<Terrains> Terrains { get; set; }
        public virtual DbSet<TileRoles> TileRoles { get; set; }
        public virtual DbSet<WorldCreatures> WorldCreatures { get; set; }
        public virtual DbSet<WorldTerrains> WorldTerrains { get; set; }
        public virtual DbSet<Worlds> Worlds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TOWD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<BitmapPixels>(entity =>
            {
                entity.HasKey(e => e.BitmapPixelId);

                entity.HasIndex(e => new { e.BitmapId, e.X, e.Y })
                    .HasName("AK_BitmapPixels_BitmapId_X_Y")
                    .IsUnique();

                entity.HasOne(d => d.Bitmap)
                    .WithMany(p => p.BitmapPixels)
                    .HasForeignKey(d => d.BitmapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BitmapPixel_Bitmaps");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.BitmapPixels)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BitmapPixels_Colors");
            });

            modelBuilder.Entity<BitmapSequences>(entity =>
            {
                entity.HasKey(e => e.BitmapSequenceId);

                entity.HasIndex(e => e.BitmapSequenceName)
                    .HasName("AK_BitmapSequences_BitmapSequenceName")
                    .IsUnique();

                entity.Property(e => e.BitmapSequenceName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Bitmaps>(entity =>
            {
                entity.HasKey(e => e.BitmapId);

                entity.HasIndex(e => new { e.BitmapSequenceId, e.BitmapIndex })
                    .HasName("AK_Bitmaps_BitmapSequenceId_BitmapIndex")
                    .IsUnique();

                entity.HasOne(d => d.BitmapSequence)
                    .WithMany(p => p.Bitmaps)
                    .HasForeignKey(d => d.BitmapSequenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bitmaps_BitmapSequences");
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.Property(e => e.ColorId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Creatures>(entity =>
            {
                entity.HasKey(e => e.CreatureId);

                entity.HasIndex(e => e.CreatureName)
                    .HasName("AK_Creatures_CreatureName")
                    .IsUnique();

                entity.Property(e => e.CreatureName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Bitmap)
                    .WithMany(p => p.Creatures)
                    .HasForeignKey(d => d.BitmapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Creatures_Bitmaps");
            });

            modelBuilder.Entity<Fonts>(entity =>
            {
                entity.HasKey(e => e.FontId);

                entity.HasIndex(e => e.FontName)
                    .HasName("AK_Fonts_FontName")
                    .IsUnique();

                entity.Property(e => e.FontName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<GlyphPixels>(entity =>
            {
                entity.HasKey(e => e.GlyphPixelId);

                entity.HasIndex(e => new { e.GlyphId, e.X, e.Y })
                    .HasName("AK_GlyphPixels_GlyphId_X_Y")
                    .IsUnique();

                entity.HasOne(d => d.Glyph)
                    .WithMany(p => p.GlyphPixels)
                    .HasForeignKey(d => d.GlyphId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GlyphPixels_Glyphs");
            });

            modelBuilder.Entity<Glyphs>(entity =>
            {
                entity.HasKey(e => e.GlyphId);

                entity.HasIndex(e => new { e.FontId, e.GlyphCharacter })
                    .HasName("AK_Glyphs_FontId_GlyphName")
                    .IsUnique();

                entity.HasOne(d => d.Font)
                    .WithMany(p => p.Glyphs)
                    .HasForeignKey(d => d.FontId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Glyphs_Fonts");
            });

            modelBuilder.Entity<Terrains>(entity =>
            {
                entity.HasKey(e => e.TerrainId);

                entity.HasIndex(e => e.TerrainName)
                    .HasName("AK_Terrains_TerrainName")
                    .IsUnique();

                entity.Property(e => e.TerrainName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Bitmap)
                    .WithMany(p => p.Terrains)
                    .HasForeignKey(d => d.BitmapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Terrains_Bitmaps");

                entity.HasOne(d => d.TileRole)
                    .WithMany(p => p.Terrains)
                    .HasForeignKey(d => d.TileRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Terrains_TileRoles");
            });

            modelBuilder.Entity<TileRoles>(entity =>
            {
                entity.HasKey(e => e.TileRoleId);

                entity.Property(e => e.TileRoleId).ValueGeneratedNever();

                entity.Property(e => e.TileRoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<WorldCreatures>(entity =>
            {
                entity.HasKey(e => e.WorldCreatureId);

                entity.HasIndex(e => new { e.WorldId, e.CreatureId })
                    .HasName("AK_WorldCreatures_WorldId_CreatureId")
                    .IsUnique();

                entity.HasOne(d => d.Creature)
                    .WithMany(p => p.WorldCreatures)
                    .HasForeignKey(d => d.CreatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorldCreatures_Creatures");

                entity.HasOne(d => d.World)
                    .WithMany(p => p.WorldCreatures)
                    .HasForeignKey(d => d.WorldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorldCreatures_Worlds");
            });

            modelBuilder.Entity<WorldTerrains>(entity =>
            {
                entity.HasKey(e => e.WorldTerrainId);

                entity.HasIndex(e => new { e.WorldId, e.TerrainId })
                    .HasName("AK_WorldTerrains_WorldId_TerrainId")
                    .IsUnique();

                entity.HasOne(d => d.Terrain)
                    .WithMany(p => p.WorldTerrains)
                    .HasForeignKey(d => d.TerrainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorldTerrains_Terrains");

                entity.HasOne(d => d.World)
                    .WithMany(p => p.WorldTerrains)
                    .HasForeignKey(d => d.WorldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorldTerrains_Worlds");
            });

            modelBuilder.Entity<Worlds>(entity =>
            {
                entity.HasKey(e => e.WorldId);

                entity.HasIndex(e => e.WorldName)
                    .HasName("AK_Worlds_WorldName")
                    .IsUnique();

                entity.Property(e => e.WorldName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
