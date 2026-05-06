namespace CarePlusApi.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Models.Usuario usuario);
    }
}
