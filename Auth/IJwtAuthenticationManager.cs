using InventoryManagemenSystem_Ims.DTOs;

namespace InventoryManagemenSystem_Ims.Auth
{
    public interface IJwtAuthenticationManager
    {
        public string GenerateToken(UserDto user);
    }
}