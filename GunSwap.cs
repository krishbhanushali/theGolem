using UnityEngine;
using System.Collections;

public class GunSwap : MonoBehaviour {
    public GameObject currentWeapon;
    public GameObject[] weaponsArray = new GameObject[2];
    public int currentWeaponIndex = 0;

	// Use this for initialization
	void Start () {
        selectWeapon(currentWeaponIndex);
    }
	
    public int getCurrentWeaponIndex()
    {
        return currentWeaponIndex;
    }

    public GameObject getCurrentWeapon(int weaponIndex)
    {
        for (int i = 0; i < weaponsArray.Length; i++)
        {
            if (i == weaponIndex)
            {
                return transform.GetChild(i).gameObject;
            }
        }
        return null;
    }
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(1))
        {
            
            currentWeaponIndex++;
            if(currentWeaponIndex>=weaponsArray.Length)
            {
                currentWeaponIndex = 0;
            }
            selectWeapon(currentWeaponIndex);
        }
	}

    void selectWeapon(int weaponIndex)
    {
        for(int i=0;i<weaponsArray.Length;i++)
        {
            if(i==weaponIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    
}
