namespace Airtickets.Data;

public class DbContextFactory
{
    private static DbContextFactory _ticketService;

    private readonly string _connectionString;

    private DbContextFactory()
    {
        _connectionString = WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection");
    }

    public static DbContextFactory GetInstance()
    {
        if (_ticketService == null)
            _ticketService = new DbContextFactory();
        return _ticketService;
    }
    
    public AirTicketsDBContext CreateAirTicketsDbContext()
    {
        var options = new DbContextOptionsBuilder<AirTicketsDBContext>()
            .UseNpgsql(_connectionString)
            .Options;
        return new AirTicketsDBContext(options);
    }
}