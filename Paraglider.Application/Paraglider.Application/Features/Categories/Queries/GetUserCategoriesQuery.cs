using MapsterMapper;
using MediatR;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Features.Categories.Queries;

public class GetUserCategoriesRequest : IRequest<InternalOperation<IEnumerable<CategoryDTO>>>
{

}

public class GetUserCategoriesQueryHandler 
    : IRequestHandler<GetUserCategoriesRequest, 
        InternalOperation<IEnumerable<CategoryDTO>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;

    public GetUserCategoriesQueryHandler(
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _accessor = accessor;
        _mapper = mapper;
    }

    public async Task<InternalOperation<IEnumerable<CategoryDTO>>> Handle(
        GetUserCategoriesRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<IEnumerable<CategoryDTO>>();

        //получаем текущего пользователя
        var nameIdentifier = _accessor.HttpContext!.GetNameIdentifier();
        var user = await _userRepository.FindByNameIdentifierAsync(nameIdentifier!);
        if (user is null) return operation.AddError("Пользователь не найден");

        //получаем категории пользователя
        var categories = user.Planning.Categories
            .Select(x => _mapper.Map<CategoryDTO>(x))
            .ToList();

        return operation.AddSuccess(categories);
    }
}
