using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSong : MonoBehaviour {
    public TextMeshProUGUI text;

    public void Start() {
        text.text = gameObject.name;
    }

    public void OnClick() {
        PersistantData.SelectedSong = gameObject.name;
        SceneManager.LoadSceneAsync("Scenes/TrackScene");
    }
}