using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePost : MonoBehaviour
{

    public Dropdown dropdown;
    private main script;

    private void Start() {
        dropdown = GetComponent<Dropdown>();
        script = GameObject.Find("Script").GetComponent<main>();
        if(script == null) {
            Debug.LogError("[PrePost] Failure to find the main script!");
        }
    }

    public void SetTest() {
        if (dropdown.value == 0)
            script.setTrue(main.track.Pretest);
        else /* dropdown. value == 1*/
            script.setTrue(main.track.PostTest);
    }
}
