using System;
using TMPro;
using UnityEngine;

public class EndScene : MonoBehaviour {
    public TextMeshProUGUI StatsText;
    
    private void Start() {
        StatsText.text = "Longest Combo: " + PersistantData.Stats.LongestCombo + "\n" +
                         "Score: " + PersistantData.Stats.Score + "\n" +
                         "Misses: " + PersistantData.Stats.Misses + "\n" +
                         "Hits: " + PersistantData.Stats.Hits + "/" + (PersistantData.Stats.Hits + PersistantData.Stats.Misses);
    }
}