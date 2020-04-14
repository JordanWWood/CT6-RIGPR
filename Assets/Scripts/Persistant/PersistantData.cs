using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour {
    public static string SelectedSong = "None";
    public static Stats Stats = new Stats();

    private void Awake() {
        DontDestroyOnLoad(this);
    }
}
