using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementChecker : MonoBehaviour
{
    public AchivementList achivementName;

    private bool[] activity;

    private void Awake()
    {
        int id = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Unlocker _unlocker = gameObject.transform.GetChild(i).GetComponent<Unlocker>();
            if (_unlocker != null)
            {
                _unlocker.SetAchivementChecker(this, id);
                id++;
            }
        }

        activity = new bool[id];
    }

    public void UnlockSwitch(Unlocker unlocker, int id)
    {
        activity[id] = true;

        TestAllUnlock();
    }

    private void TestAllUnlock()
    {
        for (int i = activity.Length; i-- > 0;)
        {
            if (!activity[i])
                return;
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Unlocker _unlocker = gameObject.transform.GetChild(i).GetComponent<Unlocker>();
            if (_unlocker != null)
            {
                _unlocker.SetEndColor();
            }
        }

        Achivement.instance.UnlockAchivement(achivementName);
    }
}
