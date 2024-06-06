using Application.Dtos;
using DataAccess;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("user/{userId:int}/store/{storeId:int}/table", (DBContext context, int userId, int storeId, CreateTableDto dto) =>
{
    var store = context.Users
    .Include(x => x.Stores)
    .SingleOrDefault(x => x.Id == userId)?
    .Stores
    .SingleOrDefault(x => x.Id == storeId);

    if (store is null)
        return Results.BadRequest();

    var table = new Table
    {
        StoreId = dto.StoreId,
        Number = dto.Number,
        IsAvailable = dto.IsAvailable,
        SeatsNumber = dto.SeatsNumber
    };

    store.Tables.Add(table);

    context.SaveChanges();

    return Results.Created($"/table/{table.Id}", table);
});

app.MapPost("/user", (DBContext context, CreateUserDto dto) =>
{
    var user = context.Users.Add(new User {
        FullName = dto.FullName,
        Email = dto.Email,
        Password = dto.Password
    });

    context.SaveChanges();

    return Results.Created($"user/{user.Entity.Id}", user);
});

app.MapPost("user/{userId:int}/store/", (DBContext context, 
    [FromRoute]int userId, 
    [FromBody]string storeName) =>
{
    var store = context.Stores.Add(new Store
    {
        UserId = userId,
        Name = storeName,
    });

    context.SaveChanges();

    return Results.Created($"user/{store.Entity.UserId}/store/", store);
});

app.MapGet("/user/auth/{email}&{pass}", (DBContext context, string email, string pass) =>
{
    var isAuthenticated = context.Users.SingleOrDefault(x => x.Email == email && x.Password == pass && x.IsActive) != null;
    return isAuthenticated ? Results.Ok() : Results.Unauthorized();
});

app.MapGet("user/{userId:int}/store/", (DBContext context, int userId) =>
{
    var user = context.Users.Include(x => x.Stores).SingleOrDefault(x => x.Id == userId);
    return user is null ? Results.BadRequest() : Results.Ok(user.Stores);
});

app.MapDelete("user/{userId:int}/store/{storeId:int}", (DBContext context, int userId, int storeId) =>
{
    var store = context.Users
    .Include(x => x.Stores)
    .SingleOrDefault(x => x.Id == userId)?
    .Stores.SingleOrDefault(x => x.Id == storeId);
    
    if(store is null)
        return Results.BadRequest();

    context.Stores.Remove(store);
    context.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("user/{id:int}", (DBContext context, int id) =>
{
    var user = context.Users.Find(id);
    
    if(user is null)
        return Results.BadRequest();

    user.IsActive = false;
    context.SaveChanges();

    return Results.Ok();
});


app.MapDelete("user/{userId:int}/store/{storeId:int}table/{idTable:int}", (DBContext context, int userId, int storeId, int idTable) =>
{
    var table = context.Users
    .Include(x => x.Stores)
    .SingleOrDefault(x => x.Id == userId)?
    .Stores
    .AsQueryable().Include(x => x.Tables)
    .SingleOrDefault(x => x.Id == storeId)?
    .Tables
    .SingleOrDefault(x => x.Id == idTable);

    if (table is null)
        return Results.BadRequest();

    context.Tables.Remove(table);
    context.SaveChanges();

    return Results.NoContent();
});

app.Run();