using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RunnerGameLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AutoUpdater.Start("https://Narmalone.github.io/RunnerGameLauncher/");
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AutoUpdater.CheckForUpdateEvent -= AutoUpdater_CheckForUpdateEvent;
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {

            string currentVersion = string.Empty;
            string latestVersion = string.Empty;

            // Récupérer la dernière version disponible sur Internet
            string directoryPath = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(directoryPath, "version.txt");

            try
            {
                currentVersion = File.ReadAllText(filePath);
                MessageBox.Show($"CurrentVersion = {currentVersion}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                latestVersion = new WebClient().DownloadString("https://Narmalone.github.io/RunnerGameLauncher/version.txt");
                MessageBox.Show($"Sucessfully get the latest version {latestVersion}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (args.IsUpdateAvailable)
            {
                if (latestVersion != currentVersion)
                {
                    // Affiche un message pour informer l'utilisateur qu'une mise à jour est disponible
                    MessageBoxResult messageBoxResult = MessageBox.Show($"Une nouvelle version ({latestVersion}) est disponible. Voulez-vous la télécharger ?", "Mise à jour disponible", MessageBoxButton.YesNo);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        // Télécharge et installe la dernière version disponible
                        AutoUpdater.DownloadUpdate(args);
                    }
                }
            }
            else
            {
                // L'application est à jour
                MessageBox.Show("Vous utilisez la dernière version de l'application.");
            }
        }
    }
}
