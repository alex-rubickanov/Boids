using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Attractor : MonoBehaviour
{
    public static Vector3 Pos = Vector3.zero;

    [SerializeField] private float radius = 10.0f;
    [SerializeField] private float xPhase = 0.5f;
    [SerializeField] private float yPhase = 0.4f;
    [SerializeField] private float zPhase = 0.1f;
    

    private void Update()
    {
        Vector3 tPos = Vector3.zero;
        Vector3 scale = transform.localScale;
        tPos.x = Mathf.Sin(xPhase * Time.time) * radius * scale.x;
        tPos.y = Mathf.Sin(yPhase * Time.time) * radius * scale.y;
        tPos.z = Mathf.Sin(zPhase * Time.time) * radius * scale.z;

        transform.position = tPos;
        Pos = tPos;
    }
}
