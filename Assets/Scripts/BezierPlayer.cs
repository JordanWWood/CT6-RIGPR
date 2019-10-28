using BansheeGz.BGSpline.Components;
using UnityEngine;

public class BezierPlayer : MonoBehaviour {
    public Vector2 offset;
    public float duration = 10;
    public GameObject playerObject;
    public GameObject trackerObject;

    public Vector3 trackerStartPos;
    public Vector3 trackerPosDiff;

    private BGCcMath curve;
    private float time;
    private float distance;
    
    void Start() {
        curve = GameObject.FindWithTag("TrackCurve").GetComponent<BGCcMath>();
    }

    private Vector3 lastPoint;
    private bool set = false;
    private void Update() {
        if (time < 2 && !set) {
            time += Time.deltaTime;
            return;
        } else if (time >= 2 && !set) {
            trackerStartPos = trackerObject.transform.localPosition;
            set = true;
        }

        trackerPosDiff = trackerStartPos - trackerObject.transform.localPosition;
        offset += new Vector2((-trackerPosDiff.x) / 100, 0);
        
        distance += Time.deltaTime / duration;

        transform.position = curve.CalcPositionAndTangentByDistanceRatio(distance, out var tangent);
        transform.rotation = Quaternion.LookRotation(tangent);

        playerObject.transform.localPosition += new Vector3(offset.x, offset.y);
    }
}