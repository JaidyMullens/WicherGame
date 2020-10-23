using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{

    public int ammoAmount = 5;

    void Update()
    {
        Debug.Log( "Amount of ammo: " + ammoAmount);
        checkAmmo();
    }

    public bool checkAmmo()
    {
        bool containsAmmo = true;

        if (ammoAmount == 0)
        {
            containsAmmo = false;
        }
        else
        {
            containsAmmo = true;
        }

        return containsAmmo;
    }

    public void subtractAmmo()
    {
        if (ammoAmount > 0)
        {
            ammoAmount--;
        }

    }
}
