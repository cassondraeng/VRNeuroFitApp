using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//not main anymore; it's just gonna be a note-taking page for me I guess..

/* 5 blocks, each with 32 congruent & 16 incongruent trials
 * cross hair appears(1000ms), word appears (200ms), max response window(800ms)

 * color name "(string)" : color hex
 * put each color in an array: {("red",FF0000), ("blue", 0000FF), ("green",00FF00)}
 * random number generate (0-2) --> for CON-TEXT & CON-COLOR (32)
 * random number generate (0-2) --> INCON-TEXT (16), another time for INCON-COLOR
 * INCON-COLOR != TEXT
 * 
 * x = RAND();
do {
    y = RAND();
} while( y != x)

*/

public class StroopyDoop:MonoBehaviour {
	// Use this for initialization
    //for now, doing just one block
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	}
}
