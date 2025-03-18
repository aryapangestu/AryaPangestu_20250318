using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AryaPangestu_20250318.Data.Models;
using AryaPangestu_20250318.Data;
using AryaPangestu_20250318.Web.ViewModels;

namespace AryaPangestu_20250318.Web.Controllers
{
    [Authorize]
    public class ShipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shipment
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shipments.ToListAsync());
        }

        // GET: Shipment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(m => m.ShipmentId == id);

            if (shipment == null)
            {
                return NotFound();
            }

            // Get the status history for this shipment
            var statusHistory = await _context.ShipmentStatusHistory
                .Where(s => s.ShipmentId == id)
                .OrderByDescending(s => s.StatusDate)
                .ToListAsync();

            ViewBag.StatusHistory = statusHistory;

            return View(shipment);
        }

        // GET: Shipment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                // Generate tracking number if not provided
                if (string.IsNullOrEmpty(shipment.TrackingNumber))
                {
                    shipment.TrackingNumber = GenerateTrackingNumber();
                }

                shipment.CreatedDate = DateTime.Now;
                shipment.CurrentStatus = "Created";

                _context.Add(shipment);
                await _context.SaveChangesAsync();

                // Add initial status to history
                var initialStatus = new ShipmentStatusHistory
                {
                    ShipmentId = shipment.ShipmentId,
                    Status = "Created",
                    StatusDate = DateTime.Now,
                    Notes = "Shipment created in system"
                };

                _context.ShipmentStatusHistory.Add(initialStatus);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = shipment.ShipmentId });
            }
            return View(shipment);
        }

        // GET: Shipment/UpdateStatus/5
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateStatusViewModel
            {
                ShipmentId = shipment.ShipmentId,
                TrackingNumber = shipment.TrackingNumber
            };

            return View(viewModel);
        }

        // POST: Shipment/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(UpdateStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var shipment = await _context.Shipments.FindAsync(model.ShipmentId);
                if (shipment == null)
                {
                    return NotFound();
                }

                // Update shipment status
                shipment.CurrentStatus = model.Status;
                _context.Update(shipment);

                // Add to status history
                var statusHistory = new ShipmentStatusHistory
                {
                    ShipmentId = model.ShipmentId,
                    Status = model.Status,
                    StatusDate = DateTime.Now,
                    Notes = model.Notes
                };

                _context.ShipmentStatusHistory.Add(statusHistory);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = shipment.ShipmentId });
            }

            return View(model);
        }

        // Helper method to generate tracking number
        private string GenerateTrackingNumber()
        {
            // Get last tracking number
            var lastShipment = _context.Shipments
                .OrderByDescending(s => s.ShipmentId)
                .FirstOrDefault();

            int lastNumber = 0;
            if (lastShipment != null && lastShipment.TrackingNumber.StartsWith("KEID"))
            {
                int.TryParse(lastShipment.TrackingNumber.Substring(4), out lastNumber);
            }

            return $"KEID{(lastNumber + 1):D7}";
        }

        // GET: Shipment/Track
        [AllowAnonymous]
        public IActionResult Track()
        {
            return View();
        }

        // POST: Shipment/Track
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Track(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
            {
                ModelState.AddModelError("", "Please enter a tracking number");
                return View();
            }

            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);

            if (shipment == null)
            {
                ModelState.AddModelError("", "Shipment not found");
                return View();
            }

            var statusHistory = await _context.ShipmentStatusHistory
                .Where(s => s.ShipmentId == shipment.ShipmentId)
                .OrderByDescending(s => s.StatusDate)
                .ToListAsync();

            ViewBag.Shipment = shipment;
            ViewBag.StatusHistory = statusHistory;

            return View("TrackResult");
        }
    }
}
