using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.API.Extensions;
using Paraglider.Data.EntityFrameworkCore.Repositories;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Infrastructure.Common.Extensions;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.WeddingPlannings.Queries;

[TsClass]
public class GetPlanningRequest : IRequest<OperationResult>
{

}

public class GetPlanningQueryHandler : IRequestHandler<GetPlanningRequest, OperationResult>
{
    private readonly UserRepository _userRepository;
    private readonly IDataAccess<WeddingComponent> _components;
    private readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;

    public GetPlanningQueryHandler(
        IUnitOfWork unitOfWork,
        IDataAccess<WeddingComponent> components,
        IHttpContextAccessor accessor,
        IMapper mapper)
    {
        _userRepository = (UserRepository)unitOfWork.GetRepository<ApplicationUser>();
        _components = components;
        _accessor = accessor;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(GetPlanningRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //получаем пользователя
        var username = _accessor.HttpContext!.User!.Identity!.Name;
        var user = await _userRepository.FindByUsernameAsync(username!);
        if (user is null) 
        {
            operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //получаем свадебный план
        var planning = user!.WeddingPlannings
            .Where(x => x.CityId == user.CityId)
            .Single();

        //формируем результат p.s. очень грустно
        var result = _mapper.Map<WeddingPlanningDTO>(planning);
        result.Components = new List<object>();
        foreach(var id in planning.ComponentIds)
        {
            var component = await _components.GetByIdAsync(id);
            if (component is null)
            {
                operation.AddError(ExceptionMessages.ObjectNotFound(nameof(WeddingComponent)));
            }

            result.Components.Add(component!.Convert(_mapper));
        }

        return operation.AddSuccess(string.Empty, result);
    }
}