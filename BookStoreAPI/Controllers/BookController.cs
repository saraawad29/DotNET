using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using BookStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BookStoreAPI.Models;

namespace BookStoreAPI.Controllers; //bookstoreAPI est l'espace 

// la décorateur /annotation
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
    //méthode renvoie une Task asynchrone contenant un objet Book
    public async Task<ActionResult<Book>> CreateBook([FromBody] BookCreateRequestDto bookDto)
    {
        if(bookDto == null)
        return BadRequest();

        var book = _mapper.Map<Book>(bookDto);

        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync(); //Enregistre les modifications dans db

        return CreatedAtAction(nameof(GetBooks), new {id = book.Id}, book);
    }

    [HttpPost("validationTest")]
    public ActionResult ValidationTest([FromBody] BookDto book)
    {
        if (book.Title == null)
        {
            ModelState.AddModelError("Title", "Le titre du livre est requis.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok();
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
