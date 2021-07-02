using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DD.Library.Data
{
	public static class Validator<T>
	{
        public static void CheckValid(T entity)
		{
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                if(results.Count>0)
				{
                    throw new Exception($"{string.Join(';',results.Select(x=>x.ErrorMessage))}");
				}
            }
        }
        public static void CheckValid(List<T> entitys)
        {
            var results = new List<ValidationResult>();      
			foreach (var entity in entitys)
			{
                var context = new ValidationContext(entity);
                if (!Validator.TryValidateObject(entity, context, results, true))
                {
                    if (results.Count > 0)
                    {
                        throw new Exception($"{string.Join(';', results.Select(x => x.ErrorMessage))}");
                    }
                }
            }
        }
    }
}
