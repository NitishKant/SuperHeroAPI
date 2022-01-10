using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero {
                Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Praker",
                    Place = "New York"
                }
        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
           _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            //var heros = new List<SuperHero>
            //{
            //    new SuperHero
            //{
            //    Id = 1,
            //        Name = "Spider Man",
            //        FirstName = "Peter",
            //        LastName = "Praker",
            //        Place = "New York"
            //    }
            //};
            //return Ok(heroes);

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int Id)
        {
            //var hero = heroes.Find(x => x.Id == Id);
            var hero = await _context.SuperHeroes.FindAsync(Id);
            if (hero == null)
                return BadRequest("Not Hero");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //heroes.Add(hero);
            //return Ok(heroes);

            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            //var hero = heroes.Find(x => x.Id == request.Id);
            var hero = await _context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest("Not Hero");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await _context.SaveChangesAsync();
            //return Ok(hero);
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int Id)
        {
            //var hero = heroes.Find(x => x.Id == Id);
            var hero = await _context.SuperHeroes.FindAsync(Id);
            if (hero == null)
                return BadRequest("Not Hero");

            //heroes.Remove(hero);
            //return Ok(hero);
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
