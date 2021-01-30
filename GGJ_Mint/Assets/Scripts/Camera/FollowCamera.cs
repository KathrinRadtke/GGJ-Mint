using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minPos;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxPos;
    [SerializeField] private float maxRotation;

    // Update is called once per frame
    void Update()
    {
        float pos = target.position.x;
        float t = (pos - minPos) / (maxPos - minPos);
        float rotation = Mathf.Lerp(minRotation, maxRotation, t);
        Camera main = Camera.main;
        main.transform.rotation = Quaternion.Euler(35, rotation, 0);
    }
}
