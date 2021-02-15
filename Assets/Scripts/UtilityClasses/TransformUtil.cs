using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class TransformUtil
{
    #region ChildManipulation
    //A failing parent gives up their kids to the grandparent
    
    public static void DisownChildren(Transform transform)
    {
        //Debug.Assert(transform.childCount == 2);

        Transform grandparent = transform.parent;
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).parent = grandparent;
        }
    }

    //You know this function is fairly cruel if you think about it.
    public static void DisableChildren(Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //The evil parent kills all of their stupid kids in the hierarchy. Die, idiots.
    public static void KillChildren(Transform evilparent)
    {
        int killcount = evilparent.childCount;
        //Kill them one at a time. Go down your hit-list. You know what to do.
        for (int i = 0; i < killcount; i++)
        {
            GameObject.Destroy(evilparent.GetChild(i).gameObject);
        }
    }
    #endregion
}
