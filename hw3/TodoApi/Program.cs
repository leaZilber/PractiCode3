// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using TodoApi;

// var builder = WebApplication.CreateBuilder(args);

// // הוסף שירות CORS
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAllOrigins",
//         builder =>
//         {
//             builder.AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader();
//         });
// });

// // הוסף את DbContext לקונפיגורציה
// builder.Services.AddDbContext<ToDoDbContext>(options =>
//     options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"), 
//     Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql")));

// // הוסף את Swagger
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // הפעלת CORS
// app.UseCors("AllowAllOrigins");

// // // הפעלת Swagger
// // if (app.Environment.IsDevelopment())
// // {
//     app.UseSwagger();
//     app.UseSwaggerUI();
//     // app.UseSwaggerUI(c =>
//     // {
//     //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//     //     c.RoutePrefix = string.Empty; // זה יאפשר לך לגשת ל-Swagger בכתובת הראשית
//     // });
// // }

// app.MapGet("/", () => "Hello World!");

// // שליפת כל המשימות
// app.MapGet("/items", async (ToDoDbContext db) =>
//     await db.Items.ToListAsync());

// // הוספת משימה חדשה
// app.MapPost("/items", async (ToDoDbContext db, Item item) =>
// {
//     db.Items.Add(item);
//     await db.SaveChangesAsync();
//     return Results.Created($"/items/{item.Id}", item);
// });

// // עדכון משימה
// // app.MapPut("/items/{id}", async (int id, bool update,ToDoDbContext db, Item updatedItem) =>
// // {
// //     var item = await db.Items.FindAsync(id);
// //     if (item is null) return Results.NotFound();

// //     // item.Name = updatedItem.Name;
// //     item.IsComplete = update;

// //     await db.SaveChangesAsync();
// //     return Results.NoContent();
// // });
// app.MapPut("/items/{id}", async (int id, ToDoDbContext db, Item updatedItem) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     item.IsComplete = updatedItem.IsComplete; // עדכון הסטטוס

//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });


// // מחיקת משימה
// app.MapDelete("/items/{id}", async (int id, ToDoDbContext db) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     db.Items.Remove(item);
//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });

// app.Run();
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

// הוסף שירות CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// הוסף את DbContext לקונפיגורציה
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"), 
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql")));

// הוסף את Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// הפעלת CORS
app.UseCors("AllowAllOrigins");

// הפעלת Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

// שליפת כל המשימות
app.MapGet("/items", async (ToDoDbContext db) =>
    await db.Items.ToListAsync());

// הוספת משימה חדשה
app.MapPost("/items", async (ToDoDbContext db, Item item) =>
{
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{item.Id}", item);
});

// עדכון משימה
app.MapPut("/items/{id}", async (int id, ToDoDbContext db, Item updatedItem) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    item.IsComplete = updatedItem.IsComplete; // עדכון הסטטוס

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// מחיקת משימה
app.MapDelete("/items/{id}", async (int id, ToDoDbContext db) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

