namespace Paraglider.Infrastructure.Common.Abstractions
{
    public interface IShouldSaveChanges
    {
        public Task SaveChangesAsync();
    }
}
