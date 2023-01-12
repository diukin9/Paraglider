using MapsterMapper;
using MediatR;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Features.Categories.Queries;

public class GetAllCategoriesRequest : IRequest<InternalOperation<IEnumerable<CategoryDTO>>>
{

}

public class GetAllCategoriesQueryHandler 
    : IRequestHandler<GetAllCategoriesRequest, 
        InternalOperation<IEnumerable<CategoryDTO>>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<InternalOperation<IEnumerable<CategoryDTO>>> Handle(
        GetAllCategoriesRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<IEnumerable<CategoryDTO>>();

        //получаем все категории
        var collection = await _repository.FindAsync(_ => true);

        var result = collection.Select(_mapper.Map<CategoryDTO>).ToList();
        return operation.AddSuccess(result);
    }
}