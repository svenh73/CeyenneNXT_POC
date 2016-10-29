using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Concentrator.Core.Common.Core;
using Concentrator.Core.Types.Dto.Crud;
using Concentrator.Core.Types.Entities.Base;
using Dapper;
using Dapper.Extensions.Linq.Core.Predicates;

namespace Concentrator.Modules.Settings.Repository
{
  public abstract class Repository<TEntity> where TEntity : BaseEntity
  {
    private bool _useTransaction;

    private IDbConnection DbConnection { get; set; }

    public Repository(IDbConnection connection)
    {
      DbConnection = connection;
    } 

    public IEnumerable<TEntity> GetByQuery(string sql, params object[] parameters)
    {
      return DbConnection.Query<TEntity>(sql, parameters);
    }

    public TEntity GetById(int id)
    {
      return DbConnection.Get<TEntity>(id);
    }

    public IEnumerable<TEntity> GetByIds(int[] ids)
    {
      return DbConnection.GetList<TEntity>(String.Format("where id in ({0})", String.Join(",", ids)));
    }

    public IEnumerable<TEntity> GetItems()
    {
      return DbConnection.GetList<TEntity>();
    }

    public IEnumerable<TEntity> GetItems(object predicate, List<ISort> sort = null)
    {
      return DbConnection.GetList<TEntity>(predicate, sort);
    }

    public CrudResult<TEntity> Insert(TEntity item)
    {
      CrudResult<TEntity> result = new CrudResult<TEntity>(true);
      try
      {
        var id = DbConnection.Insert(item);
        item.ID = id.GetValueOrDefault();
        result = new CrudResult<TEntity>(id > 0, item);
      }
      catch (Exception ex)
      {
        while (ex.InnerException != null) ex = ex.InnerException;

        result = new CrudResult<TEntity>(false, item, ex.Message);
      }
      return result;
    }

    public CrudResult<TEntity> Update(TEntity item)
    {
      CrudResult<TEntity> result = new CrudResult<TEntity>(true);
      try
      {
        var success = false;
        var dbitem = GetById(item.ID);
        if (dbitem != null)
        {
          Mapper.Map(item, dbitem);
          success = DbConnection.Update(dbitem) > 0;
        }
        result = new CrudResult<TEntity>(success);
      }
      catch (Exception ex)
      {
        while (ex.InnerException != null) ex = ex.InnerException;
        result = new CrudResult<TEntity>(false, item, ex.Message);
      }
      return result;
    }

    public CrudResult<TEntity> Delete(TEntity item)
    {
      CrudResult<TEntity> result = new CrudResult<TEntity>(true);
      try
      {
        var deleted = DbConnection.Delete<TEntity>(item);
        result = new CrudResult<TEntity>(deleted);
      }
      catch (Exception ex)
      {
        while (ex.InnerException != null) ex = ex.InnerException;
        result = new CrudResult<TEntity>(false, item, ex.Message);
      }
      return result;
    }

    public void Commit()
    {
      Transaction?.Commit();
    }

    public void Rollback()
    {
      Transaction?.Rollback();
    }


    public void Dispose()
    {
      if (Transaction != null)
      {
        Transaction.Rollback();
      }
      DbConnection?.Dispose();
    }

  }
}
