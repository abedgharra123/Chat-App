using System;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class DataBase(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
