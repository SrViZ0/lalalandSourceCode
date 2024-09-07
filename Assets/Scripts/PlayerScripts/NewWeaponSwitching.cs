using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewWeaponSwitching : MonoBehaviour
{
    //public TppCharacterController tppCharacterController;
    public static int selectedWeapon = 0;
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckWeapon();
        //int previousSelectedWeapon = selectedWeapon;
        //if (!uiManager.isPausing)
        //{
        //    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        //    {
        //        if (selectedWeapon >= transform.childCount - 1)
        //        {
        //            selectedWeapon = 0;
        //        }
        //        else
        //        {
        //            selectedWeapon++;
        //        }
        //    }
        //    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        //    {
        //        if (selectedWeapon <= 0)
        //        {
        //            selectedWeapon = transform.childCount - 1;
        //        }
        //        else
        //        {
        //            selectedWeapon--;
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        selectedWeapon = 0;
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        selectedWeapon = 1;
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha3))
        //    {
        //        selectedWeapon = 2;
        //    }
        //    if (previousSelectedWeapon != selectedWeapon)
        //    {
        //        SelectWeapon();
        //    }
        //}

        if (!uiManager.isPausing)
        {
            GetInput(CalculateIndex().Item1, CalculateIndex().Item2);
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
    void CheckWeapon()
    {
        if (selectedWeapon == 2)
        {
            //tppCharacterController.fallingVelocity = 10f;
        }
    }



    private GameObject GetActiveChild()
    {
        GameObject temp = null;
        foreach (Transform item in this.transform)
        {
            if (item.gameObject.activeInHierarchy == true)
            {
                temp = item.gameObject;
            }
        }
        return temp;
    }

    private int GetActiveChildIndex()
    {
        int activeChildIndex = 0;
        GameObject activeChild = GetActiveChild();
        if (activeChild != null) { activeChildIndex = activeChild.transform.GetSiblingIndex(); }
        return activeChildIndex;
    }

    private (int, int) CalculateIndex()
    {
        int index = GetActiveChildIndex();
        int prevIndx = index - 1;
        int nextIndx = index + 1;

        if (prevIndx < 0)
        {
            prevIndx = this.transform.childCount - 1;
        }
        if (nextIndx > this.transform.childCount - 1)
        {
            nextIndx = 0;
        }

        return (prevIndx, nextIndx);
    }
    private void GetInput(int nextIndx, int prevIndx)
    {
        if (!uiManager.isPausing)
        {

            //switch (Input.GetAxis("Mouse ScrollWheel"))
            //{

            //}
            //int result;
            //if (TryConvertInputToInt(Input.inputString, out result))
            //{
                
            //}

        }
    }

    bool TryConvertInputToInt(string input, out int result)
    {
        try
        {
            if (Input.GetKeyUp(input))
            {
                result = Convert.ToInt32(input);
                return true;
            }
            result = 0;
            return false;
        }
        catch (Exception)
        {
            result = 0;
            return false;
        }
    }
}
