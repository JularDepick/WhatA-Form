using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject referObj;
    private Vector3 pos;
    void Start()
    {
        referObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
