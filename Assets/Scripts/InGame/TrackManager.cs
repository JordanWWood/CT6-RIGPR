using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Curve;
using UnityEngine;

public class TrackManager : MonoBehaviour {
    public GameObject track;
    public GameObject note;
    public ConfigLoader configLoader;

    public int speedMultiplier = 6;

    void Start() {
        // Load the configuration from the config manager which has already loaded the correct config for the chosen song
        Config config = configLoader.config;

        var z = 0f;

        // Calculate the number of segments required for the length of the song pulled from the config loader
        var segmentsMinutes = (config.duration.minutes * (config.beatsPerMinute / 6));
        var segmentsSeconds = ((float) config.duration.seconds / 60) * (config.beatsPerMinute / 6);
        var numberOfSegments = (((segmentsMinutes + (int) segmentsSeconds) + 1) * speedMultiplier) + 5;

        // Place track segments and points of the curve iteratively for the number of times previously calculated
        for (int i = 0; i < numberOfSegments; i++) {
            var item = Instantiate(track, transform);

            // TODO replace with logic to place on curve so it isnt a perfectly straight tack
            if (i != 0) z += 1.8044f;
            item.transform.position = new Vector3(0, 0, z);

            var curveObject = GameObject.FindWithTag("TrackCurve");
            var curve = curveObject.GetComponent<BGCurve>();
            curve.AddPoint(new BGCurvePoint(curve, new Vector3(0, 0, z), true));
        }
        
        // Start placing notes in the fifth track segment
        z = 1.8044f * 5;
        
        // Generate notes based on what is read from the configuration for the given song.
        for (int i = 0; i < config.map.Length; i++) {
            if (i != 0) z += (0.30073f * config.beatsPerRow) * speedMultiplier;

            // Each lane is precisely 0.245 units wide. 
            var x = -1f;
            if (config.map[i][0]) x = 0.245f + 0.245f;
            if (config.map[i][1]) x = 0.245f;
            if (config.map[i][2]) x = 0f;
            if (config.map[i][3]) x = -0.245f;
            if (config.map[i][4]) x = -0.245f + -0.245f;
            if (x == -1f) continue;
            
            var item = Instantiate(note, transform);
            item.tag = "NoteCollision";
            item.transform.position = new Vector3(x, .26f, z);
        }
    }
}