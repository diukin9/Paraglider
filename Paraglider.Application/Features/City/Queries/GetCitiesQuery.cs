using MapsterMapper;
using MediatR;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Infrastructure.Common;

// ReSharper disable once CheckNamespace
namespace Paraglider.API.Features;

public record GetCitiesQuery : IRequest<OperationResult>;

public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, OperationResult>
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(ICityRepository cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.GetAll(cancellationToken);
        var citiesDto = _mapper.Map<IEnumerable<CityDTO>>(cities);
        return OperationResult.Success(string.Empty, citiesDto);
    }
}