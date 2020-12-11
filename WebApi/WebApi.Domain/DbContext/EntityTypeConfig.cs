using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApi.Data.Map;

namespace WebApi.Data
{
    public class EntityTypeConfig
    {
        private DbModelBuilder modelBuilder;

        public EntityTypeConfig(DbModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Mapping()
        {
            this.modelBuilder.Configurations.Add(new T_InputVGMMap());
        }
    }
}