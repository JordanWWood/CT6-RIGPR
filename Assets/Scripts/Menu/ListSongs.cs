using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ListSongs : MonoBehaviour {
    public GameObject ButtonPrefab;
    public GameObject Parent;
    
    // Start is called before the first frame update
    void Start() {
        var directory = Directory.GetFiles("Assets/Maps");

        foreach (var file in directory) {
            if (file.EndsWith(".json")) {
                GameObject go = Instantiate(ButtonPrefab, Parent.transform, true) as GameObject;
                var name = file.Split('\\');
                go.name = name[1].Substring(0, name[1].Length - 5);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.SetActive(true);
            }
        }
    }
}
