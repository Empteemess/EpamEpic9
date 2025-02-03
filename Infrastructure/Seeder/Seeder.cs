using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;

namespace Infrastructure.Seeder;

public class Seeder
{
    private readonly AppDbContext _context;

    public Seeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        if (!_context.PaginationOptions.Any() && !_context.SortingOptions.Any() && !_context.DateFilters.Any())
        {
            // Seed pagination options
            var paginationOptions = new List<PaginationOption>
            {
                new PaginationOption { Id = Guid.NewGuid(), Option = "10" },
                new PaginationOption { Id = Guid.NewGuid(), Option = "20" },
                new PaginationOption { Id = Guid.NewGuid(), Option = "50" },
                new PaginationOption { Id = Guid.NewGuid(), Option = "100" },
                new PaginationOption { Id = Guid.NewGuid(), Option = "all" }
            };

            await _context.PaginationOptions.AddRangeAsync(paginationOptions);

            // Seed sorting options
            var sortingOptions = new List<SortingOption>
            {
                new SortingOption { Id = Guid.NewGuid(), Option = "Most popular" },
                new SortingOption { Id = Guid.NewGuid(), Option = "Most commented" },
                new SortingOption { Id = Guid.NewGuid(), Option = "Price ASC" },
                new SortingOption { Id = Guid.NewGuid(), Option = "Price DESC" },
                new SortingOption { Id = Guid.NewGuid(), Option = "New" }
            };

            await _context.SortingOptions.AddRangeAsync(sortingOptions);

            // Seed date filter options
            var dateFilters = new List<DateFilter>
            {
                new DateFilter { Id = Guid.NewGuid(), Option = "last week" },
                new DateFilter { Id = Guid.NewGuid(), Option = "last month" },
                new DateFilter { Id = Guid.NewGuid(), Option = "last year" },
                new DateFilter { Id = Guid.NewGuid(), Option = "2 years" },
                new DateFilter { Id = Guid.NewGuid(), Option = "3 years" }
            };

            await _context.DateFilters.AddRangeAsync(dateFilters);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        // Check if there are already any genres or platforms seeded
        if (!_context.Genres.Any() && !_context.Platforms.Any())
        {
            // Seed genres
            var genres = new List<Genre>
            {
                new Genre { Id = Guid.NewGuid(), Name = "Strategy" },
                new Genre { Id = Guid.NewGuid(), Name = "RTS" },
                new Genre { Id = Guid.NewGuid(), Name = "TBS" },
                new Genre { Id = Guid.NewGuid(), Name = "RPG" },
                new Genre { Id = Guid.NewGuid(), Name = "Sports" },
                new Genre { Id = Guid.NewGuid(), Name = "Races" },
                new Genre { Id = Guid.NewGuid(), Name = "Rally" },
                new Genre { Id = Guid.NewGuid(), Name = "Arcade" },
                new Genre { Id = Guid.NewGuid(), Name = "Formula" },
                new Genre { Id = Guid.NewGuid(), Name = "Off-road" },
                new Genre { Id = Guid.NewGuid(), Name = "Action" },
                new Genre { Id = Guid.NewGuid(), Name = "FPS" },
                new Genre { Id = Guid.NewGuid(), Name = "TPS" },
                new Genre { Id = Guid.NewGuid(), Name = "Adventure" },
                new Genre { Id = Guid.NewGuid(), Name = "Puzzle & Skill" }
            };

            await _context.Genres.AddRangeAsync(genres);

            // Seed platforms
            var platforms = new List<Platform>
            {
                new Platform { Id = Guid.NewGuid(), Type = "Mobile" },
                new Platform { Id = Guid.NewGuid(), Type = "Browser" },
                new Platform { Id = Guid.NewGuid(), Type = "Desktop" },
                new Platform { Id = Guid.NewGuid(), Type = "Console" }
            };

            await _context.Platforms.AddRangeAsync(platforms);
            
            // Seed payment methods
            var paymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "image link1",
                    Title = "Bank",
                    Description = "Some text 1"
                },
                new PaymentMethod
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "image link2",
                    Title = "IBox terminal",
                    Description = "Some text 2"
                },
                new PaymentMethod
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "image link3",
                    Title = "Visa",
                    Description = "Some text 3"
                }
            };

            await _context.PaymentMethods.AddRangeAsync(paymentMethods);
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}