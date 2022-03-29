using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public float sunmove;
    void Update()
    {
        transform.Rotate(new Vector3(0, sunmove, 0) * Time.deltaTime);
    }
}