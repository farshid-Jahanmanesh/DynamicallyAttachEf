public class PersonalSiteContext : DbContext
    {
        public PersonalSiteContext() : base("PersonalSiteContext")
        {
        }
        
        public override DbSet Set(Type entityType)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name ==entityType.Name);
            if (type != null)
                return base.Set(type);
            else
                return null;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AttachDbSet();
            modelBuilder.AttachConfig();
        }
    }
