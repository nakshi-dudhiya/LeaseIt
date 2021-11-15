using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaseIt.Data;
using LeaseIt.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LeaseIt.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Admin")]
        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feedbacks.ToListAsync());
        }

        

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Rating,Comments")] Feedback feedback)
        {

            string userEmail = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (feedback.UserEmail == null)
                {
                    feedback.UserEmail = userEmail;
                }
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                
            }
            return View("FeedbackSent");
        }




        // GET: Feedbacks/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.FeedbackId == id);
        }
    }
}
