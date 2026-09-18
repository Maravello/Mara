using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Text.Json;

namespace Mara.Services
{
    internal class ActionService
    {
        public async Task Executer(string json)
        {
            using JsonDocument jsonDocument =
                JsonDocument.Parse(json);

            string action =
                jsonDocument.RootElement
                    .GetProperty("action")
                    .GetString() ?? "";

            // =========================
            // RECHERCHE IMAGE
            // =========================

            if (action == "search_image")
            {
                string query =
                    jsonDocument.RootElement
                        .GetProperty("query")
                        .GetString() ?? "";

                ImageSearchService imageSearchService =
                    new ImageSearchService();

                string imageUrl =
                    await imageSearchService.RechercherImage(query);

                imageVisualisation fenetre =
                    new imageVisualisation(imageUrl);

                fenetre.Show();
            }
            // =========================
            // Info systeme
            // =========================

            else if (action == "system_info")
            {
                string type = jsonDocument.RootElement
                        .GetProperty("type")
                        .GetString() ?? "";

                if (type == "ram")
                {
                    ComputerInfo computerInfo = new ComputerInfo();

                    ulong ramDisponible = computerInfo.AvailablePhysicalMemory;

                    ulong ramTotale =
                                    computerInfo.TotalPhysicalMemory;

                    double ramDisponibleGo =
                        ramDisponible / 1024.0 / 1024.0 / 1024.0;

                    double ramTotaleGo =
                        ramTotale / 1024.0 / 1024.0 / 1024.0;

                    MessageBox.Show(
            $"RAM totale : {ramTotaleGo:F1} Go\n" +
            $"RAM disponible : {ramDisponibleGo:F1} Go"
        );
                }

                if (type == "cpu")
                {
                    using var chercheur = new System.Management.ManagementObjectSearcher("SELECT Name FROM Win32_Processor");

                    foreach (var processeur in chercheur.Get())
                    {
                        string nomDuProcesseur = processeur["name"]?.ToString() ?? "Inconnu";

                        MessageBox.Show(
           $"Nom du processeur : {nomDuProcesseur}"
       );

                        break;
                    }
                }
                if (type == "disk")
                {
                    DriveInfo disque = new DriveInfo("C");
                    DriveInfo disqueD = new DriveInfo("D");

                    long espaceLibre = disque.TotalSize;
                    long espaceTotal = disque.AvailableFreeSpace;

                    long espaceLibreD = disqueD.TotalSize;
                    long espaceTotalD = disqueD.AvailableFreeSpace;

                    double totalGo =
        espaceTotal / 1024.0 / 1024.0 / 1024.0;

                    double libreGo =
                        espaceLibre / 1024.0 / 1024.0 / 1024.0;

                    double totalGoD =
                        espaceTotalD / 1024.0 / 1024.0 / 1024.0;

                    double libreGoD =
                        espaceLibreD / 1024.0 / 1024.0 / 1024.0;

                    MessageBox.Show(
                        $"Disque C:\n" +
                        $"Espace total : {totalGo:F1} Go\n" +
                        $"Espace libre : {libreGo:F1} Go"
                    );
                    MessageBox.Show(
                        $"Disque D:\n" +
                        $"Espace total : {totalGoD:F1} Go\n" +
                        $"Espace libre : {libreGoD:F1} Go"
                    );
                }
                else if (type == "gpu")
                {
                    using var searcher =
                        new ManagementObjectSearcher(
                            "SELECT Name FROM Win32_VideoController"
                        );

                    foreach (var gpu in searcher.Get())
                    {
                        string gpuName =
                            gpu["Name"]?.ToString() ?? "Inconnu";

                        MessageBox.Show(
                            $"Carte graphique : {gpuName}"
                        );

                        break;
                    }
                }
            }

            // =========================
            // OUVRIR UNE URL
            // =========================

            else if (action == "open_url")
            {
                string url =
                    jsonDocument.RootElement
                        .GetProperty("url")
                        .GetString() ?? "";

                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }

            // =========================
            // RECHERCHE WEB
            // =========================

            else if (action == "search_web")
            {
                string engine =
                    jsonDocument.RootElement
                        .GetProperty("engine")
                        .GetString() ?? "";

                string query =
                    jsonDocument.RootElement
                        .GetProperty("query")
                        .GetString() ?? "";

                string url = "";

                if (engine == "google")
                {
                    url =
                        "https://www.google.com/search?q="
                        + Uri.EscapeDataString(query);
                }
                else if (engine == "github")
                {
                    url =
                        "https://github.com/search?q="
                        + Uri.EscapeDataString(query);
                }
                else if (engine == "youtube")
                {
                    url =
                        "https://www.youtube.com/results?search_query="
                        + Uri.EscapeDataString(query);
                }

                if (url != "")
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
            }

            // =========================
            // OUVRIR UNE APPLICATION
            // =========================

            else if (action == "open_program")
            {
                string program =
                    jsonDocument.RootElement
                        .GetProperty("program")
                        .GetString() ?? "";

                Dictionary<string, string> applications =
                    new Dictionary<string, string>
                    {
                        {
                            "vscode",
                            @"C:\Users\yahay\AppData\Local\Programs\Microsoft VS Code\Code.exe"
                        },

                        {
                            "discord",
                            @"C:\Users\yahay\AppData\Local\Discord\app-1.0.9256\Discord.exe"
                        },

                        {
                            "smath",
                            @"C:\Program Files\LibreOffice\program\smath.exe"
                        },

                        {
                            "writer",
                            @"C:\Program Files\LibreOffice\program\swriter.exe"
                        },

                        {
                            "calc",
                            @"C:\Program Files\LibreOffice\program\scalc.exe"
                        },

                        {
                            "edge",
                            @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"
                        },

                        {
                            "visualstudio",
                            @"C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\devenv.exe"
                        }
                    };

                if (applications.TryGetValue(
                    program,
                    out string? chemin))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = chemin,
                        UseShellExecute = true
                    });
                }
            }
        }
    
}
}
