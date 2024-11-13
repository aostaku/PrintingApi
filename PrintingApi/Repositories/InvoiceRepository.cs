using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PrintingApi.Data;
using PrintingApi.Model;

namespace PrintingApi.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DbContextClass _dbContext;

        public InvoiceRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<InvoiceDetails> AddInvoice(InvoiceDetails invoiceDetails)
        {
            var result = _dbContext.Invoices.Add(invoiceDetails); 
            await _dbContext.SaveChangesAsync();     
            return result.Entity;
        }

        public async Task<PaginatedList<InvoiceDetails>> GetAllInvoices(int pageIndex,int pageSize)
        {

            var invoices = await _dbContext.Invoices
                .OrderBy(b => b.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            var count = await _dbContext.Invoices.CountAsync();
            var totalPages = (int)Math.Ceiling(count/(double)pageSize);
            return new PaginatedList<InvoiceDetails>(invoices,pageIndex,pageSize);
        }

        public async Task<InvoiceDetails> GetInvoiceById(int Id)
        {
            return await _dbContext.Invoices.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
