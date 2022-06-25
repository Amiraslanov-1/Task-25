using _25._06._2022Task.DAL;
using _25._06._2022Task.Extensions;
using _25._06._2022Task.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _25._06._2022Task.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class UserSayController : Controller
    {
        private readonly AppDbContext _context;

        public readonly IWebHostEnvironment _web;

        public UserSayController(AppDbContext context, IWebHostEnvironment web)
        {
            _context = context;
            _web = web;
        }
        public IActionResult Index()
        {

            return View(_context.UserSays.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(UserSay userSay)
        {
            if (!ModelState.IsValid)
            {
                return View(userSay);
            }
            bool isExist = _context.UserSays.Any(us => us.Name.ToLower().Trim().Contains(userSay.Name.ToLower().Trim()));
            if (isExist)
            {
                ModelState.AddModelError("Name", "Name Is Exist!");
                return View();
            }
            if (userSay.Photo != null)
            {

                if (userSay.Photo.CheckSize(5))
                {
                    ModelState.AddModelError("Photo", "Max 5 mb !");
                    return View();

                }
                if (userSay.Photo.CheckType("/image"))
                {
                    ModelState.AddModelError("Photo", "Type Is Not Image !!!");
                    return View();

                }
            }
            else
            {
                ModelState.AddModelError("Photo", "Choose Image !!!");
                return View();
            }

            userSay.ImgUrl = await userSay.Photo.SaveFileAsync(Path.Combine(_web.WebRootPath, "Assets", "img", "user-img"));
            await _context.AddAsync(userSay);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }
        public IActionResult Update(int id)
        {
            UserSay userSay = _context.UserSays.Find(id);
            if (userSay == null) { return NotFound(); }
            return View(userSay);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserSay userSay, int id)
        {
            if (userSay.Id != id)
            {
                return BadRequest();
            }
            UserSay user = _context.UserSays.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            if (userSay.Photo != null)
            {
                if (userSay.Photo.CheckSize(5))
                {
                    ModelState.AddModelError("Photo", "Max 5 mb !");
                    return View();

                }
                if (userSay.Photo.CheckType("/image"))
                {
                    ModelState.AddModelError("Photo", "Type Is Not Image !!!");
                    return View();

                }
            }
            else
            {
                ModelState.AddModelError("Photo", "Choose Image");
                return View();
            }
            userSay.ImgUrl = await userSay.Photo.SaveFileAsync(Path.Combine(_web.WebRootPath, "Assets", "img", "user-img"));
            user.Name = userSay.Name;
            user.Description = userSay.Description;
            user.Raiting = userSay.Raiting;
            user.Position = userSay.Position;
            user.ImgUrl = userSay.ImgUrl;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }
        public IActionResult Delete(int id)
        {
            UserSay usersay = _context.UserSays.Find(id);
            if (usersay == null)
            {
                return NotFound();
            }
            Extension.Delete(Path.Combine(_web.WebRootPath, "Assets", "img", "user-img", usersay.ImgUrl));
            _context.UserSays.Remove(usersay);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
