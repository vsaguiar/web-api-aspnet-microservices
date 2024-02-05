namespace VShop.IdentityServer.SeedDatabase;

public interface IDatabaseSeedInitializer
{
    void InitializeSeedRoles(); // Perfis 
    void InitializeSeedUsers(); // Usuários
}
