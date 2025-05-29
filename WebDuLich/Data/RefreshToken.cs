using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDuLich.Data
{
    [Table("REFRESHTOKEN")]
    public class RefreshToken
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }

        [Required]
        [Column("USERID")]
        [StringLength(10)]
        public string UserId { get; set; }

        [Required]
        [Column("TOKEN")]
        [StringLength(450)]
        public string Token { get; set; }

        [Required]
        [Column("JWTID")]
        [StringLength(450)]
        public string JwtId { get; set; }

        [Column("ISUSED")]
        public bool IsUsed { get; set; }

        [Column("ISREVOKED")]
        public bool IsRevoked { get; set; }

        [Required]
        [Column("ISSUEDAT")]
        public DateTime IssuedAt { get; set; }

        [Required]
        [Column("EXPIREDAT")]
        public DateTime ExpiredAt { get; set; }

        [ForeignKey("UserId")]
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}