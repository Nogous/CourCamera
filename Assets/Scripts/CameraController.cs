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
        CameraConfiguration config = new CameraConfiguration();

        if (activeViews.Count > 0)
        {
            float weight = 0f;
            bool weightZero;

            foreach (AView view in activeViews)
            {
                weight += view.weight;
            }

            if (weight == 0)
                weightZero = true;
            else
                weightZero = false;

            if (weightZero)
            {
                foreach (AView view in activeViews)
                {
                    CameraConfiguration tmpConfig = view.GetConfiguration();
                    config.yaw += tmpConfig.yaw;
                    config.pitch += tmpConfig.pitch;
                    config.roll += tmpConfig.roll;
                    config.fov += tmpConfig.fov;
                    config.pivot += tmpConfig.pivot;
                    config.distance += tmpConfig.distance;

                    weight++;
                }
            }
            else
            {
                foreach (AView view in activeViews)
                {
                    CameraConfiguration tmpConfig = view.GetConfiguration();
                    config.yaw += tmpConfig.yaw * view.weight;
                    config.pitch += tmpConfig.pitch * view.weight;
                    config.roll += tmpConfig.roll * view.weight;
                    config.fov += tmpConfig.fov * view.weight;
                    config.pivot += tmpConfig.pivot * view.weight;
                    config.distance += tmpConfig.distance * view.weight;
                }
            }

            config.yaw /= weight;
            config.pitch /= weight;
            config.roll /= weight;
            config.fov /= weight;
            config.pivot /= weight;
            config.distance /= weight;
        }


        transform.rotation = config.GetRotation();
        transform.position = config.GetPosition();
    }
}
