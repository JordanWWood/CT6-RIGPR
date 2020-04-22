using System;
using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Curve;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrackManager : MonoBehaviour {
    [Header("Track Settings")]
    public GameObject track;
    public GameObject note;
    public ConfigLoader configLoader;
    public int speedMultiplier = 6;

    [Header("Building Settings")]
    public List<GameObject> buildingPrefabs;

    void Start() {
        // Load the configuration from the config manager which has already loaded the correct config for the chosen song
        Config config = configLoader.config;

        var z = 0f;

        // Calculate the number of segments required for the length of the song pulled from the config loader
        var segmentsMinutes = (config.duration.minutes * (config.beatsPerMinute / 6));
        var segmentsSeconds = ((float) config.duration.seconds / 60) * (config.beatsPerMinute / 6);
        var numberOfSegments = (((segmentsMinutes + (int) segmentsSeconds) + 1) * speedMultiplier) + 5;

        var trackGO = Instantiate(new GameObject(), transform);
        trackGO.name = "Track";
        List<Vector3> segments = new List<Vector3>();
        // Place track segments and points of the curve iteratively for the number of times previously calculated
        for (int i = 0; i < numberOfSegments; i++) {
            var item = Instantiate(track, trackGO.transform);

            // TODO replace with logic to place on curve so it isnt a perfectly straight tack
            if (i != 0) z += 1.8044f;
            item.transform.position = new Vector3(0, 0, z);

            var curveObject = GameObject.FindWithTag("TrackCurve");
            var curve = curveObject.GetComponent<BGCurve>();
            curve.AddPoint(new BGCurvePoint(curve, new Vector3(0, 0, z), true));
            segments.Add(new Vector3(0,0, z));
        }
        
        // Start placing notes in the fifth track segment
        z = 1.8044f * 5;
        
        var notesGO = Instantiate(new GameObject(), transform);
        notesGO.name = "Notes";
        // Generate notes based on what is read from the configuration for the given song.
        for (int i = 0; i < config.map.Length; i++) {
            // TODO replace with logic to place on curve so it isnt a perfectly straight tack
            if (i != 0) z += (0.30073f * config.beatsPerRow) * speedMultiplier;

            // Each lane is precisely 0.245 units wide. 
            var x = -1f;
            if (config.map[i][0]) x = 0.245f + 0.245f;
            if (config.map[i][1]) x = 0.245f;
            if (config.map[i][2]) x = 0f;
            if (config.map[i][3]) x = -0.245f;
            if (config.map[i][4]) x = -0.245f + -0.245f;
            if (x == -1f) continue;
            
            var item = Instantiate(note, notesGO.transform);
            item.tag = "NoteCollision";
            item.transform.position = new Vector3(x, .26f, z);
        }

        var buildingGO = Instantiate(new GameObject(), transform);
        buildingGO.name = "Buildings";
        foreach (var segment in segments) {
            var r = Random.Range(0, 10);

            for (int i = 0; i < r; i++) {
                var x = rollSafeLocation();
                
                var buildingIndex = Random.Range(0, buildingPrefabs.Count);
                var item = Instantiate(buildingPrefabs[buildingIndex], buildingGO.transform);
                
                var rotation = new Vector3(item.transform.rotation.eulerAngles.x, 0, Random.Range(0.0f, 360.0f));
                item.transform.rotation = Quaternion.Euler(rotation);
                item.transform.position = new Vector3(x, 0, segment.z);
            }
        }
    }

    private void Update() {
        
    }

    float rollSafeLocation() {
        var r = Random.Range(-50.0f, 50.0f);
        if (r < 4.0f && r > -4.0f) return rollSafeLocation();

        return r;
    }
}