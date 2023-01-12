using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Users.Commands;

public class ChangeUserCityRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("city_id")]
    public Guid CityId { get; set; }
}

public class ChangeUserCityCommandHandler 
    : IRequestHandler<ChangeUserCityRequest, InternalOperation>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _accessor;
    private readonly ICityRepository _cityRepository;
    private readonly HttpContext? _httpContext;

    public ChangeUserCityCommandHandler(
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor accessor,
        ICityRepository cityRepository)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _httpContext = httpContextAccessor?.HttpContext;
        _accessor = accessor;
        _cityRepository = cityRepository;
    }

    public async Task<InternalOperation> Handle(
        ChangeUserCityRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем текущего пользователя
        var nameIdentifier = _accessor.HttpContext!.GetNameIdentifier();
        var user = await _userRepository.FindByNameIdentifierAsync(nameIdentifier!);
        if (user is null) return operation.AddError("Пользователь не найден");

        //получаем указанный город
        var city = await _cityRepository.FindByIdAsync(request.CityId);
        if (city is null) return operation.AddError("Город не найден");

        user.City = city;
        user.CityId = city.Id;

        try
        {
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return operation.AddSuccess();
        }
        catch (Exception exception)
        {
            return operation.AddError(exception.Message, exception);
        }
    }
}