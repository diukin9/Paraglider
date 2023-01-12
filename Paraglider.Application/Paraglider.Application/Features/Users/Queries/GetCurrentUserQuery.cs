using MapsterMapper;
using MediatR;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Features.Users.Queries;

public class GetCurrentUserRequest : IRequest<InternalOperation<UserDTO>>
{

}

public class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserRequest, InternalOperation<UserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        IUserRepository userRepository,
        IHttpContextAccessor accessor,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _accessor = accessor;
        _mapper = mapper;
    }

    public async Task<InternalOperation<UserDTO>> Handle(
        GetCurrentUserRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<UserDTO>();

        //получаем текущего пользователя
        var nameIdentifier = _accessor.HttpContext!.GetNameIdentifier();
        var user = await _userRepository.FindByNameIdentifierAsync(nameIdentifier!);
        if (user is null) operation.AddError("Пользователь не найден");

        //маппим в дто и заполняем компоненты из mongoDB
        var model = _mapper.Map<UserDTO>(user!);

        return operation.AddSuccess(model);
    }
}
