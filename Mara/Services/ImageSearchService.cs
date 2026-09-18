using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Mara.Services
{
    internal class ImageSearchService
    {

        private readonly string apiKey = Environment.GetEnvironmentVariable("PEXELS_API_KEY") ?? "";


        public async Task<string> RechercherImage(string query)
        {
            MessageBox.Show(
    $"Clé trouvée : {!string.IsNullOrEmpty(apiKey)}"
);
            using HttpClient client = new HttpClient();

            string url =
                "https://api.pexels.com/v1/search?query="
                + Uri.EscapeDataString(query)
                + "&per_page=1";

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", apiKey);

            HttpResponseMessage reponse =
                await client.GetAsync(url);

            reponse.EnsureSuccessStatusCode();

            string json =
                await reponse.Content.ReadAsStringAsync();

            using JsonDocument document =
                JsonDocument.Parse(json);

            JsonElement photos =
                document.RootElement.GetProperty("photos");

            if (photos.GetArrayLength() == 0)
            {
                throw new Exception("Aucune image trouvée.");
            }

            string imageUrl =
                photos[0]
                    .GetProperty("src")
                    .GetProperty("large")
                    .GetString() ?? "";

            return imageUrl;
        }
    }
}

