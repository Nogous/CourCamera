using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achivementColider : MonoBehaviour
{
    public AchivementList achivementName;

    private void OnTriggerEnter(Collider other)
    {
        Achivement.instance.UnlockAchivement(achivementName);
    }
}
