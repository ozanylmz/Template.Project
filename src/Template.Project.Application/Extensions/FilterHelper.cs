using System.Linq.Expressions;
using System.Text.Json;
using System.Xml.Linq;
using Template.Project.Application.Extensions.Model;
using Template.Project.Domain.AggregateModels.Customer;
using Template.Project.Domain.Enums;

namespace Template.Project.Application.Extensions
{
    public static class FilterHelper
    {
        public static async Task<Expression<Func<Customer, bool>>> GetFilterPredicate(string queryFilter)
        {
            if (string.IsNullOrEmpty(queryFilter))
                return null;

            List<FilteredQuery>? filters = JsonSerializer.Deserialize<List<FilteredQuery>>(queryFilter);
            Expression<Func<Customer, bool>>? predicate = null;
            ParameterExpression paramExp;
            paramExp = Expression.Parameter(typeof(Customer));

            if (filters.Count == 0 || filters is null)
                return null;

            foreach (var filter in filters)
            {
                if (filter.Category == "Name")
                {
                    Expression<Func<Customer, bool>> name = c => c.Name == filter.Value;
                    if (predicate is not null)
                        predicate = Expression.Lambda<Func<Customer, bool>>(Expression.AndAlso(predicate.Body, name.Body), predicate.Parameters);
                    else
                        predicate = name;
                }
                else if (filter.Category == "Surname")
                {
                    Expression<Func<Customer, bool>> surname = c => c.Surname == filter.Value;
                    if (predicate is not null)
                    {
                        var invokedExpr = Expression.Invoke(surname, predicate.Parameters.Cast<Expression>());
                        predicate = Expression.Lambda<Func<Customer, bool>>(Expression.AndAlso(predicate.Body, invokedExpr), predicate.Parameters);
                    }  
                    else
                        predicate = surname;
                }
                else if (filter.Category == "Status")
                {
                    Expression<Func<Customer, bool>> status = c => c.Status == (CustomerStatus)Enum.Parse(typeof(CustomerStatus), filter.Value, true);
                    if (predicate is not null)
                        predicate = Expression.Lambda<Func<Customer, bool>>(Expression.AndAlso(predicate.Body, status.Body), predicate.Parameters);
                    else
                        predicate = status;
                }
            }

            return predicate;
        }
    }
}
