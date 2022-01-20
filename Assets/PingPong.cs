using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int size = 4;
    bool isForward = true;
    int current = 0;
    void Start()
    {
        Debug.Log(5 % 4);
        InvokeRepeating("FindNextTarger", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isForward)
        {
            GetNextForward();
            Debug.Log(current);
        }
        else
        {
            GetNextBack();
            Debug.Log(current);
        }
    }
    void GetNextForward()
    {
        if(current + 1 >= size)
        {
            isForward = false;
            current = size - 2;
        }
        else
        {
            current++;
        }
    }
    void GetNextBack()
    {
        if (current - 1 < 0)
        {
            isForward = true;
            current = 1;
        }
        else
        {
            current--;
        }
    }
}
