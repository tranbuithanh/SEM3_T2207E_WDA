using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CTsinhvien.Models;

public partial class Sem3MvcContext : DbContext
{
    public Sem3MvcContext()
    {
    }

    public Sem3MvcContext(DbContextOptions<Sem3MvcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Classcourse> Classcourses { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subjectcla> Subjectclas { get; set; }

    public virtual DbSet<Subjectclass> Subjectclasses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-V21PR5VB\\SQLEXPRESS;Database=SEM3_MVC;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK_CLASS");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId)
                .ValueGeneratedNever()
                .HasColumnName("classId");
            entity.Property(e => e.Classname)
                .HasMaxLength(20)
                .HasColumnName("classname");
        });

        modelBuilder.Entity<Classcourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("classcourse");

            entity.Property(e => e.Clsroomid).HasColumnName("clsroomid");
            entity.Property(e => e.Coursedate)
                .HasColumnType("date")
                .HasColumnName("coursedate");
            entity.Property(e => e.Coursetime).HasColumnName("coursetime");
            entity.Property(e => e.Subjectclsid).HasColumnName("subjectclsid");

            entity.HasOne(d => d.Clsroom).WithMany()
                .HasForeignKey(d => d.Clsroomid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("classcourse_fk1");

            entity.HasOne(d => d.Subjectcls).WithMany()
                .HasForeignKey(d => d.Subjectclsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("classcourse_fk0");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Clsroomid).HasName("PK_CLASSROOM");

            entity.ToTable("Classroom");

            entity.Property(e => e.Clsroomid)
                .ValueGeneratedNever()
                .HasColumnName("clsroomid");
            entity.Property(e => e.Clsroomname)
                .HasMaxLength(1)
                .HasColumnName("clsroomname");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Exid).HasName("PK_EXAM");

            entity.ToTable("Exam");

            entity.Property(e => e.Exid)
                .ValueGeneratedNever()
                .HasColumnName("exid");
            entity.Property(e => e.Clsroomid).HasColumnName("clsroomid");
            entity.Property(e => e.Excourse).HasColumnName("excourse");
            entity.Property(e => e.Exdate)
                .HasColumnType("date")
                .HasColumnName("exdate");
            entity.Property(e => e.Exresult).HasColumnName("exresult");
            entity.Property(e => e.Extime).HasColumnName("extime");
            entity.Property(e => e.Studentid).HasColumnName("studentid");
            entity.Property(e => e.Subjectclsid).HasColumnName("subjectclsid");

            entity.HasOne(d => d.Clsroom).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Clsroomid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Exam_fk2");

            entity.HasOne(d => d.Student).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Exam_fk0");

            entity.HasOne(d => d.Subjectcls).WithMany(p => p.Exams)
                .HasForeignKey(d => d.Subjectclsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Exam_fk1");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Person__CB9A1CDFD1B28853");

            entity.ToTable("Person");

            entity.HasIndex(e => e.Tel, "UQ__Person__DC107AB3FDF4E97A").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tel");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("PK_STUDENT");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Studenttel, "UQ__Student__3CE6C64F0F4A1546").IsUnique();

            entity.Property(e => e.Studentid)
                .ValueGeneratedNever()
                .HasColumnName("studentid");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Studentadress)
                .HasColumnType("ntext")
                .HasColumnName("studentadress");
            entity.Property(e => e.Studentbirth)
                .HasColumnType("date")
                .HasColumnName("studentbirth");
            entity.Property(e => e.Studentname)
                .HasMaxLength(20)
                .HasColumnName("studentname");
            entity.Property(e => e.Studenttel)
                .HasMaxLength(10)
                .HasColumnName("studenttel");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Student_fk0");
        });

        modelBuilder.Entity<Subjectcla>(entity =>
        {
            entity.HasKey(e => e.Subjectclsid).HasName("PK_SUBJECTCLAS");

            entity.Property(e => e.Subjectclsid)
                .ValueGeneratedNever()
                .HasColumnName("subjectclsid");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.Subjectname)
                .HasMaxLength(1)
                .HasColumnName("subjectname");
        });

        modelBuilder.Entity<Subjectclass>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("subjectclass");

            entity.Property(e => e.Studentid).HasColumnName("studentid");
            entity.Property(e => e.Subjectclsid).HasColumnName("subjectclsid");

            entity.HasOne(d => d.Student).WithMany()
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subjectclass_fk1");

            entity.HasOne(d => d.Subjectcls).WithMany()
                .HasForeignKey(d => d.Subjectclsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subjectclass_fk0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
