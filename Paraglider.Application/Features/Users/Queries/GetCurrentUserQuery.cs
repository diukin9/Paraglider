using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Infrastructure.Common.Extensions;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Users.Queries
{
    [TsClass]
    public class GetCurrentUserRequest : IRequest<OperationResult>
    {

    }

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserRequest, OperationResult>
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _userRepository = (UserRepository)unitOfWork.GetRepository<ApplicationUser>();
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
        {
            var operation = new OperationResult();

            //получаем текущего пользователя
            var username = _accessor.HttpContext!.User!.Identity!.Name;
            var user = await _userRepository.FindByUsernameAsync(username!);
            if (user is null)
            {
                operation.AddError(ExceptionMessages.ObjectNotFound(nameof(user)));
            }

            return operation.AddSuccess(string.Empty, _mapper.Map<UserDTO>(user!));
        }
    }
}
