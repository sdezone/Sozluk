using System;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Infrastructure;

namespace Sozluk.Infrastructure.Persistence.Context
{
	public class SeedData
	{

		private static List<User> GetUsers()
		{
			var result = new Faker<User>("tr")
				.RuleFor(i => i.Id, i => Guid.NewGuid())
				.RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
				.RuleFor(i => i.FirsName, i => i.Person.FirstName)
				.RuleFor(i => i.LastName, i => i.Person.LastName)
				.RuleFor(i => i.EmailAddress, i => i.Internet.Email())
				.RuleFor(i => i.UserName, i => i.Internet.UserName())
				.RuleFor(i => i.Password, i => PasswordEncryptor.Encryp(i.Internet.Password()))
				.RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
				.Generate(500);
			return result;
		}


		public async Task SeedAsync(IConfiguration configuration)
		{
			var dbContextBuilder = new DbContextOptionsBuilder();
			dbContextBuilder.UseSqlite(configuration.GetConnectionString("SozlukSqllite"));

			var context = new SozlukContext(dbContextBuilder.Options);

			var users = GetUsers();
			var usersIds = users.Select(i => i.Id);

			await context.Users.AddRangeAsync(users);

			var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
			int counter = 0;

			var entries = new Faker<Entry>("tr")
				.RuleFor(i => i.Id, i=> guids[counter++])
				.RuleFor(i => i.CreateDate,
				i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
				.RuleFor(i => i.Subject, i => i.Lorem.Sentence(5))
				.RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
				.RuleFor(i => i.CreatedById, i => i.PickRandom(usersIds))
				.Generate(150);

			await context.Entries.AddRangeAsync(entries);

			var comments = new Faker<EntryComment>("tr")
				.RuleFor(i => i.Id,i=> new Guid())
				.RuleFor(i => i.CreateDate,
				i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
				.RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
				.RuleFor(i => i.CreatedById, i => i.PickRandom(usersIds))
				.RuleFor(i => i.EntryId, i => i.PickRandom(guids))
				.Generate(1000);

			await context.EntryComments.AddRangeAsync(comments);
			
            await context.SaveChangesAsync();


        }

	}
}

