using System;
using UnityEngine;

public class BackNote : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("NoteCollision")) return;
        if (gameObject.name == "Destroyed") return;
        
        Debug.Log("Note missed");
        
        StaticEvents.NoteMissEvent.Invoke();
    }
}