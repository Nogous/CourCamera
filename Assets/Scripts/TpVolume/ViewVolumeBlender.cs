using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewVolumeBlender : MonoBehaviour
{
    // Start is called before the first frame update

    public static ViewVolumeBlender instance;

    private void Awake()
    {
        if(instance == null){

            instance = this;

        } else
        {
            Destroy(this);
        }
    }

    private List<AViewVolume> ActiveViewVolumes = new List<AViewVolume>();
    private Dictionary<AView, List<AViewVolume>> VolumesPerViews = new Dictionary<AView, List<AViewVolume>>();


    // Update is called once per frame
    void Update()
    {
        int heightsPrio = int.MinValue;

        foreach (AViewVolume item in ActiveViewVolumes)
        {
            item.view.weight = 0;
            if (item.priority> heightsPrio)
            {
                heightsPrio = item.priority;
            }
        }
        foreach (AViewVolume item in ActiveViewVolumes)
        {
            if (heightsPrio == item.priority)
            {
                item.view.weight = item.ComputeSelfWeight();
            }
        }
    }

    public void AddVolume(AViewVolume viewVolume)
    {
        ActiveViewVolumes.Add(viewVolume);

        Achivement.instance.UnlockAchivement(AchivementList.Suivre_le_joueur);

        if (!VolumesPerViews.ContainsKey(viewVolume.view))
        {
            VolumesPerViews.Add(viewVolume.view, new List<AViewVolume>());
            viewVolume.view.SetActive(true);
        }

        VolumesPerViews[viewVolume.view].Add(viewVolume);
    }
    public void RemoveVolume(AViewVolume viewVolume)
    {
        ActiveViewVolumes.Remove(viewVolume);

        if (VolumesPerViews.ContainsKey(viewVolume.view))
        {
            VolumesPerViews[viewVolume.view].Remove(viewVolume);

            if (VolumesPerViews[viewVolume.view].Count <= 0)
            {
                VolumesPerViews.Remove(viewVolume.view);
                viewVolume.view.SetActive(false);
            }
        }
    }

    private void OnGUI()
    {
        foreach (AViewVolume item in ActiveViewVolumes)
        {
            GUILayout.Label(item.name);
        }
    }
}
