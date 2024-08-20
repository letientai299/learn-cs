using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

await using var db = new BloggingContext(LogLevel.Debug);

// await TestEntryStateAsync(db, b);
await GenSomeDataAsync(db);

Header("Done, the rest should be clean up log from EF");
return;

async Task GenSomeDataAsync(BloggingContext ctx)
{
    Header("Insert some blogs");
    ctx.Add(new Blog { Url = "https://letientai.io" });
    ctx.Add(new Blog { Url = "https://blogs.msdn.com/adonet" });
    await ctx.SaveChangesAsync();

    Header("Querying for some blogs");
    var blogs = ctx.Blogs.OrderBy(b => b.Id).Take(10);
    Log(blogs);

    Header("Updating the blog and adding a post");
    var firstBlog = await blogs
        .OrderBy(b => b.Id)
        .Include(b => b.Posts) // eager loading
        .FirstAsync();
    firstBlog.Posts.Add(
        new Post
        {
            Title = $"Hello {firstBlog.Posts.Count}",
            Content = $"Post number {firstBlog.Posts.Count}",
        }
    );
    await ctx.SaveChangesAsync();

    Header("Read the updated blog by ID");
    var updatedBlog = await ctx
        .Blogs.OrderBy(b => b.Id)
        .FirstAsync(x => x.Id == firstBlog.Id);
    Log(updatedBlog);
    Log(updatedBlog.Posts);

    Header("Delete the blog");
    var secondBlog = await ctx.Blogs.OrderBy(b => b.Id).Skip(1).FirstAsync();
    ctx.Remove(secondBlog);
    await ctx.SaveChangesAsync();
}

async Task TestEntryStateAsync(BloggingContext ctx)
{
    var b = new Blog { Url = "http://localhost:5000" };
    Header("State of detached entity");
    Log(ctx.Entry(b).State);

    Header("State of attached, but not added entity");
    ctx.Attach(b);
    Log(ctx.Entry(b).State);

    Header("State of newly added entity");
    ctx.Add(b);
    Log(ctx.Entry(b).State);

    Header("State of modified, but not saved yet");
    b.Posts.Add(new Post { Title = "post 1" });
    Log(ctx.Entry(b).State);

    Header("State of after saving entity");
    await ctx.SaveChangesAsync();
    Log(ctx.Entry(b).State);

    Header("State of not modified after saved");
    var updated = ctx.Update(b);
    Log(updated.State, ctx.Entry(b).State);

    Header("State of modified and saved");
    b.Posts.Add(new Post { Title = "post 1" });
    updated = ctx.Update(b);
    await ctx.SaveChangesAsync();
    Log(updated.State, ctx.Entry(b).State);
}
