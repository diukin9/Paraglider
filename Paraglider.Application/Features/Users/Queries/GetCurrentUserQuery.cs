using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.MongoDB;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Queries;

public record GetCurrentUserRequest() : IRequest<OperationResult>;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserRequest, OperationResult>
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

    public async Task<OperationResult> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //получаем текущего пользователя
        var username = _accessor.HttpContext!.User!.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null)
        {
            operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //маппим в дто и заполняем компоненты из mongoDB
        var result = _mapper.Map<UserDTO>(user!);

        //TODO может можно лучше?
        foreach (var planningComponent in result.Planning.Components)
        {
            planningComponent.Component = (await _components.FindByIdAsync(planningComponent.ComponentId))!;
        }

        foreach(var favourite in result.Favourites)
        {
            favourite.Component = (await _components.FindByIdAsync(favourite.ComponentId))!;
        }

        return operation.AddSuccess(string.Empty, result);
    }
}
