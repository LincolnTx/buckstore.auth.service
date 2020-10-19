﻿using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Aggregates.UserAggregate;

namespace buckstore.auth.service.application.AutoMapper.UserMaps
{
    public class UserToCreateUserDto : MappingProfile
    {
        
        public UserToCreateUserDto()
        {
            CreateUserDto();
        }

        public void CreateUserDto()
        {
            CreateMap<User, CreateUserDto>()
                .ConstructUsing(user => new CreateUserDto(user.Id, user.GetName, user.GetSurname,
                    user.GetEmail, user.GetCpf, user.GetPassword, user.GetSalt));
        }
    }
}