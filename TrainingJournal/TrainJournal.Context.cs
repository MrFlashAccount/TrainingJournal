﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainingJournal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Training_JournalEntities : DbContext
    {
        public Training_JournalEntities()
            : base("name=Training_JournalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TrainJournal> TrainJournal { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserAntropometry> UserAntropometry { get; set; }
        public virtual DbSet<Weight> Weight { get; set; }
    }
}
