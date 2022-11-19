using System.Linq.Expressions;
using Paraglider.GorkoClient.Models;
using Paraglider.GorkoClient.Result;

namespace Paraglider.GorkoClient.Resources;

public interface IGorkoResource<T>
{
    IGorkoResource<T> FilterBy<TProp>(Expression<Func<T, TProp>> selector,
        TProp value);

    IGorkoResource<T> WithPaging(PagingParameters pagingParameters);

    Task<Result<PagedResult<T>?>> GetResult();
}