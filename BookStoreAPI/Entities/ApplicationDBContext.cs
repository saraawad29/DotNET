using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Entities;

public class ApplicationDbContext: DbContext
{
    // c'est la constructeur
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    //surcharger de la méthode OnConfiguring (lors de la configuration) qui permet de specifier la connection string à utiliser 
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // recupere le chemin du dossier courant lors de l'execution de l'application
        var currentDir = Directory.GetCurrentDirectory();

        // combine le chemin du dossier courant avec le nom du fichier de la base de données
        var dbPath = Path.Combine(currentDir, "bookstore.db");
        Console.WriteLine($"dbPath: {dbPath}");
        optionsBuilder.UseSqlite($"Filename=P{dbPath}");

    }
    public DbSet<Book> Books { get; set;} = default!; // Books c'est le nom de la table dasn DB; Book entity 
}