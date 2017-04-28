using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Seafood.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Seafood.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly AdminDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(UserManager<ApplicationUser> userManager, AdminDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult AllPosts()
        {
            return View(_db.Posts.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            post.User = currentUser;
            _db.Posts.Add(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Posts.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Edit(int id)
        {
            var thisPost = _db.Posts.FirstOrDefault(items => items.Id == id);
            return View(thisPost);
        }

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            _db.Entry(post).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}