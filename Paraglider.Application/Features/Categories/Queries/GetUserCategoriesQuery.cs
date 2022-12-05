using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Categories.Queries;

public record GetUserCategoriesRequest() : IRequest<OperationResult<IEnumerable<CategoryDTO>>>;

public class GetUserCategoriesQueryHandler : IRequestHandler<GetUserCategoriesRequest, OperationResult<IEnumerable<CategoryDTO>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;

    public GetUserCategoriesQueryHandler(
        ICategoryRepository categoryRepository,
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _accessor = accessor;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<CategoryDTO>>> Handle(GetUserCategoriesRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult<IEnumerable<CategoryDTO>>();

        //получаем текущего пользователя
        var username = _accessor.HttpContext?.User?.Identity?.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null) 
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //получаем категории пользователя
        var categories = user.Planning.Categories
            .Select(x => _mapper.Map<CategoryDTO>(x))
            .ToList();

        return operation.AddSuccess(string.Empty, categories);
    }
}
