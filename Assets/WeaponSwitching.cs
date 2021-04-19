using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        #region Switch with scrollwheel

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //scroll wheel goes up = +1f 
        {
            if (selectedWeapon >= transform.childCount - 1) //can't be -1
                selectedWeapon = 0;                         //make it 0, and get first weapon
            else
            selectedWeapon++;                               //switch weapon next
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //scroll wheel goes down = -1f 
        {
            if (selectedWeapon <= 0) 
                selectedWeapon = transform.childCount -1;  
            else
                selectedWeapon--;   //switch to previous weapon
        }

        #endregion

        #region Switch with keyboard numbers

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        #endregion

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
