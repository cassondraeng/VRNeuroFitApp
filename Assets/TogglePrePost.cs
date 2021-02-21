using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePrePost : MonoBehaviour
{

    private main script;
    [SerializeField] private bool Pre, Post;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("Script").GetComponent<main>();
        if (Pre)
            gameObject.SetActive(script.banana[(int)main.track.Pretest]);
        if (Post)
            gameObject.SetActive(script.banana[(int)main.track.PostTest]);
    }

    private void Update() {
        if (!gameObject.activeSelf) {
            if(Pre)
                gameObject.SetActive(script.banana[(int)main.track.Pretest]);
            if(Post)
                gameObject.SetActive(script.banana[(int)main.track.PostTest]);
        }
    }
}
