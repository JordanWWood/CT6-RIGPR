using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Curve;
using UnityEngine;

public class CreateTrack : MonoBehaviour {
    public GameObject track;
    public GameObject note;
    public ConfigLoader ConfigLoader;
    
    // Start is called before the first frame update
    void Start() {
        var z = 0f;
        for (int i = 0; i < 100; i++) {
            GameObject item = Instantiate(track, transform);
            
            if (i != 0) z += 1.8044f;
            item.transform.position = new Vector3(0, 0, z);
            
            GameObject curveObject = GameObject.FindWithTag("TrackCurve");
            BGCurve curve = curveObject.GetComponent<BGCurve>();
            curve.AddPoint(new BGCurvePoint(curve, new Vector3(0, 0, z), true));
        }

        Config config = ConfigLoader.config;
        z = 1.8044f;
        for (int i = 0; i < config.map.Length; i++) {
            if (i != 0) z += 0.30073f * config.beatsPerRow;

            var x = -1f;
            if (config.map[i][0]) x = 0.245f + 0.245f;
            if (config.map[i][1]) x = 0.245f;
            if (config.map[i][2]) x = 0f;
            if (config.map[i][3]) x = -0.245f;
            if (config.map[i][4]) x = -0.245f + -0.245f;
            if (x == -1f) continue;
            
            GameObject item = Instantiate(note, transform);
            item.transform.position = new Vector3(x, .26f, z);
        }
    }
}
