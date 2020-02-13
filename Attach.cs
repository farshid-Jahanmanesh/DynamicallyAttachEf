public static class Attach
    {
        public static void AttachConfig(this DbModelBuilder dbModel)
        {
            var addMethod = typeof(ConfigurationRegistrar)
           .GetMethods()
           .Single(m =>
            m.Name == "Add"
            && m.GetGenericArguments().Any(a => a.Name == "TEntityType"));

            var assembly = Assembly.GetExecutingAssembly();
            var configTypes = assembly
              .GetTypes()
              .Where(t => t.BaseType != null
                && t.BaseType.IsGenericType
                && t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in configTypes)
            {
                var entityType = type.BaseType.GetGenericArguments().Single();

                var entityConfig = assembly.CreateInstance(type.FullName);
                addMethod.MakeGenericMethod(entityType)
                  .Invoke(dbModel.Configurations, new object[] { entityConfig });
            }
        }

        //need to add DbSetAttachAttribute to all entity
        public static void AttachDbSet(this DbModelBuilder dbModel)
        {
            Assembly modelInAssembly = Assembly.GetExecutingAssembly();
            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity", new Type[] { });
            var exportedTypes = modelInAssembly.ExportedTypes;

            foreach (Type type in exportedTypes)
            {
                if (type.BaseType is System.Object && type.GetCustomAttributes().Any(n => n.GetType() == typeof(DbSetAttachAttribute)))
                {
                    entityMethod.MakeGenericMethod(type)
                        .Invoke(dbModel, new object[] { });
                }
            }
        }
    }
