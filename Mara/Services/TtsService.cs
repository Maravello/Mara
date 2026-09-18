using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Text;

namespace Mara.Services
{
    internal class TtsService
    {
        private readonly string cheminDePiper = @"C:\piper\piper.exe";
        private readonly string cheminDuModele = @"C:\piper\voices\fr_FR-siwis-medium.onnx";
        private readonly string cheminAudio = @"C:\piper\voices\mara.wav";


        public async Task Parler(string texte)
        {
            ProcessStartInfo information = new ProcessStartInfo
            {
                FileName = this.cheminDePiper,
                Arguments = $"--model \"{cheminDuModele}\" --output_file \"{cheminAudio}\"",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process processus = new Process();
            processus.StartInfo = information;

            processus.Start();
            await processus.StandardInput.WriteLineAsync(texte);
            processus.StandardInput.Close();

            await processus.WaitForExitAsync();

            using SoundPlayer lecteur  = new SoundPlayer(cheminAudio);
            lecteur.Play();
        }

    }
}
