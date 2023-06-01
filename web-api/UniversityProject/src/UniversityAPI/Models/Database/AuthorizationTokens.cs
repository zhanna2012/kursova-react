using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models.Database;

[Table("AuthorizationTokens")]
public class AuthorizationTokens
{
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("access_token")]
    public string AccessToken { get; set; }

    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("is_revoked")]
    public bool IsRevoked { get; set; }
    
    [Column("expiration_datetime")]
    public DateTime ExpirationDatetime { get; set; }
}