﻿using System;
using BansheeGz.BGSpline.Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BezierPlayer : MonoBehaviour {
    [Header("Configuration Attributes")]
    public ConfigLoader configLoader;
    
    [Header("Player Information")]
    public Vector2 offset;
    public GameObject playerObject;
    public GameObject trackerObject;

    [Header("Track Settings")]
    public Vector3 trackerStartPos;
    public Vector3 trackerPosDiff;
    public AudioSource AudioSource;

    [Header("User Interface")]
    public TextMeshProUGUI ScoreUI;
    public TextMeshProUGUI ComboUI;
    public TextMeshProUGUI MultiplierUI;
    public TextMeshProUGUI MissesUI;
    
    [Header("Debug")]
    public uint Score = 0;
    public uint Streak = 0;
    public uint Multiplier = 1;
    public uint Misses = 0;

    public Slider Slider;
    public TextMeshProUGUI SliderText;
    
    private BGCcMath curve;
    private float time;
    private float distance;
    private float duration = 10;
    
    private bool playing = false;
    private Config config;
    private bool started = false;

    void Start() {
        curve = GameObject.FindWithTag("TrackCurve").GetComponent<BGCcMath>();
        
        StaticEvents.NoteHitEvent.AddListener(OnNoteHit);
        StaticEvents.NoteMissEvent.AddListener(OnNoteMiss);
        
        config = configLoader.config;
        Debug.Log("AudioClip is " + "Music/" + PersistantData.SelectedSong);
        var audioClip = Resources.Load<AudioClip>("Music/" + PersistantData.SelectedSong);
        if (audioClip == null) {
            Debug.LogError("Failed to load AudioClip");
            SceneManager.LoadSceneAsync("Scenes/MenuScene");
        }

        AudioSource.clip = audioClip;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private Vector3 lastPoint;
    private bool set = false;
    private void Update() {
        if (config != null) duration = (config.duration.minutes * 60) + config.duration.seconds;

        if (time < 2 && !set) {
            time += Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Escape! Returning to Menu");
            SceneManager.LoadSceneAsync("Scenes/MenuScene");
        }
        
        if (time >= 2 && !set) {
            trackerStartPos = trackerObject.transform.localPosition;
            set = true;
        }

        offset.x += Input.GetAxis("Mouse X") / 5;

        trackerPosDiff = trackerStartPos - trackerObject.transform.localPosition;
        offset += new Vector2((-trackerPosDiff.x) / 100, 0);
        if (offset.x > .5f) offset.x = .5f;
        else if (offset.x < -.5f) offset.x = -.5f;

        distance += Time.deltaTime / duration;

        transform.position = curve.CalcPositionAndTangentByDistanceRatio(distance, out var tangent);
        transform.rotation = Quaternion.LookRotation(tangent);

        playerObject.transform.localPosition = new Vector3(offset.x, offset.y + 0.5f);

        if (started == false) {
            var delay = (1f / config.beatsPerMinute) * 5f;
            AudioSource.PlayDelayed(delay);
            started = true;
        }
        
        if (distance >= 1f) {
            PersistantData.Stats.Score = Score;
            SceneManager.LoadSceneAsync("Scenes/EndScene");
        }
    }

    void OnNoteHit() {
        Multiplier = (uint) Mathf.FloorToInt(f: Streak / 15f) + 1;
        if (Multiplier > 4) Multiplier = 4;

        Score += Multiplier;
        Streak++;
        PersistantData.Stats.Hits++;

        UpdateUI();
    }

    void OnNoteMiss() {
        if (Streak > PersistantData.Stats.LongestCombo)
            PersistantData.Stats.LongestCombo = Streak;
        
        Streak = 0;
        Misses++;
        PersistantData.Stats.Misses++;
        
        UpdateUI();
    }

    void UpdateUI() {
        ScoreUI.text = "Score: " + Score;
        ComboUI.text = "Combo: " + Streak;
        MultiplierUI.text = "Multi: " + Multiplier;
        MissesUI.text = "Misses: " + Misses;
    }
}