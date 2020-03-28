using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class TransformHelpers
{
    public static Transform DeepFind(this Transform parent,string targetName)
    {
        Transform temp=null;
        foreach (Transform child in parent)
        {
            if (child.name==targetName)
            {
                
                return child;
            }
            temp = DeepFind(child, targetName);
            if (temp != null) return temp;
        }

        return temp;
    }
}
