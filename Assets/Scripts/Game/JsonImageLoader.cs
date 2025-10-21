using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class JsonImageLoader
{
    public async Task<Sprite> LoadImageFromUrlAsync(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                return null;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            return Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        }
    }
}