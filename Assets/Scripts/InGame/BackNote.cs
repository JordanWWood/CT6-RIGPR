using System;
using UnityEngine;

public class BackNote : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("NoteCollision")) return;
        Debug.Log("Note missed");
        
        StaticEvents.NoteMissEvent.Invoke();
    }
}