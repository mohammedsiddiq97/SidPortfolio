using Microsoft.EntityFrameworkCore;
using SidPortfolio.Models;

namespace SidPortfolio.DBContext
{
    public class MyDBContext :DbContext
    {
  
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
        public DbSet<CertificationModel> Certifications { get; set; }
        public DbSet<ContactUsModel> ContactUs { get; set; }
        public DbSet<CertificationAssociationModel> CertificationAssociation { get; set; }
        public DbSet<ProjectModel> ProjectFile { get; set; }
        public DbSet<ProjectAssociationModel> ProjectAssociation { get; set; }
        public DbSet<TechnicalSkillModel> TechnicalSkill { get; set; }
        public DbSet<ExperienceModel> Experience { get; set; }
        public DbSet<ExperienceResponsibilitiesAssociationModel> ExperienceResponsibilitiesAssociation { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure
            base.OnModelCreating(modelBuilder);
            // Map entities to tables
            modelBuilder.Entity<ContactUsModel>().ToTable("ContactUs");
            modelBuilder.Entity<TechnicalSkillModel>().ToTable("TechnicalSkill");
            modelBuilder.Entity<CertificationModel>()
                    .HasOne(p => p.CertificationAssociation)
                    .WithOne(pa => pa.certificationModel)
                    .HasForeignKey<CertificationAssociationModel>(pa => pa.CertificationId);

            modelBuilder.Entity<ProjectModel>()
                       .HasOne(p => p.ProjectAssociation)
                       .WithOne(pa => pa.ProjectModel)
                       .HasForeignKey<ProjectAssociationModel>(pa => pa.ProjectId);
            modelBuilder.Entity<ExperienceResponsibilitiesAssociationModel>()
                    .HasOne(e => e.ExperienceModel)
                    .WithMany(e => e.ExperienceResponsibilitiesAssociation)
                    .HasForeignKey(e => e.ExperienceId);




            //// Configure columns
            //modelBuilder.Entity<CertificationModel>().Property(ug => ug.CertificationId).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            //modelBuilder.Entity<CertificationModel>().Property(ug => ug.FileName).HasColumnType("nvarchar(100)").IsRequired();
            //modelBuilder.Entity<CertificationModel>().Property(ug => ug.ContentType).HasColumnType("nvarchar(100)").IsRequired();
            //modelBuilder.Entity<CertificationModel>().Property(ug => ug.Size).HasColumnType("BigInt").IsRequired();
            //modelBuilder.Entity<CertificationModel>().Property(ug => ug.ActiveStatus).HasColumnType("bit").IsRequired();
            //modelBuilder.Entity<CertificationModel>().Property(ug => ug.CreationDateTime).HasColumnType("datetime").IsRequired();
            //modelBuilder.Entity<CertificationModel>().Property(ug => ug.LastUpdateDateTime).HasColumnType("datetime").IsRequired(false);

            //modelBuilder.Entity<CertificationAssociationModel>().Property(ug => ug.CertificationAssociationId).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            //modelBuilder.Entity<CertificationAssociationModel>().Property(ug => ug.Title).HasColumnType("nvarchar(100)").IsRequired();
            //modelBuilder.Entity<CertificationAssociationModel>().Property(ug => ug.IssuingOrganization).HasColumnType("nvarchar(100)").IsRequired();
            //modelBuilder.Entity<CertificationAssociationModel>().Property(ug => ug.Description).HasColumnType("nvarchar(500)").IsRequired();
            //modelBuilder.Entity<CertificationAssociationModel>().Property(ug => ug.ActiveStatus).HasColumnType("bit").IsRequired();
            //modelBuilder.Entity<CertificationAssociationModel>().Property(ug => ug.CreationDateTime).HasColumnType("datetime").IsRequired();
            //modelBuilder.Entity<CertificationAssociationModel>().Property(ug => ug.LastUpdateDateTime).HasColumnType("datetime").IsRequired(false);
            //modelBuilder.Entity<CertificationAssociationModel>().HasOne<CertificationModel>().WithMany().HasPrincipalKey(ug => ug.CertificationId).HasForeignKey(u => u.CertificationId)
            //    .OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Certifications_FileId");



            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.Name).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.Email).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.PhoneNumber).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.Description).HasColumnType("nvarchar(300)").IsRequired(false);
            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.ActiveStatus).HasColumnType("bit").IsRequired();
            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.CreationDateTime).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<ContactUsModel>().Property(ug => ug.LastUpdateDateTime).HasColumnType("datetime").IsRequired();


            modelBuilder.Entity<TechnicalSkillModel>().Property(ug => ug.TechnicalSkillId).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<TechnicalSkillModel>().Property(ug => ug.SkillName).HasColumnType("nvarchar(100)").IsRequired(true);
            modelBuilder.Entity<TechnicalSkillModel>().Property(ug => ug.ActiveStatus).HasColumnType("bit").IsRequired();
            modelBuilder.Entity<TechnicalSkillModel>().Property(ug => ug.CreationDateTime).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<TechnicalSkillModel>().Property(ug => ug.LastUpdateDateTime).HasColumnType("datetime").IsRequired();


        }
    }
}
