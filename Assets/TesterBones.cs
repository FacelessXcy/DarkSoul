using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TesterBones : MonoBehaviour
{
    public SkinnedMeshRenderer srcMeshRenderer;
    public List<SkinnedMeshRenderer>  tgtMeshRenderers;

    private void Start()
    {
        foreach (SkinnedMeshRenderer tgtMeshRenderer in tgtMeshRenderers)
        {
            tgtMeshRenderer.bones = srcMeshRenderer.bones;
        }
        
        
    }
}
