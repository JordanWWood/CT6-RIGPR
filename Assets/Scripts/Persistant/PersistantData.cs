using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour {
    public static string SelectedSong = "None";
    
    private void Awake() {
        DontDestroyOnLoad(this);
    }
}
