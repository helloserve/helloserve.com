using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Validation;

namespace helloserve.Common
{
    public class BaseRepo<TEntity> where TEntity: class, IEntity
    {
        internal static helloserveContext DB { get { return ContextFactory<helloserveContext>.GetContext(); } }

        public static void Save(TEntity entity)
        {
            try
            {
                if (DB.Entry(entity).State == EntityState.Detached)
                {
                    if (entity.IsNew())
                    {
                        DB.Set<TEntity>().Add(entity);
                    }
                    else
                    {
                        // When entities are attached, their state is set to Unchanged.
                        // If they have references to other entities that are not yet 
                        // tracked these are also attached in the Unchanged state.
                        // See http://blogs.msdn.com/b/adonet/archive/2011/01/29/using-dbcontext-in-ef-feature-ctp5-part-4-add-attach-and-entity-states.aspx
                        DB.Set<TEntity>().Attach(entity);

                        // Force the state to modified so the entity will be saved.
                        // This will not modify referenced "Unchanged" entities.
                        DB.Entry(entity).State = EntityState.Modified;
                    }
                }

                DB.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("Property: '{0}' Error: '{1}'", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
                throw new Exception(string.Format("Failed saving entity: '{0}'. {1}", entity.ToString(), sb.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Failed saving entity: '{0}'. Exception: {1}", entity.ToString(), ex.Message));
            }
        }

        public static IEnumerable<TEntity> GetAll()
        {
            return DB.Set<TEntity>();
        }

        public static IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> filter)
        {
            return filter(DB.Set<TEntity>());
        }

        public static void Delete(TEntity entity)
        {
            DB.Entry(entity).State = EntityState.Deleted;
            DB.SaveChanges();
        }
    }
}
