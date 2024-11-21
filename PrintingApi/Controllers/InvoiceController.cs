using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintingApi.Commands;
using PrintingApi.Model;
using PrintingApi.Queries;

namespace PrintingApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Printer _printer; 

        public InvoiceController(IMediator mediator,IConfiguration configuration)
        {
            _mediator = mediator; 
            _printer = new Printer(configuration);
        }
         

        [HttpPost]
        public async Task<InvoiceDetails> AddInvoiceAsync(InvoiceDetails invoice)
        {
            var invoiced = await _mediator.Send(new CreateInvoiceCommand(
                invoice.Company, 
                invoice.StreetAddress,
                invoice.CityZipCode,
                invoice.Website,
                invoice.Date,
                invoice.ProductName,
                invoice.Price
                ));

            return invoiced;
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoiceById(int invoiceId)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery() { Id = invoiceId });
            
            if (invoice == null)
            {
                return NotFound($"Invoice with ID {invoiceId} not found.");
            }
            return Ok(invoice);
        }
        [HttpPost("print/{invoiceId}")]
        public async Task<IActionResult> PrintInvoiceById(int invoiceId)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery() { Id = invoiceId });
            if (invoice == null)
            {
                return NotFound($"Invoice with ID {invoiceId} not found.");
            }
            await _printer.Print(invoice);
            return Ok($"Invoice {invoiceId} printed successfully.");
        }

        // POST: Print not saved invoice
        [HttpPost("print")]
        public async Task<IActionResult> PrintInvoice(InvoiceDetails invoice)
        {
            if (invoice == null)
            {
                return BadRequest("Invoice is null.");
            }
            await _printer.Print(invoice);
            return Ok("Invoice printed successfully.");
        }

        [HttpGet]
        public async Task<PaginatedList<InvoiceDetails>> GetAllInvoices(int pageIndex, int pageSize)
        {
            var invoices = await _mediator.Send(new GetInvoicesPaginationQuery() { PageIndex = pageIndex, PageSize = pageSize });
            return invoices;
        }


        [HttpGet("heartbeat")]
        public IActionResult GetHeartbeat()
        {
            return Ok();
        }




    }

}
