﻿using MyWebFormApp.BLL.DTOs;
using System.Collections.Generic;

namespace MyWebFormApp.BLL.Interfaces
{
    public interface IUserBLL
    {
        void ChangePassword(string username, string newPassword);
        void Delete(string username);
        IEnumerable<UserDTO> GetAll();
        UserDTO GetByUsername(string username);
        void Insert(UserCreateDTO entity);
        UserDTO Login(string username, string password);
        UserDTO LoginMVC(LoginDTO loginDTO);
        IEnumerable<UserDTO> GetAllWithRole();
    }
}
