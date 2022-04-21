using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Data.DTOs.Author;
using BookStoreApp.API.Data.Models;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDTO>>> GetAuthors()
        {
            try
            {
                var authorsDto = await _context.Authors
                    .ProjectTo<AuthorReadDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return Ok(authorsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(GetAuthors)}");
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDTO>> GetAuthor(int id)
        {
            try
            {
                var authorDto = await _context.Authors
                    .ProjectTo<AuthorReadDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (authorDto == null)
                {
                    _logger.LogWarning($"Record not found - {nameof(GetAuthor)} - {nameof(id)}:{id}");
                    return NotFound();
                }

                return Ok(authorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(GetAuthor)} - {nameof(id)}:{id}");
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDTO authorDto)
        {
            try
            {
                if (id != authorDto.Id)
                {
                    _logger.LogWarning($"Update ID invalid - {nameof(PutAuthor)} - {nameof(id)}:{id}", authorDto);
                    ModelState.TryAddModelError(nameof(authorDto.Id), $"Action {nameof(id)} and Model {nameof(authorDto.Id)} must match");
                    return BadRequest(ModelState);
                }

                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record not found - {nameof(PutAuthor)} - {nameof(id)}:{id}", authorDto);
                    return NotFound();
                }

                _mapper.Map(authorDto, author);
                _context.Entry(author).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AuthorExists(id))
                    {
                        _logger.LogWarning($"Record not found - {nameof(PutAuthor)} - {nameof(id)}:{id}", authorDto);
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(PutAuthor)} - {nameof(id)}:{id}", authorDto);
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDTO>> PostAuthor(AuthorCreateDTO authorDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDto);

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(PostAuthor)}", authorDto);
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"Record not found - {nameof(DeleteAuthor)} - {nameof(id)}:{id}");
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(DeleteAuthor)} - {nameof(id)}:{id}");
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
