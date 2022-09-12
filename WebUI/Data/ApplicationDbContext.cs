using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Data
{
    public partial class ApplicationDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, string>
        where TUser : IdentityUser
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext<TUser>> options)
            : base(options)
        {
        }

        //public virtual DbSet<Client> Clients { get; set; }
        //public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ContentDetails> ContentDetails { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<CPLConnection> CPLConnections { get; set; }
        public virtual DbSet<Standard> Standards { get; set; }
        public virtual DbSet<UserGrade> UserGrades { get; set; }
        public virtual DbSet<UserSubject> UserSubjects { get; set; }
        public virtual DbSet<ContentGrade> ContentGrades { get; set; }
        public virtual DbSet<ContentSubject> ContentSubjects { get; set; }
        public virtual DbSet<ContentTopic> ContentTopics { get; set; }
        public virtual DbSet<ContentStandard> ContentStandards { get; set; }
        public virtual DbSet<ContentFiles> ContentFiles { get; set; }
        public virtual DbSet<ContentAuthor> ContentAuthors { get; set; }
        public virtual DbSet<ContentDerivation> ContentDerivations { get; set; }
        public virtual DbSet<ContentExternalLink> ContentExternalLinks { get; set; }
        public virtual DbSet<ContentBookmark> ContentBookmarks { get; set; }
        public virtual DbSet<ContentComment> ContentComments { get; set; }
        public virtual DbSet<ContentRating> ContentRatings { get; set; }

        public virtual DbSet<ContentQuestion> ContentQuestions { get; set; }
        public virtual DbSet<ModerationComment> ModerationComments { get; set; }

        public virtual DbSet<ContactUs> ContactUsRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // call base.OnModelCreating so Identity Models are configured properly
            base.OnModelCreating(builder);
            // EF core generally lets the last called win, so override after calling the base method

           
            builder.Entity<Company>(entity =>
            {
                entity.OwnsOne(x => x.CompanyAddress, y =>
                {
                    y.Property(z => z.CompanyAddressLine1).IsRequired();
                    y.Property(z => z.CompanyCity).IsRequired();
                    y.Property(z => z.CompanyState).IsRequired();
                    y.Property(z => z.CompanyZipCode).IsRequired();
                });
            });
            builder.Entity<ContentDetails>(entity =>
            {
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.ContentCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ContentDetails$tblAspNetUsersCreatedBy");

                entity.HasOne(d => d.ChangedBy)
                    .WithMany(p => p.ContentChangedBy)
                    .HasForeignKey(d => d.ChangedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ContentDetails$tblAspNetUsersChangedBy");
            });
            builder.Entity<Grade>(entity =>
            {
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.GradeCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Grade$tblAspNetUsersCreatedBy");

                entity.HasOne(d => d.ChangedBy)
                    .WithMany(p => p.GradeChangedBy)
                    .HasForeignKey(d => d.ChangedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Grade$tblAspNetUsersChangedBy");
            });
            builder.Entity<Subject>(entity =>
            {
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.SubjectCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subject$tblAspNetUsersCreatedBy");

                entity.HasOne(d => d.ChangedBy)
                    .WithMany(p => p.SubjectChangedBy)
                    .HasForeignKey(d => d.ChangedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subject$tblAspNetUsersChangedBy");
            });
            builder.Entity<Topic>(entity =>
            {
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.TopicCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Topic$tblAspNetUsersCreatedBy");

                entity.HasOne(d => d.ChangedBy)
                    .WithMany(p => p.TopicChangedBy)
                    .HasForeignKey(d => d.ChangedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Topic$tblAspNetUsersChangedBy");
            });
            builder.Entity<CPLConnection>(entity =>
            {
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.CPLConnectionCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CPLConnection$tblAspNetUsersCreatedBy");

                entity.HasOne(d => d.ChangedBy)
                    .WithMany(p => p.CPLConnectionChangedBy)
                    .HasForeignKey(d => d.ChangedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CPLConnection$tblAspNetUsersChangedBy");
            });
            builder.Entity<Standard>(entity =>
            {
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.StandardCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Standard$tblAspNetUsersCreatedBy");

                entity.HasOne(d => d.ChangedBy)
                    .WithMany(p => p.StandardChangedBy)
                    .HasForeignKey(d => d.ChangedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Standard$tblAspNetUsersChangedBy");
            });
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.UserCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ApplicationUser$tblAspNetUsersCreatedBy");

                entity.HasOne(d => d.ChangedBy)
                    .WithMany(p => p.UserChangedBy)
                    .HasForeignKey(d => d.ChangedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ApplicationUser$tblAspNetUsersChangedBy");
            });
            builder.Entity<UserGrade>(entity =>
            {
                builder.Entity<UserGrade>()
                    .HasKey(d => new {d.UserId, d.GradeId });

                builder.Entity<UserGrade>()
                    .HasOne(p => p.User)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.UserId);

                builder.Entity<UserGrade>()
                    .HasOne(d => d.Grade)
                    .WithMany(p => p.UserGrades)
                    .HasForeignKey(d => d.GradeId);
            });
            builder.Entity<UserSubject>(entity =>
            {
                builder.Entity<UserSubject>()
                    .HasKey(d => new { d.UserId, d.SubjectId });

                builder.Entity<UserSubject>()
                    .HasOne(p => p.User)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.UserId);

                builder.Entity<UserSubject>()
                    .HasOne(d => d.Subject)
                    .WithMany(p => p.UserSubjects)
                    .HasForeignKey(d => d.SubjectId);
            });


            builder.Entity<ContentDerivation>(entity =>
            {
                builder.Entity<ContentDerivation>()
                    .HasKey(d => new { d.ContentDetailsId, d.DerivationId });

                builder.Entity<ContentDerivation>()
                    .HasOne(p => p.ContentDetails)
                    .WithMany(p => p.Derivations)
                    .HasForeignKey(d => d.ContentDetailsId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.Entity<ContentDerivation>()
                    .HasOne(d => d.Derivation)
                    .WithMany(p => p.ContentDerivations)
                    .HasForeignKey(d => d.DerivationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<ContentGrade>(entity =>
            {
                builder.Entity<ContentGrade>()
                    .HasKey(d => new { d.ContentDetailsId, d.GradeId });

                builder.Entity<ContentGrade>()
                    .HasOne(p => p.ContentDetails)
                    .WithMany(p => p.ContentGrades)
                    .HasForeignKey(d => d.ContentDetailsId);

                builder.Entity<ContentGrade>()
                    .HasOne(d => d.Grade)
                    .WithMany(p => p.ContentGrades)
                    .HasForeignKey(d => d.GradeId);
            });
            builder.Entity<ContentSubject>(entity =>
            {
                builder.Entity<ContentSubject>()
                    .HasKey(d => new { d.ContentDetailsId, d.SubjectId });

                builder.Entity<ContentSubject>()
                    .HasOne(p => p.ContentDetails)
                    .WithMany(p => p.ContentSubjects)
                    .HasForeignKey(d => d.ContentDetailsId);

                builder.Entity<ContentSubject>()
                    .HasOne(d => d.Subject)
                    .WithMany(p => p.ContentSubjects)
                    .HasForeignKey(d => d.SubjectId);
            });
            builder.Entity<ContentTopic>(entity =>
            {
                builder.Entity<ContentTopic>()
                    .HasKey(d => new { d.ContentDetailsId, d.TopicId });

                builder.Entity<ContentTopic>()
                    .HasOne(p => p.ContentDetails)
                    .WithMany(p => p.ContentTopics)
                    .HasForeignKey(d => d.ContentDetailsId);

                builder.Entity<ContentTopic>()
                    .HasOne(d => d.Topic)
                    .WithMany(p => p.ContentTopics)
                    .HasForeignKey(d => d.TopicId);
            });
            builder.Entity<ContentStandard>(entity =>
            {
                builder.Entity<ContentStandard>()
                    .HasKey(d => new { d.ContentDetailsId, d.StandardId });

                builder.Entity<ContentStandard>()
                    .HasOne(p => p.ContentDetails)
                    .WithMany(p => p.ContentStandards)
                    .HasForeignKey(d => d.ContentDetailsId);

                builder.Entity<ContentStandard>()
                    .HasOne(d => d.Standard)
                    .WithMany(p => p.ContentStandards)
                    .HasForeignKey(d => d.StandardId);
            });
            builder.Entity<ContentBookmark>(entity =>
            {
                builder.Entity<ContentBookmark>()
                    .HasKey(bc => new { bc.ContentDetailsId, bc.UserId });

                builder.Entity<ContentBookmark>()
                    .HasOne(bc => bc.ContentDetails)
                    .WithMany(b => b.ContentBookmarks)
                    .HasForeignKey(bc => bc.ContentDetailsId);

                builder.Entity<ContentBookmark>()
                    .HasOne(bc => bc.User)
                    .WithMany(c => c.ContentBookmarks)
                    .HasForeignKey(bc => bc.UserId);
            });
            //builder.Entity<ContentAuthor>(entity =>
            //{
            //    builder.Entity<ContentAuthor>()
            //        .HasKey(d => new { d.ContentId, d.AuthorId });

            //    builder.Entity<ContentAuthor>()
            //        .HasOne(p => p.Content)
            //        .WithMany(p => p.CoAuthors)
            //        .HasForeignKey(d => d.ContentId);

            //    builder.Entity<ContentAuthor>()
            //        .HasOne(d => d.Author)
            //        .WithMany(p => p.CoAuthors)
            //        .HasForeignKey(d => d.AuthorId);
            //});
            OnModelCreatingPartial(builder);
        }

        /// <summary>
        /// Detach an entity from this context to stop tracking
        /// </summary>
        public void Detach(object entity)
        {
            if (entity == null) return;

            Entry(entity).State = EntityState.Detached;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}