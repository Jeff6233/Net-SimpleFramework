using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Domain
{
    public interface IRepositoryBase<TSource>
    {
        IQueryable<TSource> GetMany(Expression<Func<TSource, bool>> predicate);
        void AddRange(IEnumerable<TSource> sources);
        void Add(TSource source);
        void Remove(TSource source);
        void RemoveRange(IEnumerable<TSource> sources);
        TSource GetByKey(params object[] keyValues);
    }

    public class RepositoryBase<TSource> : IRepositoryBase<TSource> where TSource : class
    {
        private readonly WebAppContext db;
        public RepositoryBase(WebAppContext db)
        {
            this.db = db;
        }

        public TSource GetByKey(params object[] keyValues)
        {
            return this.db.Set<TSource>().Find(keyValues);
        }
        public IQueryable<TSource> GetMany(Expression<Func<TSource, bool>> predicate)
        {
            predicate=Combine(predicate, ReadExpression(), Expression.And);
            return this.db.Set<TSource>().Where(predicate);
        }

        public void AddRange(IEnumerable<TSource> sources)
        {
            this.db.Set<TSource>().AddRange(sources);
        }

        public void Add(TSource source)
        {
            this.db.Set<TSource>().Add(source);
        }

        public void Remove(TSource source)
        {
            this.db.Set<TSource>().Remove(source);
        }

        public void RemoveRange(IEnumerable<TSource> sources)
        {
            this.db.Set<TSource>().RemoveRange(sources);
        }

        protected virtual Expression<Func<TSource, bool>> ReadExpression()
        {
            return i=>true;
        }

        private Expression<T> Combine<T>(Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            MyExpressionVisitor visitor = new MyExpressionVisitor(first.Parameters[0]);
            Expression bodyone = visitor.Visit(first.Body);
            Expression bodytwo = visitor.Visit(second.Body);
            return Expression.Lambda<T>(merge(bodyone, bodytwo), first.Parameters[0]);
        }
    }
    

    public class MyExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _Parameter { get; set; }

        public MyExpressionVisitor(ParameterExpression Parameter)
        {
            _Parameter = Parameter;
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            return _Parameter;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);//Visit会根据VisitParameter()方法返回的Expression修改这里的node变量
        }
    }
}
