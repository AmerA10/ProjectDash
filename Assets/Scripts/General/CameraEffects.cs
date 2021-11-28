using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    Camera main;
    [SerializeField] float cameraSize = 16.45f;
    public float camSpeed = 0.005f;
    public float camDistance = 30f;


    private void Awake()
    {
        if (!main) main = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        EventManager.OnDash += DashEffect;
    }

    private void OnDisable()
    {
        EventManager.OnDash -= DashEffect;
    }

    private void Update()
    {
        ReturnToDefault();
    }

    void ReturnToDefault()
    {
        if (main.orthographicSize != cameraSize)
        {
            if(Mathf.Abs(main.orthographicSize - cameraSize) <= camSpeed)
            {
                main.orthographicSize = cameraSize;
            } else
            {
                float newSize = main.orthographicSize > cameraSize ? main.orthographicSize - camSpeed : main.orthographicSize + camSpeed;
                main.orthographicSize = newSize;
            }

        }
    }

    /*
     *  Various camera effects triggered on events. 
     */
    void DashEffect()
    {
        float change = camDistance / 100f;
        main.orthographicSize = main.orthographicSize - change;
    }
}
