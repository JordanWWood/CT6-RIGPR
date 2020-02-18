using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSong : MonoBehaviour {
    public void OnClick(string songName) {
        PersistantData.SelectedSong = songName;
        SceneManager.LoadSceneAsync("Scenes/TrackScene");
    }
}