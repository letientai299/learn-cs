using System.Collections;
using System.Text;
using Arg = System.Runtime.CompilerServices.CallerArgumentExpressionAttribute;
using File = System.Runtime.CompilerServices.CallerFilePathAttribute;
using Line = System.Runtime.CompilerServices.CallerLineNumberAttribute;

namespace Example.Utils;

#pragma warning disable RCS1163 // Unused parameter
#pragma warning disable IDE0060 // Remove unused parameter
// ReSharper disable UnusedParameter.Global
public static class Logs
{
    private const string Line =
        "------------------------------------------------------------";

    private static readonly object MsgLock = new();

    public static void Header(string s) =>
        WriteLine($"\n{Colors.BoldCyan}{s}\n{Line}{Colors.Reset}");

    public static void LogTv(
        object? a,
        [Arg(nameof(a))] string arg = "",
        [File] string path = "",
        [Line] int line = 0
#pragma warning disable S3236 // Caller information arguments should not be provided explicitly
    )
    {
        // ReSharper disable ExplicitCallerInfoArgument
        Log(a?.GetType(), a, null, $"type of {arg}", arg, path, line);
    }
#pragma warning restore S3236 // Caller information arguments should not be provided explicitly

    public static void Log(
        dynamic? a,
        Func<int>? skip = null,
        [Arg(nameof(a))] string arg = "",
        [File] string path = "",
        [Line] int line = 0
    ) => Print(path, line, (arg, a));

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [File] string path = "",
        [Line] int line = 0
    ) => Print(path, line, (arg1, a1), (arg2, a2));

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        dynamic? a3,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [Arg(nameof(a3))] string arg3 = "",
        [File] string path = "",
        [Line] int line = 0
    ) => Print(path, line, (arg1, a1), (arg2, a2), (arg3, a3));

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        dynamic? a3,
        dynamic? a4,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [Arg(nameof(a3))] string arg3 = "",
        [Arg(nameof(a4))] string arg4 = "",
        [File] string path = "",
        [Line] int line = 0
    ) => Print(path, line, (arg1, a1), (arg2, a2), (arg3, a3), (arg4, a4));

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        dynamic? a3,
        dynamic? a4,
        dynamic? a5,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [Arg(nameof(a3))] string arg3 = "",
        [Arg(nameof(a4))] string arg4 = "",
        [Arg(nameof(a5))] string arg5 = "",
        [File] string path = "",
        [Line] int line = 0
    ) =>
        Print(
            path,
            line,
            (arg1, a1),
            (arg2, a2),
            (arg3, a3),
            (arg4, a4),
            (arg5, a5)
        );

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        dynamic? a3,
        dynamic? a4,
        dynamic? a5,
        dynamic? a6,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [Arg(nameof(a3))] string arg3 = "",
        [Arg(nameof(a4))] string arg4 = "",
        [Arg(nameof(a5))] string arg5 = "",
        [Arg(nameof(a6))] string arg6 = "",
        [File] string path = "",
        [Line] int line = 0
    ) =>
        Print(
            path,
            line,
            (arg1, a1),
            (arg2, a2),
            (arg3, a3),
            (arg4, a4),
            (arg5, a5),
            (arg6, a6)
        );

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        dynamic? a3,
        dynamic? a4,
        dynamic? a5,
        dynamic? a6,
        dynamic? a7,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [Arg(nameof(a3))] string arg3 = "",
        [Arg(nameof(a4))] string arg4 = "",
        [Arg(nameof(a5))] string arg5 = "",
        [Arg(nameof(a6))] string arg6 = "",
        [Arg(nameof(a7))] string arg7 = "",
        [File] string path = "",
        [Line] int line = 0
    ) =>
        Print(
            path,
            line,
            (arg1, a1),
            (arg2, a2),
            (arg3, a3),
            (arg4, a4),
            (arg5, a5),
            (arg6, a6),
            (arg7, a7)
        );

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        dynamic? a3,
        dynamic? a4,
        dynamic? a5,
        dynamic? a6,
        dynamic? a7,
        dynamic? a8,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [Arg(nameof(a3))] string arg3 = "",
        [Arg(nameof(a4))] string arg4 = "",
        [Arg(nameof(a5))] string arg5 = "",
        [Arg(nameof(a6))] string arg6 = "",
        [Arg(nameof(a7))] string arg7 = "",
        [Arg(nameof(a8))] string arg8 = "",
        [File] string path = "",
        [Line] int line = 0
    ) =>
        Print(
            path,
            line,
            (arg1, a1),
            (arg2, a2),
            (arg3, a3),
            (arg4, a4),
            (arg5, a5),
            (arg6, a6),
            (arg7, a7),
            (arg8, a8)
        );

    public static void Log(
        dynamic? a1,
        dynamic? a2,
        dynamic? a3,
        dynamic? a4,
        dynamic? a5,
        dynamic? a6,
        dynamic? a7,
        dynamic? a8,
        dynamic? a9,
        Func<int>? skip = null,
        [Arg(nameof(a1))] string arg1 = "",
        [Arg(nameof(a2))] string arg2 = "",
        [Arg(nameof(a3))] string arg3 = "",
        [Arg(nameof(a4))] string arg4 = "",
        [Arg(nameof(a5))] string arg5 = "",
        [Arg(nameof(a6))] string arg6 = "",
        [Arg(nameof(a7))] string arg7 = "",
        [Arg(nameof(a8))] string arg8 = "",
        [Arg(nameof(a9))] string arg9 = "",
        [File] string path = "",
        [Line] int line = 0
    ) =>
        Print(
            path,
            line,
            (arg1, a1),
            (arg2, a2),
            (arg3, a3),
            (arg4, a4),
            (arg5, a5),
            (arg6, a6),
            (arg7, a7),
            (arg8, a8),
            (arg9, a9)
        );

    private static void Print(
        string path,
        int line,
        params (string, dynamic?)[] args
    )
    {
        var file = Path.GetFileName(path);
        var sb = new StringBuilder()
            .Append(Colors.Green)
            .Append(file)
            .Append(':')
            .Append(line)
            .Append(args.Length > 1 ? "\n" : string.Empty);

        foreach (var (key, val) in args)
        {
            sb.Append('\t')
                .Append(Colors.Blue)
                .Append(key)
                .Append(Colors.Reset)
                .Append(" = ");

#pragma warning disable IL2026
            Serialize(sb, val);
#pragma warning restore IL2026
            sb.AppendLine();
        }

        var output = sb.ToString();

        lock (MsgLock)
        {
            Write(output);
        }
    }

    private static void Serialize(StringBuilder sb, dynamic? val)
    {
        switch (val)
        {
            case null:
                sb.Append("null");
                return;
            case string s:
                sb.Append(s);
                return;
            case IEnumerable arr:
                SerializeEnumerable(sb, arr);
                return;
            default:
#pragma warning disable IL2026
                sb.AppendFormat("{0}", val);
#pragma warning restore IL2026
                break;
        }
    }

    private static void SerializeEnumerable(StringBuilder sb, IEnumerable arr)
    {
        var cnt = 0;
        foreach (var v in arr)
        {
            sb.AppendLine(cnt == 0 ? "[" : ", ");
            sb.Append(
                $"{cnt, -2} -> {Colors.Yellow}{v.GetType()}{Colors.Reset} {v}"
            );
            cnt++;
        }
        sb.AppendLine(cnt == 0 ? "[]" : "\n]");
    }
}
#pragma warning restore RCS1163 // Unused parameter
#pragma warning restore IDE0060 // Remove unused parameter
