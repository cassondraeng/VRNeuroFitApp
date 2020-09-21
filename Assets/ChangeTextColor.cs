using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    public Text test;
    public Image cross;

    private int time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var green= Color.green;
        if (time % 60 == 0) {
            if (test.fontSize == 1) {
                test.fontSize = 20;
            } else {
                test.fontSize = 1;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            test.fontSize = 1;
            test.color = green;
        }
        time++;
    }
}
