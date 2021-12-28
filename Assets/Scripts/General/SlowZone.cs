using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timeFactor = 0.5f;
    private float defaultFixedDeltaTime = 0.02f;
    private float defaultTime = 1.0f;
    private bool timeToggle = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Debug.Log("Slowing Time");
            ToggleTime();
        }   
    }

    private void ToggleTime()
    {
        timeToggle = !timeToggle;
        Debug.Log("Time: " + timeToggle);
        Time.timeScale = timeToggle ? timeFactor : defaultTime;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Debug.Log("Done Slowing Time");
            ToggleTime();
        }
    }

}
