using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using BookStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookStoreAPI.Models;

namespace BookStoreAPI.Controllers; //bookstoreAPI est l'espace 

//est un annotation, elle permet de definir des métadonnées sur une classe 
//ici elle permet de définir que la calss BookController est un controller API 
// la décorateur
[ApiController]
[Route("api/[controller]")]//permet de ne plus mettre [HttpPost[books]]
public class BookController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public BookController(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // [HttpGet("books")]
    // public ActionResult<List<Book>> GetBooks()
    // {

    //     var books = new List<Book>
    //     {
    //         new() { Id = 1, Title = "Le seigneur des anneaux", Author = "J.R.R Tolkien" }
    //     };

    //     return Ok(books);
    // }

    // méthode autoMapper 
    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetBooks()
    {
        var books = await _dbContext.Books.ToListAsync();

        var booksDto = books.Select(book => _mapper.Map<BookDto>(book)).ToList();

        return Ok(booksDto);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Book))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
    {

        if (book == null)
        {
            return BadRequest();
        }
        Book? addedBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Title == book.Title);
        if (addedBook != null)
        {
            return BadRequest("Book already exists");
        }
        else
        {
        
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return Created("api/book", book);

        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Book>> PutBook(int id, [FromBody] Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }
        var bookToUpdate = await _dbContext.Books.FindAsync(id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        _dbContext.Entry(bookToUpdate).CurrentValues.SetValues(book);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> DeleteBook(int id)
    {
        var bookToDelete = await _dbContext.Books.FindAsync(id);

        if (bookToDelete == null)
        {
            return NotFound();
        }

        _dbContext.Books.Remove(bookToDelete);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

}


