using System;
using System.Security.Cryptography;
using UnityEngine;

public class Note : MonoBehaviour {
    private void Start() {
        Debug.Log("Note instantiated");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Note Trigger Enter");
        if (!other.gameObject.CompareTag("NoteCollision") || gameObject.CompareTag("BackNote")) return;
        
        StaticEvents.NoteHitEvent.Invoke();
        Destroy(gameObject);
    }
}