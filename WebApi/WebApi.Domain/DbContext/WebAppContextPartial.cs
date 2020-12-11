using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WebApi.Data;

namespace WebApi.Domain
{
    public class WebAppContext : DbContext
    {
        public WebAppContext() : base(ConfigurationManager.ConnectionStrings["WebApplication1Context"].ConnectionString)
        {
           
        }

        //public override int SaveChanges()
        //{
        //    int result = 0;
        //    using (TransactionScope scope=new TransactionScope())
        //    {
        //        try
        //        {
        //            result=base.SaveChanges();
        //            scope.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            scope.Dispose();
        //        }
        //    }
        //    return result;
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new EntityTypeConfig(modelBuilder).Mapping();
            base.OnModelCreating(modelBuilder);
        }
    }
}
