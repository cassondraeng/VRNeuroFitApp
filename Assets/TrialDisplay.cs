using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialDisplay : MonoBehaviour
{
    public int trialCount;

    // Start is called before the first frame update
    void Start()
    {
        trialCount = 0;
    }

    public void IncrementTrial()
    {
        trialCount++;
    }
}
