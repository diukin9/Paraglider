namespace Paraglider.Application.DataTransferObjects;

using Paraglider.Infrastructure.Common.Interfaces;

public class AgreementStatusDTO : IDataTransferObject
{
    public string Name { get; set; } = null!;
    public int Value { get; set; }
}
