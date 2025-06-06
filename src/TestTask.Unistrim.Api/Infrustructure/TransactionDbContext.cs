using Microsoft.EntityFrameworkCore;
using TestTask.Unistrim.Api.Models;

namespace TestTask.Unistrim.Api.Infrustructure;

public class TransactionDbContext: DbContext
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options)
    {
    }

    public DbSet<TransactionModel> TransactionModels { get; set; }
}