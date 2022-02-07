using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PROJET_FIN.Models;
using Microsoft.AspNetCore.Http;

namespace PROJET_FIN.Controllers
{
    public class HomeController : Controller
    {
        private readonly projetfinalContext _context;
        private readonly ISession _session;

        public HomeController(projetfinalContext context,IHttpContextAccessor accessor)
        {
            this._context = context;
            this._session = accessor.HttpContext.Session;
        }
// -------------------------------------------------------------------------------
        public IActionResult Index()
        {
           ViewBag.Email = _session.GetString("Email");
           ViewBag.Password = _session.GetString("Password");
           ViewBag.UserName = _session.GetString("UserName");
           ViewBag.comments = _context.Comments.ToList();
           ViewBag.users = _context.Users.ToList();
           var sortRating =  _context.Posts.ToList().OrderByDescending(p => p.UpVote-p.DownVote);// Sets the links in rating descending order
           return View(sortRating);  
        }

        [HttpGet]
        public IActionResult Login()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
       
         public IActionResult Login (User user)
        {
             var validation = _context.Users.Where(v => v.Email==user.Email && v.Password==user.Password).FirstOrDefault();
             if (validation!=null)
             {
                 _session.SetString("Email",user.Email);
                 _session.SetString("Password",user.Password);
                 _session.SetString("UserName",validation.UserName);
                 return RedirectToAction("Index");
             }
             else {
                 return View(user);
             }
        }

        public IActionResult Register(){

            return View();
        }

        public IActionResult Logout()
        {
             _session.Clear();
             return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult MyLinks(){
          int id =  _context.Users.ToList().Where(u=> u.Email=="elian@gmail.com" ).FirstOrDefault().Id;
           //var validation = _context.Users.Where(v => v.Email==user.Email && v.Password==user.Password).FirstOrDefault();
           //var sortRating =  _context.Posts.ToList().OrderByDescending(p => p.UpVote-p.DownVote);
           //var id =  _context.Users.ToList().Where(u=> u.Email);
            
           ViewBag.comments = _context.Comments.ToList();//.Where(u => u.UserId==user.Id);
           return View( _context.Posts.ToList().Where (u=> u.UserId==id));
        }
        public IActionResult AddLink(){
            return View();
        }

        public IActionResult Remove(){

            return View("MyLinks");
        }

         [HttpGet] 
        public IActionResult Addlink()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Addlink(string link, string description, DateTime publicationdate, int upvote, int downvote, int userid)
        {
            Post post = new Post()
            {
                Link = link,
                Description = description,
                PublicationDate = DateTime.Now,
                UpVote = upvote,
                DownVote= downvote,
                UserId= userid
            };
            _context.AddNewPost(post);
            return View();
        }
 
        
// -------------------------------------------------------------------------------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
