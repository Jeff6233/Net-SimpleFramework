using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class T_InputVGM
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DoNo { get; set; }
        public string vgmCtnNo { get; set; }
        public string vgmSealNo { get; set; }
        public string vgmCtnType { get; set; }
        public string vgmMethod { get; set; }
        public decimal vgmWeight { get; set; }
        public string vgmCompany { get; set; }
        public string vgmDirector { get; set; }
        public string vgmEmail { get; set; }
        public string vgmPhone { get; set; }
    }
}
