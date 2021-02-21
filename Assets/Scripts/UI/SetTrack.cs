using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrack : MonoBehaviour
{

    [SerializeField] private main.track change;

    private main script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("Script").GetComponent<main>();
    }

    public void DoIt() {
        script.setTrue(change);
    }
}
