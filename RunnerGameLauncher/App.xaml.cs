using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButtons = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;

using Application = System.Windows.Application;
namespace RunnerGameLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string currentVersion = string.Empty;
        string latestVersion = string.Empty;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AutoUpdater.Start("https://Narmalone.github.io/RunnerGameLauncher/");
            AutoUpdater.UpdateMode = Mode.Normal;
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            System.Diagnostics.Process.Start("cmd.exe", "/c netsh advfirewall set currentprofile state on");
            AutoUpdater.CheckForUpdateEvent -= AutoUpdater_CheckForUpdateEvent;
        }

        private async void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {


            // Récupérer la dernière version disponible sur Internet
            string directoryPath = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(directoryPath, "version.txt");

            try
            {
                currentVersion = File.ReadAllText(filePath);
                MessageBox.Show($"CurrentVersion = {currentVersion}"); // version dans nos files
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                await AsyncMethod();
                //latestVersion = new WebClient().DownloadString("https://Narmalone.github.io/RunnerGameLauncher/RunnerGameLauncher/version.txt"); //Version en ligne
                MessageBox.Show($"Sucessfully get the latest version {latestVersion}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (latestVersion != currentVersion && latestVersion != null)
            {
                // Affiche un message pour informer l'utilisateur qu'une mise à jour est disponible
                MessageBoxResult messageBoxResult = MessageBox.Show($"Une nouvelle version ({latestVersion}) est disponible. Voulez-vous la télécharger ?", "Mise à jour disponible", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Demander à l'utilisateur s'il souhaite désactiver le pare-feu Windows
                        DialogResult result = System.Windows.Forms.MessageBox.Show("Le téléchargement de la mise à jour semble bloqué. Voulez-vous désactiver temporairement le pare-feu Windows ?", "Pare-feu Windows bloquant", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                        // Si l'utilisateur a répondu "Oui", désactiver le pare-feu
                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("cmd.exe", "/c netsh advfirewall set currentprofile state off");
                            MessageBox.Show("Le pare-feu Windows a été désactivé temporairement. Veuillez relancer l'application pour poursuivre le téléchargement de la mise à jour.", "Pare-feu Windows désactivé", MessageBoxButtons.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Le téléchargement de la mise à jour a été interrompu en raison du pare-feu Windows. Veuillez désactiver temporairement le pare-feu pour poursuivre le téléchargement.", "Pare-feu Windows bloquant", MessageBoxButtons.OK, MessageBoxImage.Warning);
                        }
                        AutoUpdater.DownloadUpdate(args);
                        AutoUpdater.ApplicationExitEvent += AutoUpdaterOnApplicationExitEvent;
                    }
                    catch (WebException ex)
                    {
                        MessageBox.Show($"Une erreur s'est produite lors du téléchargement de la mise à jour : {ex.Message}", "Erreur de téléchargement", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur s'est produite lors de l'installation de la mise à jour : {ex.Message}", "Erreur d'installation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                // L'application est à jour
                MessageBox.Show("Vous utilisez la dernière version de l'application.");
            }
        }

        private void AutoUpdaterOnApplicationExitEvent()
        {
            AutoUpdater.ApplicationExitEvent -= AutoUpdaterOnApplicationExitEvent;
            Application.Current.Shutdown();
        }

        public async Task AsyncMethod()
        {
            string url = "https://Narmalone.github.io/RunnerGameLauncher/RunnerGameLauncher/version.txt";

            // best practice to create one HttpClient per Application and inject it
            HttpClient client = new HttpClient();

            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                using (HttpContent content = response.Content)
                {
                    var json = await content.ReadAsStringAsync(); // Version actuelle qu'il y'a dans le txt en string
                    latestVersion = json;
                        
                }
            }
        }
    }
   
}