using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using Whisper.net;
using Whisper.net.Ggml;

namespace Mara.Services
{
    internal class SpeechService
    {
        private readonly string modelPath = "ggml-base.bin";
        private WaveIn? microphone;
        private WaveFileWriter? audioWriter;

        private readonly string audioPath = "recording.wav";

   
        public async Task ChargementDuModele()
        {
            if (File.Exists(modelPath))
            {
                return;
            }
            using var modeleFlux = await WhisperGgmlDownloader.Default.GetGgmlModelAsync(GgmlType.Base);
            using var fichierEcrit = File.OpenWrite(modelPath);

            await modeleFlux.CopyToAsync(fichierEcrit);
        }

        public async Task<string> Transcription (string audio) {

            using var UsineAMurmures = WhisperFactory.FromPath(modelPath);
            using var processeur = UsineAMurmures.CreateBuilder().WithLanguage("fr").Build();

            using var ficher = File.OpenRead(audio);
            string text = "";

            await foreach(var resultat in processeur.ProcessAsync(ficher))
            {
                text += resultat.Text;
            }

            return text.Trim();
        }

        public void StartRecording()
        {
            microphone = new WaveIn();

            microphone.WaveFormat = new WaveFormat(16000, 1);

            audioWriter = new WaveFileWriter(
                audioPath,
                microphone.WaveFormat
            );

            microphone.DataAvailable += (sender, e) =>
            {
                audioWriter.Write(e.Buffer, 0, e.BytesRecorded);
            };

            microphone.StartRecording();
        }
        public void StopRecording()
        {
            microphone?.StopRecording();

            microphone?.Dispose();
            microphone = null;

            audioWriter?.Dispose();
            audioWriter = null;
        }
    }
}
