using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BookStoreAPI.Entities;

namespace BookStoreAPI.Controllers; //bookstoreAPI est l'espace 

//est un annotation, elle permet de definir des métadonnées sur une classe 
//ici elle permet de définir que la calss BookController est un controller API 
// la décorateur
[ApiController]
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
}