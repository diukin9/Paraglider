using MapsterMapper;
using MediatR;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.MongoDB;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Users.Queries;

public record GetCurrentUserRequest() : IRequest<OperationResult<UserDTO>>;

public class GetCurrentUserQueryHandler 
    : IRequestHandler<GetCurrentUserRequest, OperationResult<UserDTO>>
{
    private readonly IMongoDataAccess<Component> _components;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        IMongoDataAccess<Component> components,
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _accessor = accessor;
        _mapper = mapper;
        _components = components;
    }

    public async Task<OperationResult<UserDTO>> Handle(
        GetCurrentUserRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<UserDTO>();

        //получаем текущего пользователя
        var username = _accessor.HttpContext!.User!.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null)
        {
            operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //маппим в дто и заполняем компоненты из mongoDB
        var result = _mapper.Map<UserDTO>(user!);

        //заполняем компоненты из плана
        foreach (var planningComponent in result.Planning.Components)
        {
            var component = await _components.FindByIdAsync(planningComponent.ComponentId)!;
            planningComponent.Component = _mapper.Map<ComponentDTO>(component!);
        }

        //заполняем компоненты из избранного
        foreach(var favourite in result.Favourites)
        {
            var component = await _components.FindByIdAsync(favourite.ComponentId)!;
            favourite.Component = _mapper.Map<ComponentDTO>(component!);
        }

        return operation.AddSuccess(string.Empty, result);
    }
}
