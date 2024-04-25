using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float distanceLeftUntilFollow = 2.0f;
    [SerializeField] private float distanceRightUntilFollow = 3.0f;

    private void Update()
    {
        float distance = followTarget.position.x - transform.position.x;
        if(distance < -distanceLeftUntilFollow)
        {
            transform.position = new(followTarget.position.x + distanceLeftUntilFollow, transform.position.y, transform.position.z);
        }else if(distance > distanceRightUntilFollow) 
        {
            transform.position = new(followTarget.position.x - distanceRightUntilFollow, transform.position.y, transform.position.z);
        }
    }
}
