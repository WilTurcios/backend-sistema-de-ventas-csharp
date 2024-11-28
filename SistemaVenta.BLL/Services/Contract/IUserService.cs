using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> Query();
        Task<SessionDTO> ValidateCredentials(string email, string password);
        Task<UserDTO> Create(UserDTO entity);
        Task<bool> Update(UserDTO entity);
        Task<bool> Delete(int id);

    }
}
