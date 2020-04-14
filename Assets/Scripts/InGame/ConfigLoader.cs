using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ConfigLoader : MonoBehaviour {
    public Config config;

    private void Awake() {
        var data = File.ReadAllText("Assets/Maps/" + PersistantData.SelectedSong + ".json");
        config = JsonConvert.DeserializeObject<Config>(data);
    }
}
