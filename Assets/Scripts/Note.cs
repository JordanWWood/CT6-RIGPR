using System;
using System.Security.Cryptography;
using UnityEngine;

public class Note : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collision Enter");
        if (other.gameObject.name == "Main Bike Body") {
            StaticEvents.NoteHitEvent.Invoke();
            Destroy(gameObject);
        }
    }
}