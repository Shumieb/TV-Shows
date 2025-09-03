using Microsoft.EntityFrameworkCore;
using TV_Shows.DTOs;
using TV_Shows.Models;
using TV_Shows.Services;

var builder = WebApplication.CreateBuilder(args);
// services
builder.Services.AddDbContext<ShowsDb>(opt => opt.UseInMemoryDatabase("ShowList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// show routes
RouteGroupBuilder shows = app.MapGroup("/shows");
shows.MapGet("/", GetAllShows);
shows.MapGet("/{id}", GetShow);
shows.MapPost("/", CreateShow);
shows.MapPut("/{id}", UpdateShow);
shows.MapDelete("/{id}", DeleteShow);

// platform routes
RouteGroupBuilder platforms = app.MapGroup("/platforms");
platforms.MapGet("/", GetAllPlatforms);
platforms.MapGet("/{id}", GetPlatform);
platforms.MapPost("/", CreatePlatform);
platforms.MapPut("/{id}", UpdatePlatform);
platforms.MapDelete("/{id}", DeletePlatform);

app.Run();

// get all shows
static async Task<IResult> GetAllShows(ShowsDb db)
{
    return TypedResults.Ok(
        await db.Shows
        .Include(x=> x.Platform)
        .Select(x=> new ShowDTO(x))
        .ToArrayAsync());
}

// get a show by ID
static async Task<IResult> GetShow(int id, ShowsDb db)
{
    var show = await db.Shows
        .Include(x => x.Platform)
        .FirstOrDefaultAsync(x => x.Id == id);

    return show is not null
        ? TypedResults.Ok(show)
        : TypedResults.NotFound();
}

// create a show
static async Task<IResult> CreateShow(Show show,ShowsDb db)
{
    db.Shows.Add(show);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/cars/{show.Id}", show);
}

// update a show
static async Task<IResult> UpdateShow(int id, Show show,ShowsDb db)
{

    var foundShow = await db.Shows.FindAsync(id);

    if (foundShow is null) return TypedResults.NotFound();

    foundShow.Name = show.Name;
    foundShow.Season = show.Season;
    foundShow.Episode = show.Episode;
    foundShow.PlatformID = show.PlatformID;
    foundShow.Note = show.Note;
    foundShow.Like = show.Like;

    await db.SaveChangesAsync();
    return TypedResults.Ok(foundShow);
}

// delete a show
static async Task<IResult> DeleteShow(int id, ShowsDb db)
{
    if (await db.Shows.FindAsync(id) is Show show)
    {
        db.Shows.Remove(show);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NoContent();
}

// get all platforms
static async Task<IResult> GetAllPlatforms(ShowsDb db)
{
    var platforms = await db.Platforms.ToArrayAsync();

    if(platforms.Length == 0)
    {
         List < Platform > mockData = [
            new Platform{Id=1, Name="Netflix"},
            new Platform{Id=2, Name="Amazon Prime"},
            new Platform{Id=3, Name="Disney"},
            new Platform{Id=4, Name="Hulu"},
        ];

        foreach (Platform platform in mockData) {
            db.Platforms.Add(platform);
            await db.SaveChangesAsync();
        }
    }

    return TypedResults.Ok(await db.Platforms.ToArrayAsync());
}

// get a platform by Id
static async Task<IResult> GetPlatform(int id, ShowsDb db)
{
    return await db.Platforms.FindAsync(id)
        is Platform platform
            ? TypedResults.Ok(platform)
            : TypedResults.NotFound();
}

// create platform
static async Task<IResult> CreatePlatform(Platform platform, ShowsDb db)
{
    db.Platforms.Add(platform);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/cars/{platform.Id}", platform);
}

// update platform
static async Task<IResult> UpdatePlatform(int id, Platform platform, ShowsDb db)
{

    var foundPlatform = await db.Platforms.FindAsync(id);

    if (foundPlatform is null) return TypedResults.NotFound();

    foundPlatform.Name = platform.Name;

    await db.SaveChangesAsync();
    return TypedResults.Ok(foundPlatform);
}

// delete platform
static async Task<IResult> DeletePlatform(int id, ShowsDb db)
{
    if (await db.Platforms.FindAsync(id) is Platform platform)
    {
        db.Platforms.Remove(platform);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NoContent();
}





