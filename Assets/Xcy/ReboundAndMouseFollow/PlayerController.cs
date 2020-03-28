using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xcy.Sample
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _walkSpeed;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Debug.Log(Screen.height + " height");
            Debug.Log(Screen.width + " width");
        }

        private void Update()
        {
            //Debug.Log(1);
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input
            .mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 tempDir = new Vector3(hit.point.x, hit.point.y,
                                      transform.position.z) -
                                  transform.position;
                Quaternion rotation = Quaternion.FromToRotation(
                    transform.up, tempDir);
                transform.rotation = transform.rotation * rotation;
            }

        }
    }
}
