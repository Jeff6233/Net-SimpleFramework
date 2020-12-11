using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Domain.Repository
{
    public interface IT_InputVGMRepository : IRepositoryBase<T_InputVGM>
    {

    }
    public class T_InputVGMRepository:RepositoryBase<T_InputVGM>, IT_InputVGMRepository
    {
        public T_InputVGMRepository(WebAppContext db) : base(db) { }
        protected override Expression<Func<T_InputVGM, bool>> ReadExpression()
        {
            return i => true;
        }
    }
}
