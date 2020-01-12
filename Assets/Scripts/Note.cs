using System;
using System.Security.Cryptography;
using UnityEngine;

public class Note : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Note Collision Enter");
        if (!other.gameObject.CompareTag("NoteCollision")) return;
        
        StaticEvents.NoteHitEvent.Invoke();
        Destroy(gameObject);
    }
}