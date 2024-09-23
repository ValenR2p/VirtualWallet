using Microsoft.AspNetCore.Mvc;
using TransferApplication.Exceptions;
using TransferApplication.Interfaces;
using TransferApplication.Request;

namespace Transfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferServices _service;

        public TransferController(ITransferServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _service.GetAll();

            return Ok(result);

        }

        [HttpGet("Account/{id}")]
        public async Task<IActionResult> GetAllByUser(Guid id)
        {
            try
            {
                var result = await _service.GetAllByUser(id);

                return new JsonResult(result) { StatusCode = 200 };

            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer(CreateTransferRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var message = string.Join(" ", errorMessages);

                return BadRequest(new { message });
            }
            try
            {
                var result = await _service.CreateTransfer(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpOptions("{id}")]
        public async Task<IActionResult> UpdateTransfer(CreateTransferRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var transfer = await _service.UpdateTransfer(request);

                if (transfer == null)
                {
                    return BadRequest(new { message = "No se pudo actualizar la transferencia" });
                }

                return new JsonResult(transfer) { StatusCode = 201 };
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
