using dchv_api.Models;

namespace dchv_api.DTOs;

public class AuthDTO : LoginDTO
{
    public string Token { get; set; } = String.Empty;
}