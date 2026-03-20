namespace Backend.DTOs.Usuario
{
    public class RefreshTokenResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
    }
}
