using System;
using System.Diagnostics;

public class GitHelper
{
    public static string GetCommitHash(string repositoryPath)
    {
        string command = "git";
        string args = "rev-parse HEAD";
        string output = RunCommand(command, args, repositoryPath);
        return output.Trim().Substring(0, 8);
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
