using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Data;
using LibraryApi.Models;

namespace LibraryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryContext _ctx;
    public BooksController(LibraryContext ctx) => _ctx = ctx;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _ctx.Books.Include(b => b.Author).ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await _ctx.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        _ctx.Books.Add(book);
        await _ctx.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Book book)
    {
        var existing = await _ctx.Books.FindAsync(id);
        if (existing is null) return NotFound();

        existing.Title = book.Title;
        existing.Genre = book.Genre;
        existing.Year = book.Year;
        existing.AuthorId = book.AuthorId;

        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _ctx.Books.FindAsync(id);
        if (book is null) return NotFound();

        _ctx.Books.Remove(book);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
