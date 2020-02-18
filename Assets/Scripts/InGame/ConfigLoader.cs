using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ConfigLoader : MonoBehaviour {
    public Config config;

    private void Awake() {
        config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Assets/Maps/" + PersistantData.SelectedSong + ".json"));
    }
}
