namespace Example.Utils;

public static class Fs
{
    public static string TopBinDir()
    {
        var editorConfig = FindUp(".editorconfig");
        var solutionDir = Path.GetDirectoryName(editorConfig);
        var dir = Path.Join(solutionDir, "bin");
        Directory.CreateDirectory(dir);
        return dir;
    }

    public static string FindUp(string name, string dir = "")
    {
        dir = dir.Length != 0 ? dir : Directory.GetCurrentDirectory();
        while (dir != "/")
        {
            var files = Directory.GetFileSystemEntries(dir, name);
            if (files.Length == 1)
            {
                return files[0];
            }

            dir = Path.GetDirectoryName(dir) ?? "/";
        }

        throw new FileNotFoundException("Could not find init.sql");
    }
}
