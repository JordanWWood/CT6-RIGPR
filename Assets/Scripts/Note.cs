using System;
using UnityEngine;

public class Note : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Main Bike Body") {
            StaticEvents.NoteHitEvent.Invoke();
        }
    }
}