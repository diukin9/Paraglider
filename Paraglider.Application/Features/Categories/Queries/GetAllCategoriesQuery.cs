using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;

namespace Paraglider.API.Features.Categories.Queries;

public record GetAllCategoriesRequest() : IRequest<OperationResult>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesRequest, OperationResult>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //получаем все категории
        var collection = await _repository.FindAsync(_ => true);

        var result = collection.Select(x => _mapper.Map<CategoryDTO>(x)).ToList();
        return operation.AddSuccess(string.Empty, result);
    }
}