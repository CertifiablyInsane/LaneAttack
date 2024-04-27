using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float parallaxAmountX = 0f; // Distance of the item (z-index based) 
    [SerializeField] private float parallaxAmountY = 0f;

    [SerializeField] private float smoothingX = 1f; // Smoothing factor of parrallax effect
    [SerializeField] private float smoothingY = 1f;
    
    private Transform cam; // Camera reference (of its transform)

    private Vector3 _prevCamPos;
    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (parallaxAmountX != 0f)
        {
            float parallaxX = (_prevCamPos.x - cam.position.x) * parallaxAmountX;
            Vector3 backgroundTargetPosX = new Vector3(transform.position.x + parallaxX,
                                                      transform.position.y,
                                                      transform.position.z);

            // Lerp to fade between positions
            transform.position = Vector3.Lerp(transform.position, backgroundTargetPosX, smoothingX * Time.deltaTime);
        }

        if (parallaxAmountY != 0f)
        {
            float parallaxY = (_prevCamPos.y - cam.position.y) * parallaxAmountY;
            Vector3 backgroundTargetPosY = new Vector3(transform.position.x,
                                                       transform.position.y + parallaxY,
                                                       transform.position.z);

            transform.position = Vector3.Lerp(transform.position, backgroundTargetPosY, smoothingY * Time.deltaTime);
        }

        _prevCamPos = cam.position;
    }
}
