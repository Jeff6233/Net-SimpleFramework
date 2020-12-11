using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Domain.Repository;

namespace WebApi.Service
{
    public interface IT_InputVGMService
    {
        (List<T_InputVGM> list, int total) List(int pageIndex, int pageSize);
        void Add(T_InputVGM t_InputVGM);
        void AddRange(List<T_InputVGM> t_InputVGMs);
        void Delete(T_InputVGM t_InputVGM);
        void Update(T_InputVGM t_InputVGM);

    }
    public class T_InputVGMService : IT_InputVGMService
    {
        private IT_InputVGMRepository t_InputVGMRepository;
        private IUnityInjector unity;

        public T_InputVGMService(IT_InputVGMRepository inputVGMRepository, IUnityInjector unity)
        {
            this.unity = unity;
            this.t_InputVGMRepository = inputVGMRepository;
        }

        public (List<T_InputVGM> list,int total) List(int pageIndex, int pageSize)
        {
            var q = this.t_InputVGMRepository.GetMany(i => true);
            int totals = q.Count();
            var list= q.OrderBy(i=>i.DoNo).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return (list, totals);
        }

        public void AddRange(List<T_InputVGM> t_InputVGMs)
        {
            this.t_InputVGMRepository.AddRange(t_InputVGMs);
            this.unity.Db.SaveChanges();
        }

        public void Add(T_InputVGM t_InputVGM)
        {
            var existEntity = this.t_InputVGMRepository.GetByKey(t_InputVGM.Id);
            if (existEntity != null)
                throw new ApplicationException("添加对象已存在");
            this.t_InputVGMRepository.Add(t_InputVGM);
            this.unity.Db.SaveChanges();
        }

        public void Delete(T_InputVGM t_InputVGM)
        {
            this.t_InputVGMRepository.Remove(t_InputVGM);
            this.unity.Db.SaveChanges();
        }

        public void Update(T_InputVGM t_InputVGM)
        {
            var existEntity=this.t_InputVGMRepository.GetByKey(t_InputVGM.Id);
            if (existEntity == null)
                throw new ApplicationException("更新对象不存在");
            existEntity.DoNo = t_InputVGM.DoNo;
            this.unity.Db.Entry(t_InputVGM).CurrentValues.SetValues(t_InputVGM);
            this.unity.Db.SaveChanges();
        }
    }
}
