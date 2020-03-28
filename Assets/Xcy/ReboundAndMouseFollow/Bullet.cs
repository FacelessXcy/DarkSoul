using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Xcy.Sample
{
    
    public class Bullet : MonoBehaviour
    {
        private int _colCount = 0;
        private Vector2 _lastFrame;
        private float _lifeDistance = 10;
        private Rigidbody2D _rigidbody2D;
        private float _oldVelMag;
        private Vector2 _oldVelDir;
        private Vector2 _oldVel;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = transform.TransformDirection(new
                                        Vector2(1, 2).normalized) *
                                    Time.deltaTime * 60.0f;
            _oldVel = _rigidbody2D.velocity;
            _oldVelDir = _oldVel.normalized;
            _oldVelMag = _oldVel.magnitude;
            Quaternion rotation = Quaternion.FromToRotation(
                transform.up, _oldVelDir);
            transform.rotation = transform.rotation * rotation;
            //transform.up = _rigidbody2D.velocity;
            _lastFrame = transform.position;
            Debug.Log(transform.rotation);
        }


        private void FixedUpdate()
        {
            _lifeDistance -=
                Vector2.Distance(transform.position, _lastFrame);
            if (_lifeDistance <= 0)
            {
                Destroy(gameObject);
            }

            _lastFrame = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Wall"))
            {
                _colCount++;
                if (_colCount >= 3)
                {
                    Destroy(gameObject);
                }

                Vector2 newDir = Vector2.Reflect(_oldVelDir,
                    other.GetContact(0).normal);
                //Debug.Log(newDir+"  newDir");
                Vector2 curDir = _oldVelDir;
                Quaternion rotation = Quaternion.FromToRotation(
                    curDir, newDir);
                transform.rotation = transform.rotation * rotation;
                _rigidbody2D.velocity = newDir.normalized * _oldVelMag;
                _oldVel = _rigidbody2D.velocity;
                _oldVelDir = _oldVel.normalized;
                _oldVelMag = _oldVel.magnitude;

            }
        }

    }
}
