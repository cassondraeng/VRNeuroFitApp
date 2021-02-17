using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "boolval")]
public class boolVal : ScriptableObject
{
    public bool val;

    public void swapVal() {
        val = !val;
    }

    public void setTrue() {
        val = true;
    }
}
