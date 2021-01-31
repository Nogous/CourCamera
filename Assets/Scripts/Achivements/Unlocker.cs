using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour
{
    private AchivementChecker _checker;
    private int _id = 0;

    public void SetAchivementChecker(AchivementChecker checker, int id)
    {
        _checker = checker;
        _id = id;
    }

    public void SetEndColor()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnTriggerEnter(Collider other)
    {
        _checker.UnlockSwitch(this, _id);
        GetComponent<Renderer>().material.color = Color.green;
    }
}
