using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DashEnums;

public class EventManager : MonoBehaviour
{
    public delegate void E_Dash();
    public static event E_Dash OnDash;

    public delegate void E_PreDash();
    public static event E_PreDash OnPreDash;

    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            return;
        }
        Destroy(this.gameObject);
    }


    public void TriggerCameraEffect(CameraEffect effect)
    {
        switch (effect)
        {
            case CameraEffect.PRE_DASH:
                OnPreDash?.Invoke();
                break;
            case CameraEffect.DASH:
                OnDash?.Invoke();
                break;
            default:
                break;
        }
    }
}
