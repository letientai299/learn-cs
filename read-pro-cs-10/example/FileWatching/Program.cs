// watch current dir if there's no arg

var target = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

var watcher = new FileSystemWatcher(target)
{
    // don't filter anything
    NotifyFilter = Enum.GetValues<NotifyFilters>()
        .Aggregate((NotifyFilters)0, (a, f) => a | f),
    IncludeSubdirectories = true,
};

watcher.Changed += (sender, args) =>
    Console.WriteLine(
        $"{sender}:"
            + $"\n  ChangeType: {args.ChangeType}"
            + $"\n  FullPath: {args.FullPath}"
            + $"\n  Name: {args.Name}"
    );

watcher.Created += (sender, args) =>
    Console.WriteLine(
        $"{sender}:"
            + $"\n  ChangeType: {args.ChangeType}"
            + $"\n  FullPath: {args.FullPath}"
            + $"\n  Name: {args.Name}"
    );

watcher.Deleted += (sender, args) =>
    Console.WriteLine(
        $"{sender}:"
            + $"\n  ChangeType: {args.ChangeType}"
            + $"\n  FullPath: {args.FullPath}"
            + $"\n  Name: {args.Name}"
    );

watcher.Renamed += (sender, args) =>
    Console.WriteLine(
        $"{sender}:"
            + $"\n  ChangeType: {args.ChangeType}"
            + $"\n  OldFullPath: {args.OldFullPath}"
            + $"\n  FullPath: {args.FullPath}"
            + $"\n  OldName: {args.OldName}"
            + $"\n  Name: {args.Name}"
    );

watcher.EnableRaisingEvents = true; // begin watching

Console.WriteLine($"watching {target}");
Console.WriteLine("Press any key to stop");
Console.ReadKey();
