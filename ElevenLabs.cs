using System.Net.Http.Headers;
using System.Text.Json;

public class ElevenLabs {
    public async static Task TextToSpeech(string filepath, string text) {
        StringContent content = new StringContent(JsonSerializer.Serialize(
            new {
                text = text,
                voice_settings = new {
                    stability = 0.5,
                    similarity_boost = 0.5
                }
            }
        ));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.elevenlabs.io/v1/text-to-speech/GCvLkBgE5VJHCSx2LgC8");
        request.Headers.Add("Accept", "audio/mp3");
        request.Headers.Add("xi-api-key", "615a975578611aac7147e59acf6ef969");
        request.Content = content;

        HttpClient client = new HttpClient();
        var resp = await client.SendAsync(request);
        using (var fstream = new FileStream(filepath, FileMode.Append, FileAccess.Write)) {
            resp.Content.ReadAsStream().CopyTo(fstream);
        }
    }
}