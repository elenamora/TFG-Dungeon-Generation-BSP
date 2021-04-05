using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    private Transform dungeonTransform;

    private float xMin, xMax, yMin, yMax;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;  
    }
    
    void LateUpdate()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 temp = transform.position;

        dungeonTransform = GameObject.FindGameObjectWithTag("Bounds").transform;
        Limits(dungeonTransform);

        temp.x = Mathf.Clamp(playerTransform.position.x, xMin, xMax);
        temp.y = Mathf.Clamp(playerTransform.position.y, yMin, yMax);

        transform.position = temp;

    }

    public void Limits(Transform dungeonTransform)
    {
        xMin = -0.5f + cam.orthographicSize * cam.aspect;
        xMax = dungeonTransform.localScale.x - cam.orthographicSize * cam.aspect + 0.5f;

        yMin = -0.5f + cam.orthographicSize;
        yMax = dungeonTransform.localScale.y - cam.orthographicSize - 0.5f;
    }
}
