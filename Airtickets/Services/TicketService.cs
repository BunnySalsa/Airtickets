using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using Airtickets.Data.Repositories;
using Airtickets.Entities;
using Airtickets.Models;
using Airtickets.Validators;

namespace Airtickets.Services;

public class TicketService
{
    private readonly SegmentRepository _segmentRepository;
    private static TicketService _ticketService;

    private TicketService()
    {
        _segmentRepository = SegmentRepository.GetInstance();
    }

    public static TicketService GetInstance()
    {
        if (_ticketService == null)
            _ticketService = new TicketService();
        return _ticketService;
    }

    public async Task SaleAsync(TicketDto ticket)
    {
        var segments = Segment.ToSegments(ticket);
        foreach (var segment in segments)
            segment.Status = SegmentStatus.Waiting;
        await _segmentRepository.Save(segments);
    }

    public async Task RefundAsync(TicketDto ticket)
    {
        await _segmentRepository.RefundAsync(ticket.TicketNumber);
    }
}