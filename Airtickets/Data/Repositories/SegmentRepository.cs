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

    private AirTicketsDBContext _context;

    public SegmentRepository(AirTicketsDBContext context)
    {
        _context = context;
    }

    public async Task Save(IEnumerable<Segment> segments)
    {
        await _context.Database.BeginTransactionAsync();
        await _context.Database.ExecuteSqlRawAsync(SetLockTimeoutSql);
        _context.AddRange(segments);
        await _context.Database.CommitTransactionAsync();
        await _context.SaveChangesAsync();
    }

    public async Task RefundAsync(String ticketNumber)
    {
        await using (_context)
        {
            if (await _context.Database.ExecuteSqlRawAsync(UpdateSegmentStatusSql, ticketNumber,
                    SegmentStatus.Refunded) == 0)
                throw new InvalidOperationException(RepeatedOperationMessageS);
            await _context.SaveChangesAsync();
        }
    }
}