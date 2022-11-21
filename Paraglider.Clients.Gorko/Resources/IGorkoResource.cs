using System.Linq.Expressions;
using Paraglider.Clients.Gorko.Models;
using Paraglider.Clients.Gorko.Result;

namespace Paraglider.Clients.Gorko.Resources;

public interface IGorkoResource<T>
{
    IGorkoResource<T> FilterBy<TProp>(Expression<Func<T, TProp>> selector,
        TProp value);

    IGorkoResource<T> WithPaging(PagingParameters pagingParameters);

    Task<Result<PagedResult<T>?>> GetResult();
}