using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Services.Contract;
using SistemaVenta.DAL.Repositories.Contract;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> Query()
        {
            try 
            {
                var usersQuery = await _userRepository.Query();
                var usersList = usersQuery.Include(role => role.Role).ToList();

                return _mapper.Map<List<UserDTO>>(usersList);
            } 
            catch { throw; }
        }

        public async Task<SessionDTO> ValidateCredentials(string email, string password)
        {
            try
            {
                var userQuery = await _userRepository.Query(u => u.Email.Equals(email) && u.Password.Equals(password));

                if (userQuery.FirstOrDefault() == null ) 
                { 
                    throw new TaskCanceledException("El usuario no existe");
                }

                User authenticatedUser = userQuery.Include(role => role.Role).First();

                return _mapper.Map<SessionDTO>(authenticatedUser);
            }
            catch { 
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO entity)
        {
            try
            {
                var createdUser = await _userRepository.Create(_mapper.Map<User>(entity));

                if (createdUser.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear el usuario");
                }

                var query = await _userRepository.Query(u => u.Id == createdUser.Id);

                createdUser = query.Include(role => role.Role).First();

                return _mapper.Map<UserDTO>(createdUser);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foundUser = await _userRepository.Get(u => u.Id == id);

                if (foundUser == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }

                bool result = await _userRepository.Delete(foundUser);
                
                if (!result)
                {
                    throw new TaskCanceledException("No se pudo eliminar el usuario");
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(UserDTO entity)
        {
            try
            {
                var mappedUser = _mapper.Map<User>(entity);
                var foundUser = await _userRepository.Get(u => u.Id == mappedUser.Id);

                if (foundUser == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }

                foundUser.FullName = mappedUser.FullName;
                foundUser.Email = mappedUser.Email;
                foundUser.RoleId = mappedUser.RoleId;
                foundUser.Password = mappedUser.Password;
                foundUser.IsActive = mappedUser.IsActive;

                bool result = await _userRepository.Update(foundUser);

                if(!result)
                {
                    throw new TaskCanceledException("No se pudo editar");
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        
    }
}
