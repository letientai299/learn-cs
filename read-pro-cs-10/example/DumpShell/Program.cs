using static System.Console;
using static Utils.Colors;
using static Utils.Logs;

namespace Example;

#pragma warning disable S1172
internal static class Program
{
    private static readonly string DirSep = $"{Path.DirectorySeparatorChar}";

    private static bool done;

    private static void Main(string[] args)
    {
        var cmd = args.Length > 0 ? args[0] : string.Empty;

        if (!string.IsNullOrEmpty(cmd))
        {
            Handle(cmd);
            return;
        }

        while (!done)
        {
            cmd = Input();
            Handle(cmd);
            WriteLine();
        }
    }

    private static string Input()
    {
        Write(Blue);
        Prompt();
        var cmd = ReadLine() ?? string.Empty;
        Write(Reset);
        return cmd;
    }

    private static void Prompt()
    {
        var curDir = Directory.GetCurrentDirectory();
        var dirs = curDir.Split(DirSep);
        var dirContext =
            dirs.Length > 3
                ? $"...{DirSep}" + string.Join(DirSep, dirs.TakeLast(4))
                : curDir;
        Write($"{dirContext}\n> ");
    }

    private static void Handle(string cmd)
    {
        var ss = cmd.Split(" ");
        var name = ss[0];
        var args = ss[1..];
        Handlers.GetValueOrDefault(name, new Cmd(Help, string.Empty)).F(args);
    }

    private sealed record Cmd(Action<string[]> F, string Guide);

    private static readonly Cmd ExitCmd =
        new(_ => done = true, "exit the application");

    private static readonly Dictionary<string, Cmd> Handlers =
        new()
        {
            { "pwd", new Cmd(Pwd, "get current dir") },
            { "ls", new Cmd(Ls, "list items in current dir") },
            { "cd", new Cmd(Cd, "change working dir") },
            { "quit", ExitCmd },
            { "exit", ExitCmd },
            { "q", ExitCmd },
        };

    private static void Help(params string[] args)
    {
        WriteLine("Supported commands are:\n");
        var groups =
            from p in Handlers
            group p.Key by p.Value into g
            select new { Cmd = g.Key, Names = g.ToList() };

        foreach (var g in groups)
        {
            WriteLine($"  {string.Join(", ", g.Names), -12}\t{g.Cmd.Guide}");
        }
    }

    private static void Pwd(params string[] args) =>
        WriteLine(Directory.GetCurrentDirectory());

    private static void Ls(params string[] args)
    {
        var curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        var children =
            from item in curDir.EnumerateFileSystemInfos()
            let isDir = IsDir(item)
            let suffix = isDir
                ? Path.DirectorySeparatorChar.ToString()
                : string.Empty
            let child = Path.GetRelativePath(curDir.FullName, item.FullName)
            orderby isDir descending
            select child + suffix;

        foreach (var child in children)
        {
            WriteLine(child);
        }
    }

    private static bool IsDir(FileSystemInfo item)
    {
        return (item.Attributes & FileAttributes.Directory) != 0;
    }

    private static void Cd(params string[] args)
    {
        var dst = args.Length > 0 && args[0].Length > 0 ? args[0] : ".";
        TryAndIgnoreAllException(() => Directory.SetCurrentDirectory(dst));
        Write("Current location: ");
        Pwd();
    }

    private static void TryAndIgnoreAllException(Action act)
    {
        try
        {
            act();
        }
#pragma warning disable CA1031
        catch (Exception exception)
#pragma warning restore CA1031
        {
            Log(exception);
            WriteLine();
        }
    }
}
#pragma warning restore S1172
