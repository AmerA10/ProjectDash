using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlow : MonoBehaviour
{
    [SerializeField] private float timeFactor = 0.5f;
    private float defaultFixedDeltaTime = 0.02f;
    private float defaultTime = 1.0f;
    private bool isInSlowMo = false;
    [SerializeField] private bool TimeToggle = true;
    private Vector2 dashDirection;

    private void OnEnable()
    {
        GetComponent<PlayerController>().TimeAction += TurnTime;
        Debug.Log("Enabling Time");
    }
    private void OnDisable()
    {
        GetComponent<PlayerController>().TimeAction -= TurnTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnTime(bool isSlow)
    {
        if (TimeToggle)
        {
            if (isInSlowMo == isSlow) return;
            else
            {
                isInSlowMo = isSlow;
            }
            Debug.Log("Time: " + isSlow);
            Time.timeScale = isSlow ? timeFactor : defaultTime;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }

    }
}
