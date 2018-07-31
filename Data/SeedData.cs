using Moogle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moogle.Data
{
    public static class SeedData
    {
        public static void SeedDB(CharacterContext context)
        {
            if (context.Character.Any() || context.Games.Any() || context.Monsters.Any())
            {
                return;   // DB has been seeded
            }

            context.Character.AddRange(
                new Characters
                {
                    Name = "Cecil Harvey",
                    Age = "20",
                    Gender = "Male",
                    Race = "Half-Lunarian",
                    Job = "Dark Knight/Paladin",
                    Height = "1.78",
                    Weight = "58",
                    Description = "??",
                    Picture = "??",
                    Origin = "Final Fantasy 4"
                });
                /*
                new Characters
                {
                    Name = "Rosa Joanna Farrell",
                    Age = "??",
                    Gender = "??",
                    Race = "??",
                    Job = "??",
                    Height = "??",
                    Weight = "??",
                    Description = "??",
                    Picture = "??",
                    Origin = "Final Fantasy 4"
                },
                new Characters
                {
                    Name = "Edge Geraldine",
                    Age = "??",
                    Gender = "??",
                    Race = "??",
                    Job = "??",
                    Height = "??",
                    Weight = "??",
                    Description = "??",
                    Picture = "??",
                    Origin = "Final Fantasy 4"
                },
                new Characters
                {
                    Name = "Rydia",
                    Age = "??",
                    Gender = "??",
                    Race = "??",
                    Job = "??",
                    Height = "??",
                    Weight = "??",
                    Description = "??",
                    Picture = "??",
                    Origin = "Final Fantasy 4"
                },
                new Characters
                {
                    Name = "Kain Highwind",
                    Age = "??",
                    Gender = "??",
                    Race = "??",
                    Job = "??",
                    Height = "??",
                    Weight = "??",
                    Description = "??",
                    Picture = "??",
                    Origin = "Final Fantasy 4"
                }
            );
            
            context.Games.AddRange(
                new Game
                {
                    Title = "Final Fantasy 4"
                }
            );

            context.Monsters.AddRange(
                new Monster
                {
                    Name = "Chocobo"
                }
            );
            */

            context.SaveChanges();
        }
    }
}