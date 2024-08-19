namespace Example.Utils;

public static class Fs
{
    public static string FindUp(string fileName, string dir = "")
    {
        dir = dir.Length != 0 ? dir : Directory.GetCurrentDirectory();
        while (dir != "/")
        {
            var files = Directory.GetFiles(dir, fileName);
            if (files.Length == 1)
            {
                return files[0];
            }

            dir = Path.GetDirectoryName(dir) ?? "/";
        }

        throw new FileNotFoundException("Could not find init.sql");
    }
}
