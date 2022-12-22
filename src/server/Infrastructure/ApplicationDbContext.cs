using Domain.Entities;
using Infrastructure.Configurations.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
	public DbSet<Message> Messages => Set<Message>();
	public DbSet<User> Users => Set<User>();

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageConfiguration).Assembly);
	}
}