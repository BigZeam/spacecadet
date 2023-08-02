using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbShopMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float amplitude = 2;
    [SerializeField] float speed = 1.5f;
    Vector3 posOrigin = new Vector3();
    Vector3 tempPos = new Vector3();

    void Awake()
    {
        posOrigin = transform.position;
    }
    void Update()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * amplitude;
        transform.position = tempPos;
    }
}
