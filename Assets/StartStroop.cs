using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStroop : MonoBehaviour
{
    public main magicMain;
    // Start is called before the first frame update
    void Start()
    {
        GameObject WEEEE = GameObject.Find("Script");
        magicMain = GetComponent<main>();
        magicMain.start_test;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
