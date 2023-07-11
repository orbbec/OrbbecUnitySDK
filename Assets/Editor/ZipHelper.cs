using System;
using System.Diagnostics;

class ZipHelper
{
    public static void ZipFiles(string zipFilePath, string sourceDirectory)
    {
        string command = "7z";
        string args = $"a -tzip {zipFilePath} {sourceDirectory}/*";
        RunCommand(command, args, "");
    }

    private static string RunCommand(string command, string args, string workingDirectory)
    {
        string output = string.Empty;

        using (Process process = new Process())
        {
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = args;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }

        return output;
    }
}
