using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mara.Services
{
    internal class OllamaService
    {

        private readonly HttpClient client = new HttpClient();


        public async Task<string> PoserQuestion(string question)
        {
            //client.Timeout = TimeSpan.FromMinutes(5);
            var requete = new
            {
                model = "mara",
                prompt = question,
                stream = false
            };

            string json = JsonSerializer.Serialize(requete);

            using var contenu = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage reponse = await client.PostAsync(
                "http://localhost:11434/api/generate",
                contenu
            );

            reponse.EnsureSuccessStatusCode();

            string resultat = await reponse.Content.ReadAsStringAsync();

            using JsonDocument document = JsonDocument.Parse(resultat);

            string texte = document.RootElement
                .GetProperty("response")
                .GetString() ?? "";

            return texte;
        }
    }
}

