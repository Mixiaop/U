using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using U.Domain.Entities;
using U.Domain.Repositories;

namespace U.MongoDB.Repositories
{
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    /// <summary>
    /// 用MongoDB的方式实现IRepository
    /// </summary>
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : URepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IMongoDatabaseProvider _databaseProvider;
        public virtual IMongoDatabase Database { get { return _databaseProvider.Database; } }

        public virtual IMongoCollection<TEntity> Collection {
            get { return Database.GetCollection<TEntity>(typeof(TEntity).Name); }
        }

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return this.Collection.AsQueryable();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            //var query = Builders<TEntity>.
            return null;
        }

        public override TEntity Insert(TEntity entity)
        {
            //entity.Id = this.Count<TEntity, TPrimaryKey>() + 1;
            
            Collection.InsertOne(entity);
            
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// MongoDb自增实现
        /// 
        /// db.xxx.findAndModify(
        /// {
        ///   query:{_id: sequenceName },
        ///   update: {$inc:{sequence_value:1}},
        ///   new:true
        ///  });
        /// </summary>
        /// <returns></returns>
        private int GetNextSequence()
        {
            //Collection.FindOneAndUpdate()
            //MongoCollection<T> mc = Database.GetCollection<TEntity>( this._db.GetCollection<T>(collectionName);
            //query = this.InitQuery(query);
            //sortBy = this.InitSortBy(sortBy, OBJECTID_KEY);
            //update = this.InitUpdateDocument(update, indexName);
            //var ido = mc.FindAndModify(query, sortBy, update, true, false);

            //return ido.GetModifiedDocumentAs<T>(); 
            return 0;
        }
    }
}
