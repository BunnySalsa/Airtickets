using Airtickets.Entities;
using Airtickets.Models;

namespace Airtickets.Data.Repositories;

public class SegmentRepository
{
    private const string RepeatedOperationMessageS = "Repeated attempt to perform the operation or ticket doesn't exist";
    private const string SetLockTimeoutSql = "SET LOCAL lock_timeout = '120s'";

    private const string UpdateSegmentStatusSql = "UPDATE public.\"Segments\" SET \"Status\"={1} " +
                                                  "WHERE \"TicketNumber\"={0} AND \"Status\" <> {1}";

    private static SegmentRepository _repository;
    private readonly DbContextFactory _contextFactory;

    private SegmentRepository()
    {
        _contextFactory = DbContextFactory.GetInstance();
    }

    public static SegmentRepository GetInstance()
    {
        if (_repository == null)
            _repository = new SegmentRepository();
        return _repository;
    }

    public async Task Save(IEnumerable<Segment> segments)
    {
        await using (var context = _contextFactory.CreateAirTicketsDbContext())
        {
            context.Database.ExecuteSqlRaw(SetLockTimeoutSql);
            context.AddRange(segments);
            context.SaveChanges();
        }
    }

    public async Task RefundAsync(String ticketNumber)
    {
        await using (var context = _contextFactory.CreateAirTicketsDbContext())
        {
            if (context.Database.ExecuteSqlRaw(UpdateSegmentStatusSql, ticketNumber, SegmentStatus.Refunded) == 0)
                throw new InvalidOperationException(RepeatedOperationMessageS);
            context.SaveChanges();
        }
    }
}