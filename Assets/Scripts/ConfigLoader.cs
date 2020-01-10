using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ConfigLoader : MonoBehaviour {
    public Config config;
    
    // Start is called before the first frame update
    void Start() {
        config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Assets/Maps/test.json"));
        Debug.Log("Break");
    }
}
