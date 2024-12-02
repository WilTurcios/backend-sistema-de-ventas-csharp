using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _rolRepository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            try
            {
                var rolesList = await _rolRepository.Query();

                return _mapper.Map<List<RoleDTO>>(rolesList);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
