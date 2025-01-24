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
    public class AssetsController : Controller
    {
        private readonly ItassetTrackingDbContext _context;

        public AssetsController(ItassetTrackingDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var itassetTrackingDbContext = _context.Assets.Include(a => a.AssignedStaff);
            return View(await itassetTrackingDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.AssignedStaff)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }
        public IActionResult Create()
        {
            ViewData["AssignedStaffId"] = new SelectList(_context.Staff, "StaffId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetId,AssetNumber,AssetType,Status,PurchasedDate,DiscardedDate,AssignedStaffId,AssignedDate,LastReturnedDate")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignedStaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", asset.AssignedStaffId);
            return View(asset);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["AssignedStaffId"] = new SelectList(_context.Staff, "StaffId", "Name", asset.AssignedStaffId);
            return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetId,AssetNumber,AssetType,Status,PurchasedDate,DiscardedDate,AssignedStaffId,AssignedDate,LastReturnedDate")] Asset asset)
        {
            if (id != asset.AssetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetId))
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
            ViewData["AssignedStaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", asset.AssignedStaffId);
            return View(asset);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.AssignedStaff)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.AssetId == id);
        }
    }
}
