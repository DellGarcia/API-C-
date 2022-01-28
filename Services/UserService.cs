using AutoMapper;
using Api_CSharp.Models;
using api_csharp.Repositories;
using api_csharp.Requests;
using api_csharp.Responses;
using System.Threading.Tasks;
using System;
using api_csharp.Exceptions;

namespace api_csharp.Services
{
    public class UserService
    {
        protected readonly UserRepository repository;
        protected readonly IMapper mapper;

        public UserService(
            UserRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        #region Implemented Methods

        public virtual async Task<UserResponse> Create(UserRequest request)
        {
            User entity = RequestToEntity(request);
            User result = await repository.Create(entity);

            return EntityToResponse(result);
        }

        public virtual async Task<bool> DeleteById(Guid id)
        {
            bool result = await repository.DeleteById(id);
            return result;
        }

        public virtual async Task<UserResponse> Update(UserRequest request)
        {
            User entity = RequestToEntity(request);
            User result = await repository.Update(entity);

            return EntityToResponse(result);
        }

        public virtual async Task<UserResponse> GetById(Guid id)
        {
            User entity = await repository.GetById(id);

            if (entity != null)
            {
                return EntityToResponse(entity);
            }

            throw new RegisterNotFoundException(typeof(User).Name, id);
        }

        #endregion

        #region Parse Methods

        protected User RequestToEntity(UserRequest request)
        {
            return mapper.Map<User>(request);
        }

        protected UserRequest EntityToRequest(User entity)
        {
            return mapper.Map<UserRequest>(entity);
        }

        protected UserResponse EntityToResponse(User entity)
        {
            return mapper.Map<UserResponse>(entity);
        }

        protected User ResponseToEntity(UserResponse response)
        {
            return mapper.Map<User>(response);
        }

        #endregion
    }
}