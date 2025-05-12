using Microsoft.EntityFrameworkCore;

namespace CloudStates.API.Data
{
    public class CloudStatesDbContext(DbContextOptions<CloudStatesDbContext> options) : DbContext(options)
    {

    }
}
