using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace VideosApi.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<Videos>  TableVideos { get; set; }

	}
}