public class PersonalSiteContext : DbContext
    {
        public PersonalSiteContext() : base("PersonalSiteContext")
        {
        }
        
       public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AttachConfig();
            modelBuilder.AttachDbSet();
            
        }
    }
