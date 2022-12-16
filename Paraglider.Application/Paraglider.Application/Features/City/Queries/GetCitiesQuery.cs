using MapsterMapper;
using MediatR;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common.Response;

// ReSharper disable once CheckNamespace
namespace Paraglider.Application.Features.City.Queries;

public record GetCitiesQuery : IRequest<OperationResult<IEnumerable<CityDTO>>>;

public class GetCitiesQueryHandler
    : IRequestHandler<GetCitiesQuery,
        OperationResult<IEnumerable<CityDTO>>>
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(ICityRepository cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<CityDTO>>> Handle(
        GetCitiesQuery request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<IEnumerable<CityDTO>>();
        var cities = await _cityRepository.GetAll(cancellationToken);
        var citiesDto = _mapper.Map<IEnumerable<CityDTO>>(cities);
        return operation.AddSuccess(string.Empty, citiesDto);
    }
}