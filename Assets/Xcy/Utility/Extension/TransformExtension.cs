using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Xcy.Utility.Extension
{

    public static class TransformExtension
    {

        public static void Identity(this Transform transform)
        {
            transform.localPosition=Vector3.zero;
            transform.localRotation=Quaternion.identity;
            transform.localScale=Vector3.zero;
        }
        
        public static void SetLocalPosX(this Transform transform,float x)
        {
            var localPosition = transform.localPosition;
            localPosition.x = x;
            transform.localPosition = localPosition;
        }
        public static void SetLocalPosY(this Transform transform,float y)
        {
            var localPosition = transform.localPosition;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }
        public static void SetLocalPosZ(this Transform transform,float z)
        {
            var localPosition = transform.localPosition;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }
        public static void SetLocalPosXY(this Transform transform,float x,float y)
        {
            var localPosition = transform.localPosition;
            localPosition.x = x;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }
        public static void SetLocalPosXZ(this Transform transform,float x,float z)
        {
            var localPosition = transform.localPosition;
            localPosition.x = x;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }
        public static void SetLocalPosYZ(this Transform transform,float y,float z)
        {
            var localPosition = transform.localPosition;
            localPosition.y = y;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }
        public static void AddChild(this Transform parentTrans, Transform childTrans)
        {
            childTrans.SetParent(parentTrans);
        }
        
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
}