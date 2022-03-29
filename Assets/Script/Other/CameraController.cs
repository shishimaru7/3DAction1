using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("対象")]
    [SerializeField]
    private GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = player.transform.position + offset;
        }
    }
}