using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

await using var db = new BloggingContext(LogLevel.Trace);

Header("Insert some blogs");
db.Add(new Blog { Url = "https://letientai.io" });
db.Add(new Blog { Url = "https://blogs.msdn.com/adonet" });
await db.SaveChangesAsync();

Header("Querying for some blogs");
var blogs = db.Blogs.OrderBy(b => b.Id).Take(10);
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
await db.SaveChangesAsync();

Header("Read the updated blog by ID");
var updatedBlog = await db
    .Blogs.OrderBy(b => b.Id)
    .FirstAsync(x => x.Id == firstBlog.Id);
Log(updatedBlog);
Log(updatedBlog.Posts);

Header("Delete the blog");
var secondBlog = await db.Blogs.OrderBy(b => b.Id).Skip(1).FirstAsync();
db.Remove(secondBlog);
await db.SaveChangesAsync();

Header("Done, the rest should be clean up log from EF");
