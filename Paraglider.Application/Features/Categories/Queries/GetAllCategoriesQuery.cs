using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;

namespace Paraglider.API.Features.Categories.Queries;

public record GetAllCategoriesRequest() : IRequest<OperationResult<IEnumerable<CategoryDTO>>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesRequest, OperationResult<IEnumerable<CategoryDTO>>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<CategoryDTO>>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult<IEnumerable<CategoryDTO>>();

        //получаем все категории
        var collection = await _repository.FindAsync(_ => true);

        var result = collection.Select(_mapper.Map<CategoryDTO>).ToList();
        return operation.AddSuccess(string.Empty, result);
    }
}