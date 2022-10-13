using Airtickets.Data.Repositories;
using Airtickets.Entities;
using Airtickets.Models;

namespace Airtickets.Services;

public class TicketService
{
    private readonly SegmentRepository _segmentRepository;

    public TicketService(SegmentRepository repository)
    {
        _segmentRepository = repository;
    }


    public async Task SaleAsync(TicketDto ticket)
    {
        var segments = Segment.ToSegments(ticket);
        foreach (var segment in segments)
            segment.Status = SegmentStatus.Waiting;
        await _segmentRepository.SaleAsync(segments);
    }

    public async Task RefundAsync(TicketDto ticket)
    {
        await _segmentRepository.RefundAsync(ticket.TicketNumber);
    }
}