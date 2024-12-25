using System;
using System.Models;
using Microsoft.EntityFrameworkCore;


namespace system.context;

public class SongContext:DbContext
{
    public SongContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
    public DbSet<Song> Songs => Set<Song>();


}