using Microsoft.AspNetCore.Mvc;
using MyWebFormApp.BLL;
using MyWebFormApp.BLL.DTOs;
using MyWebFormApp.BLL.Interfaces;
using System.Text.Json;
namespace SampleMVC.Controllers;

public class HomeController : Controller
{
    private readonly IUserBLL _userBLL;
    private readonly IRoleBLL _roleBLL;

    public HomeController(IUserBLL userBLL, IRoleBLL roleBLL)
    {
        _userBLL = userBLL;
        _roleBLL = roleBLL;
    }


    // Home/Index
    public IActionResult Index()
    {
        //check if session not null
        if (HttpContext.Session.GetString("user") != null)
        {
            var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
            ViewBag.Message = $"Welcome {userDto.FirstName} {userDto.LastName}";
        }

        ViewData["Title"] = "Home Page";
        return View();
    }

    [Route("/Hello/ASP")]
    public IActionResult HelloASP()
    {
        return Content("Hello ASP.NET Core MVC!");
    }

    // Home/About
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return Content("This is the Contact action method...");
    }

    //action method for uploading file
    public IActionResult UploadFilePics()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UploadFilePics(IFormFile file)
    {
        if (file != null)
        {
            if (Helper.IsImageFile(file.FileName))
            {
                //random file name based on GUID
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pics", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>File uploaded successfully !</div>";
            }
            else
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>File is not an image file !</div>";
            }
        }
        return View();
    }
    public IActionResult AddRole()
    {
        ViewBag.user = _userBLL.GetAll();
        ViewBag.role = _roleBLL.GetAllRoles();


        var userDto = JsonSerializer.Deserialize<UserDTO>(HttpContext.Session.GetString("user"));
        ViewBag.Message = $"{userDto.FirstName} {userDto.LastName}";
        var userRoles = userDto.Roles;
        var access = 0;
        foreach (var role in userRoles)
        {
            if (role.RoleID == 1)
            {
                access = 1;
            }
        }
        if (access == 1)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }

        
    }
    [HttpPost]
    public IActionResult AddRole(string username, int roleID)
    {
        _roleBLL.AddUserToRole(username, roleID);
        return RedirectToAction("Index", "Home");
    }
}
