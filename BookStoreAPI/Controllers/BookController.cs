using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using BookStoreAPI.Entities;

namespace BookStoreAPI.Controllers; //bookstoreAPI est l'espace 

//est un annotation, elle permet de definir des métadonnées sur une classe 
//ici elle permet de définir que la calss BookController est un controller API 
// la décorateur
[ApiController]
[Route("api/[controller]")]//permet de ne plus mettre [HttpPost[books]]
public class BookController : ControllerBase
{
    [HttpGet("books")]
    public ActionResult<List<Book>> GetBooks()
    {

        var books = new List<Book>
        {
            new() { Id = 1, Title = "Le seigneur des anneaux", Author = "J.R.R Tolkien" }
        };

        return Ok(books);
    }

    // [HttpPost]
    // public CreateBook ([FromBody] Book book) //[FromBody] permet de dire ou il faut aller chercher dans le Frombody pour avoir le Book
    // {
    //     Console.WriteLine(book.Title);
    //     return CreatAtAction(nameof(GetBooks), new {id = book.Id}, book);
    // }
    [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            Console.WriteLine(book.Title);
            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }
}