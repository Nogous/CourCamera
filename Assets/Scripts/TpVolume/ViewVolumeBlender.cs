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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddVolume(AViewVolume viewVolume)
    {
        ActiveViewVolumes.Add(viewVolume);

        if (!VolumesPerViews.ContainsKey(viewVolume.view))
        {
            VolumesPerViews.Add(viewVolume.view, new List<AViewVolume>());
            viewVolume.view.SetActive(true);
        }

        foreach(var i in VolumesPerViews)
        {
            if(i.Key == viewVolume.view)
            {
                i.Value.Add(viewVolume);
            }
        }
        
    }
    public void RemoveVolume(AViewVolume viewVolume)
    {
        ActiveViewVolumes.Remove(viewVolume);

        foreach (var i in VolumesPerViews)
        {
            if (i.Key == viewVolume.view)
            {
                i.Value.Remove(viewVolume);

                if(i.Value.Count == 0)
                {
                    VolumesPerViews.Remove(i.Key);
                    i.Key.SetActive(false);
                }
            }
        }
    }
}
