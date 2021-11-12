using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public float health;
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Took damage, remaining health is: " + health);
    }
}
