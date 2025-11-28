using CarePlusApi.Models;

namespace CarePlusApi.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<IEnumerable<Usuario>> GetTopRankedUsersAsync(int topN);
        Task<Usuario?> GetByIdAsync(Guid id); // Sobrescreve o GetByIdAsync para usar Guid
    }
}
