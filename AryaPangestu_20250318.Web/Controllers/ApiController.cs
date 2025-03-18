using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AryaPangestu_20250318.Data.Models;
using AryaPangestu_20250318.Data;

namespace AryaPangestu_20250318.Web.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/shipment/{trackingNumber}
        [HttpGet("shipment/{trackingNumber}")]
        public async Task<ActionResult<Shipment>> GetShipment(string trackingNumber)
        {
            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);

            if (shipment == null)
            {
                return NotFound(new { message = "Shipment not found" });
            }

            return Ok(shipment);
        }

        // GET: api/shipment/{trackingNumber}/history
        [HttpGet("shipment/{trackingNumber}/history")]
        public async Task<ActionResult<IEnumerable<ShipmentStatusHistory>>> GetShipmentHistory(string trackingNumber)
        {
            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);

            if (shipment == null)
            {
                return NotFound(new { message = "Shipment not found" });
            }

            var history = await _context.ShipmentStatusHistories
                .Where(h => h.ShipmentId == shipment.ShipmentId)
                .OrderByDescending(h => h.StatusDate)
                .ToListAsync();

            return Ok(history);
        }
    }
}
