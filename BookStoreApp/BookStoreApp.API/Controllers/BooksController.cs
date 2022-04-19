using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Data.DTOs.Book;
using BookStoreApp.API.Data.Models;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<BooksController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadDTO>>> GetBooks()
        {
            try
            {
                var booksDto = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookReadDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return Ok(booksDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(GetBooks)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDTO>> GetBook(int id)
        {
            try
            {
                var bookDto = await _context.Books
                    .Include(q => q.Author)
                    .ProjectTo<BookDetailsDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (bookDto == null)
                {
                    _logger.LogWarning($"Record not found - {nameof(GetBook)} - {nameof(id)}:{id}");
                    return NotFound();
                }

                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(GetBook)} - {nameof(id)}:{id}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDTO bookDto)
        {
            try
            {
                if (id != bookDto.Id)
                {
                    _logger.LogWarning($"Update ID invalid - {nameof(PutBook)} - {nameof(id)}:{id}", bookDto);
                    ModelState.TryAddModelError(nameof(bookDto.Id), $"Action {nameof(id)} and Model {nameof(bookDto.Id)} must match");
                    return BadRequest(ModelState);
                }

                var book = await _context.Authors.FindAsync(id);

                if (book == null)
                {
                    _logger.LogWarning($"Record not found - {nameof(PutBook)} - {nameof(id)}:{id}", bookDto);
                    return NotFound();
                }

                _mapper.Map(bookDto, book);
                _context.Entry(book).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookExists(id))
                    {
                        _logger.LogWarning($"Record not found - {nameof(PutBook)} - {nameof(id)}:{id}", bookDto);
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
                _logger.LogError(ex, $"Error - {nameof(PutBook)} - {nameof(id)}:{id}", bookDto);
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCreateDTO>> PostBook(BookCreateDTO bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);

                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(PostBook)}", bookDto);
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning($"Record not found - {nameof(DeleteBook)} - {nameof(id)}:{id}");
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error - {nameof(DeleteBook)} - {nameof(id)}:{id}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> BookExists(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
}
