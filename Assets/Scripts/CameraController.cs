using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
        if(instance == null){

            instance = this;
            DontDestroyOnLoad(this.gameObject);

        } else
        {
            Destroy(this);
        }
    }

    public CameraConfiguration configCommon;
    public CameraConfiguration configTarget;

    public float cameraSpeed = 0.1f;

    private List<AView> activeViews = new List<AView>();

    public void AddView(AView view)
    {
        activeViews.Add(view);
    }

    public void RemoveView(AView view)
    {
        activeViews.Remove(view);
    }

    private void Update()
    {
        
    }
}
