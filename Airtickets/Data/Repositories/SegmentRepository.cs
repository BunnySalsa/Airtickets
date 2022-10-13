using System.Data;
using Airtickets.Entities;
using Airtickets.Models;

namespace Airtickets.Data.Repositories;

public class SegmentRepository
{
    private const string RepeatedOperationMessageS =
        "Repeated attempt to perform the operation or ticket doesn't exist";

    private const string SetLockTimeoutSql = "SET LOCAL lock_timeout = '120s'";

    private const string UpdateSegmentStatusSql = "UPDATE public.\"Segments\" SET \"Status\"={1} " +
                                                  "WHERE \"TicketNumber\"={0} AND \"Status\" <> {1}";

    private readonly AirTicketsDBContext _context;

    public SegmentRepository(AirTicketsDBContext context)
    {
        _context = context;
    }

    public async Task SaleAsync(IEnumerable<Segment> segments)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        await _context.Database.ExecuteSqlRawAsync(SetLockTimeoutSql);
        await _context.AddRangeAsync(segments);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task RefundAsync(String ticketNumber)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        if (await _context.Database.ExecuteSqlRawAsync(UpdateSegmentStatusSql, ticketNumber,
                SegmentStatus.Refunded) == 0)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException(RepeatedOperationMessageS);
        }

        await transaction.CommitAsync();
    }
}