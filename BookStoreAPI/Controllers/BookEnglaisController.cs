using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BookStoreAPI.Entities;

namespace BookStoreAPI.Controllers; //bookstoreAPI est l'espace 

// la décorateur
[ApiController]
public class BookEnglaisController : ControllerBase
{
    [HttpGet("BookEnglais")]
    public ActionResult<List<BookEnglais>> GetBooks()
    {

        var livre = new List<BookEnglais>
        {
            new() { Id = 1, Title = "Le seigneur des anneaux", Author = "J.R.R Tolkien" }
        };

        return Ok(livre);

    }
}