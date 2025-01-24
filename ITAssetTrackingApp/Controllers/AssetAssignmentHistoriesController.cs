using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITAssetTrackingApp.Models;

namespace ITAssetTrackingApp.Controllers
{
    public class AssetAssignmentHistoriesController : Controller
    {
        private readonly ItassetTrackingDbContext _context;

        public AssetAssignmentHistoriesController(ItassetTrackingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var itassetTrackingDbContext = _context.AssetAssignmentHistories.Include(a => a.Asset).Include(a => a.Staff);
            return View(await itassetTrackingDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAssignmentHistory = await _context.AssetAssignmentHistories
                .Include(a => a.Asset)
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (assetAssignmentHistory == null)
            {
                return NotFound();
            }

            return View(assetAssignmentHistory);
        }

        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.Assets, "AssetId", "AssetId");
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentId,AssetId,StaffId,AssignedDate,ReturnedDate")] AssetAssignmentHistory assetAssignmentHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetAssignmentHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.Assets, "AssetId", "AssetId", assetAssignmentHistory.AssetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", assetAssignmentHistory.StaffId);
            return View(assetAssignmentHistory);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAssignmentHistory = await _context.AssetAssignmentHistories.FindAsync(id);
            if (assetAssignmentHistory == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Assets, "AssetId", "AssetId", assetAssignmentHistory.AssetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", assetAssignmentHistory.StaffId);
            return View(assetAssignmentHistory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentId,AssetId,StaffId,AssignedDate,ReturnedDate")] AssetAssignmentHistory assetAssignmentHistory)
        {
            if (id != assetAssignmentHistory.AssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetAssignmentHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetAssignmentHistoryExists(assetAssignmentHistory.AssignmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.Assets, "AssetId", "AssetId", assetAssignmentHistory.AssetId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", assetAssignmentHistory.StaffId);
            return View(assetAssignmentHistory);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAssignmentHistory = await _context.AssetAssignmentHistories
                .Include(a => a.Asset)
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (assetAssignmentHistory == null)
            {
                return NotFound();
            }

            return View(assetAssignmentHistory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetAssignmentHistory = await _context.AssetAssignmentHistories.FindAsync(id);
            if (assetAssignmentHistory != null)
            {
                _context.AssetAssignmentHistories.Remove(assetAssignmentHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetAssignmentHistoryExists(int id)
        {
            return _context.AssetAssignmentHistories.Any(e => e.AssignmentId == id);
        }
    }
}
