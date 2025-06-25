using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlListApp.Data;
using UrlListApp.Models;

namespace UrlListApp.Controllers
{
    [Route("lists")]
    public class UrlItemsController : Controller
    {
        private readonly UrlListDbContext _context;
        public UrlItemsController(UrlListDbContext context)
        {
            _context = context;
        }

        // GET: /lists/add-url/{listId}
        [HttpGet("add-url/{listId}")]
        public IActionResult Add(int listId)
        {
            ViewBag.ListId = listId;
            return View();
        }

        // POST: /lists/add-url/{listId}
        [HttpPost("add-url/{listId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int listId, UrlItem item)
        {
            if (ModelState.IsValid)
            {
                item.UrlListId = listId;
                _context.UrlItems.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "UrlLists", new { id = listId });
            }
            ViewBag.ListId = listId;
            return View(item);
        }

        // GET: /lists/edit-url/{id}
        [HttpGet("edit-url/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.UrlItems.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /lists/edit-url/{id}
        [HttpPost("edit-url/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UrlItem updatedItem)
        {
            var item = await _context.UrlItems.FindAsync(id);
            if (item == null) return NotFound();
            if (ModelState.IsValid)
            {
                item.Url = updatedItem.Url;
                item.Title = updatedItem.Title;
                item.Description = updatedItem.Description;
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "UrlLists", new { id = item.UrlListId });
            }
            return View(updatedItem);
        }

        // POST: /lists/delete-url/{id}
        [HttpPost("delete-url/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.UrlItems.FindAsync(id);
            if (item == null) return NotFound();
            int listId = item.UrlListId;
            _context.UrlItems.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "UrlLists", new { id = listId });
        }
    }
}
