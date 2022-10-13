using System;
using UnityEngine.Networking;

namespace Models
{
    public class CardModel
    {
        public event Action<byte[]> ImageAnswer;
        private string url = "https://picsum.photos/200";
        
        public void DownloadBytes()
        {
            var www = new UnityWebRequest(url) {downloadHandler = new DownloadHandlerBuffer()};
            var webRequest = www.SendWebRequest();
            webRequest.completed += operation =>
            {
                ImageAnswer?.Invoke(www.downloadHandler.data);
                www.Dispose();
            };
        }
    }
}