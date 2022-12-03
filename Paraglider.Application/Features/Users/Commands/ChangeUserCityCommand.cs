using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;

namespace Paraglider.API.Features.Users.Commands;

public record ChangeUserCityCommand(
        [Required] Guid CityId)
    : IRequest<OperationResult>;

public class ChangeUserCityCommandHandler : IRequestHandler<ChangeUserCityCommand, OperationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly HttpContext _httpContext;

    public ChangeUserCityCommandHandler(IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _httpContext = httpContextAccessor.HttpContext
                       ?? throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext));
    }

    public async Task<OperationResult> Handle(ChangeUserCityCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(_httpContext.User);
        if (user == null)
            return OperationResult.Error(AppData.ExceptionMessages.ObjectIsNull(nameof(user)));

        return await _userRepository.ChangeCity(user.Id, request.CityId, cancellationToken)
            ? OperationResult.Success(AppData.Messages.ObjectUpdated(nameof(ApplicationUser),
                nameof(ApplicationUser.City)))
            : OperationResult.Error(AppData.ExceptionMessages.UpdateError(nameof(ApplicationUser)));
    }
}