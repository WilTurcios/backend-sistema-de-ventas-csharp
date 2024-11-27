using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? IsActive { get; set; }
        public int? RoleId { get; set; }
        public string? RoleDescription { get; set; }
    }
}
