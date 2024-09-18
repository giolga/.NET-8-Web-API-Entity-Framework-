using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Models;

namespace SuperHeroAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperHero>>> GetAllHeros()
        {
            var heros = await _context.SuperHeroes.ToListAsync();

            return Ok(heros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> Post(SuperHero superHero)
        {
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero hero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(hero.Id);

            if(dbHero == null)
            {
                return NotFound("Hero not found");
            }

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _context.SaveChangesAsync();

            return Ok(hero);
        }

        [HttpDelete]
        public async Task<ActionResult<SuperHero>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if(hero is null)
            {
                return NotFound($"Hero with the id {id} not found");
            }

            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
