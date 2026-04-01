using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMusicStore.Models
{
    public class MusicStoreUser
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsApproved { get; set; }
    }

    public class MusicStoreRole
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class MusicStoreUserRole
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public virtual MusicStoreRole Role { get; set; }
    }
}
