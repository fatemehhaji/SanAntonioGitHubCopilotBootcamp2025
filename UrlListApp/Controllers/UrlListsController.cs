using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlListApp.Data;
using UrlListApp.Models;

namespace UrlListApp.Controllers
{
    [Route("lists")]
    public class UrlListsController : Controller
    {
        private readonly UrlListDbContext _context;
        public UrlListsController(UrlListDbContext context)
        {
            _context = context;
        }

        // GET: /lists
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lists = await _context.UrlLists.Include(l => l.Urls).ToListAsync();
            return View(lists);
        }

        // GET: /lists/new
        [HttpGet("new")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /lists/new
        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UrlList list)
        {
            if (ModelState.IsValid)
            {
                // Generate a unique URL if not provided
                if (string.IsNullOrWhiteSpace(list.CustomUrl))
                {
                    list.GeneratedUrl = Guid.NewGuid().ToString().Substring(0, 8);
                }
                _context.UrlLists.Add(list);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = list.Id });
            }
            return View(list);
        }

        // GET: /lists/{id}/edit
        [HttpGet("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var list = await _context.UrlLists.Include(l => l.Urls).FirstOrDefaultAsync(l => l.Id == id);
            if (list == null) return NotFound();
            return View(list);
        }

        // POST: /lists/{id}/edit
        [HttpPost("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UrlList updatedList)
        {
            var list = await _context.UrlLists.FindAsync(id);
            if (list == null) return NotFound();
            if (ModelState.IsValid)
            {
                list.Title = updatedList.Title;
                list.CustomUrl = updatedList.CustomUrl;
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", new { id });
            }
            return View(updatedList);
        }

        // POST: /lists/{id}/delete
        [HttpPost("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _context.UrlLists.Include(l => l.Urls).FirstOrDefaultAsync(l => l.Id == id);
            if (list == null) return NotFound();
            _context.UrlLists.Remove(list);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: /lists/{id}/publish
        [HttpPost("{id}/publish")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(int id)
        {
            var list = await _context.UrlLists.FindAsync(id);
            if (list == null) return NotFound();
            list.IsPublished = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Share", new { id });
        }

        // GET: /lists/{id}/share
        [HttpGet("{id}/share")]
        public async Task<IActionResult> Share(int id)
        {
            var list = await _context.UrlLists.FindAsync(id);
            if (list == null) return NotFound();
            return View(list);
        }

        // GET: /lists/{url}
        [HttpGet("view/{url}")]
        public async Task<IActionResult> ViewList(string url)
        {
            var list = await _context.UrlLists.Include(l => l.Urls)
                .FirstOrDefaultAsync(l => l.CustomUrl == url || l.GeneratedUrl == url);
            if (list == null || !list.IsPublished) return NotFound();
            return View("ViewList", list);
        }
    }
}
