using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace helloserve.Common
{
    public abstract class BaseEntity<TEntity> : IValidatableObject where TEntity : class, IEntity
    {
        private bool _validated = false;
        protected bool _skipValdiation = false;

        internal protected void Save(TEntity entity, bool skipValidation = false)
        {
            if (entity == null)
                return;

            _skipValdiation = skipValidation;

            BaseRepo<TEntity>.Save(entity);
        }

        internal protected void Delete(TEntity entity)
        {
            if (entity == null)
                return;

            BaseRepo<TEntity>.Delete(entity);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            
            if (_skipValdiation || _validated)
                return results;
            
            Validate(results);
            return results;            
        }

        protected virtual void Validate(List<ValidationResult> results)
        {
            _validated = true;
        }
    }
}
