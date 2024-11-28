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
    public class MenuItemService : IMenuItemService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<RolMenu> _roleMenuRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IGenericRepository<User> userRepository, IGenericRepository<RolMenu> roleMenuRepository, IGenericRepository<MenuItem> menuItemRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleMenuRepository = roleMenuRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuItemDTO>> GetMenuItems(int userId)
        {
            IQueryable<User> userTb = await _userRepository.Query();
            IQueryable<RolMenu> roleMenuTb = await _roleMenuRepository.Query();
            IQueryable<MenuItem> menuItemTb = await _menuItemRepository.Query();

            try
            {
                IQueryable<MenuItem> resultTb = (
                    from u in userTb
                    join rm in roleMenuTb on u.RoleId equals rm.RoleId
                    join mi in menuItemTb on rm.MenuItemId equals mi.Id
                    select mi
                );

                var menuItemList = resultTb.ToList();
                return _mapper.Map<List<MenuItemDTO>>( menuItemList );
            }
            catch
            {
                throw;
            }
        }
    }
}
