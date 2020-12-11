using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Data.Map
{
    public class T_InputVGMMap:EntityTypeConfiguration<T_InputVGM>
    {
        public T_InputVGMMap()
        {
            this.ToTable("T_InputVGM");
            this.HasKey(i => i.Id);
            this.Property(i => i.vgmCtnNo).HasColumnName("vgmCtnNo");
            this.Property(i => i.vgmSealNo).HasColumnName("vgmSealNo");
            this.Property(i => i.vgmCtnType).HasColumnName("vgmCtnType");
            this.Property(i => i.vgmMethod).HasColumnName("vgmMethod");
            this.Property(i => i.vgmWeight).HasColumnName("vgmWeight");
            this.Property(i => i.vgmCompany).HasColumnName("vgmCompany");
            this.Property(i => i.vgmDirector).HasColumnName("vgmDirector");
            this.Property(i => i.vgmEmail).HasColumnName("vgmEmail");
            this.Property(i => i.vgmPhone).HasColumnName("vgmPhone");
        }
    }
}
