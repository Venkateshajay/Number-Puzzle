using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id : MonoBehaviour
{
    [SerializeField] private int currentNo;
    [SerializeField] private int actualNo;

    public int GetCurrentNo()
    {
        return currentNo;
    }

    public void SetCurrentN0(int No)
    {
        currentNo = No;
    }

    public bool Check()
    {
        if (currentNo == actualNo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
