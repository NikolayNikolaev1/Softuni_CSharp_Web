using FootballBetting.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballBetting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var dbContext = new FootballBettingDbContext())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
