using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using sonesson_tools;

namespace IPTComShark
{
    public static class GitHubUpdateCheck
    {
        /// <summary>
        /// Checks the GitHub API for the latest version of owner/repo
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repo"></param>
        /// <returns></returns>
        public static string GetLatestVersion(string owner, string repo)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent",
                        "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    byte[] downloadData =
                        webClient.DownloadData($"https://api.github.com/repos/{owner}/{repo}/releases/latest");

                    string s = Encoding.ASCII.GetString(downloadData);

                    Match match = Regex.Match(s, "\"tag_name\":\"([\\.\\d]*)\",");
                    return match.Groups[1].Value;
                }
            }
            catch (Exception e)
            {
                Logger.Log("Version check failed", Severity.Warning, e);
                return null;
            }
        }

        /// <summary>
        /// Checks the GitHub API for the latest version of owner/repo in a seperate thread, then displays dialog with option to go to github if there is a newer version
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repo"></param>
        /// <param name="currentVersion"></param>
        public static void GetLatestVersionAndPromptAsync(string owner, string repo, string currentVersion)
        {
            ThreadPool.QueueUserWorkItem(thing =>
            {
                string latestVersion = GetLatestVersion(owner, repo);
                if (latestVersion == null)
                    return;

                var current = new Version(currentVersion);
                var latest = new Version(latestVersion);
                var compare = current.CompareTo(latest);

                if (compare < 0)
                {
                    Logger.Log($"Version check success, new version is available ({currentVersion} vs {latestVersion})",
                        Severity.Info);
                    if (MessageBox.Show(
                        $"New version of {repo} is available, {latestVersion}{Environment.NewLine}Go there now?",
                        "New Version Available", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                    ) == DialogResult.Yes)
                    {
                        Process.Start($"https://github.com/{owner}/{repo}/releases/latest");
                    }
                }
                else if (compare > 0)
                {
                    Logger.Log(
                        $"Version check success, application is newer then the one on github ({currentVersion} vs {latestVersion})",
                        Severity.Info);
                }
                else
                {
                    Logger.Log($"Version check success, application is up to date ({currentVersion})", Severity.Info);
                }
            });
        }
    }
}