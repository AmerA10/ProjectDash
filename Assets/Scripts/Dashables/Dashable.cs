using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dashable : MonoBehaviour
{
    //This class can take care of any of the more global level components and such, Example if the player needs the collider or something it works through here
    //Perhaps dashables can have activation cool down time which can be done through here
    //What this means now though is that dash behaviours can be done on the fly and at run time.

    public IDashable dashType;
    public IDashable defaultDash;


    public void TryDash(Transform player, Vector2 dashDirdirection)
    {
        dashType?.HandleDash( player,  dashDirdirection);
     

    }
    public void TryDefaultDash(Transform player, Vector2 dashDirdirection)
    {
        Debug.Log("Doing default");
        defaultDash?.HandleDash(player, dashDirdirection);
    }





}
