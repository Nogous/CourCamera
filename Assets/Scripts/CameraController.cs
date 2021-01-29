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

    public float cameraMoveSpeed = 0.2f;

    public bool smoothMove = false;

    private List<AView> activeViews = new List<AView>();

    private Camera cameraComponent;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

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
        configTarget = new CameraConfiguration();

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
                    configTarget.yaw += tmpConfig.yaw;
                    configTarget.pitch += tmpConfig.pitch;
                    configTarget.roll += tmpConfig.roll;
                    configTarget.fov += tmpConfig.fov;
                    configTarget.pivot += tmpConfig.pivot;
                    configTarget.distance += tmpConfig.distance;

                    weight++;
                }
            }
            else
            {
                foreach (AView view in activeViews)
                {
                    CameraConfiguration tmpConfig = view.GetConfiguration();
                    configTarget.yaw += tmpConfig.yaw * view.weight;
                    configTarget.pitch += tmpConfig.pitch * view.weight;
                    configTarget.roll += tmpConfig.roll * view.weight;
                    configTarget.fov += tmpConfig.fov * view.weight;
                    configTarget.pivot += tmpConfig.pivot * view.weight;
                    configTarget.distance += tmpConfig.distance * view.weight;
                }
            }

            configTarget.yaw /= weight;
            configTarget.pitch /= weight;
            configTarget.roll /= weight;
            configTarget.fov /= weight;
            configTarget.pivot /= weight;
            configTarget.distance /= weight;
        }


        if (smoothMove)
        {
            SmoothMovement();

            transform.position = configCommon.GetPosition();
            transform.rotation = configCommon.GetRotation();
            cameraComponent.fieldOfView = configCommon.fov;
        }
        else
        {
            transform.rotation = configTarget.GetRotation();
            transform.position = configTarget.GetPosition();
            cameraComponent.fieldOfView = configTarget.fov;
        }

    }

    private void SmoothMovement()
    {
        
        if (cameraMoveSpeed * Time.deltaTime < 1)
        {
            configCommon.pivot = configCommon.pivot + (configTarget.pivot - configCommon.pivot) * cameraMoveSpeed * Time.deltaTime;
            configCommon.yaw = configCommon.yaw + (configTarget.yaw - configCommon.yaw) * cameraMoveSpeed * Time.deltaTime;
            configCommon.roll = configCommon.roll + (configTarget.roll - configCommon.roll) * cameraMoveSpeed * Time.deltaTime;
            configCommon.pitch = configCommon.pitch + (configTarget.pitch - configCommon.pitch) * cameraMoveSpeed * Time.deltaTime;
            configCommon.fov = configCommon.fov + (configTarget.fov - configCommon.fov) * cameraMoveSpeed * Time.deltaTime;
            configCommon.distance = configCommon.distance + (configTarget.distance - configCommon.distance) * cameraMoveSpeed * Time.deltaTime;
        }
        else
        {
            configCommon.yaw = configTarget.yaw;
            configCommon.roll = configTarget.roll;
            configCommon.pitch = configTarget.pitch;
            configCommon.fov = configTarget.fov;
            configCommon.pivot = configTarget.pivot;
            configCommon.distance = configTarget.distance;
        }
            
    }

    private void OnDrawGizmos()
    {
        if(configTarget != null)
        {
            configTarget.DrawGizmos(Color.green);
        }
        if(configTarget != null)
        {
            configTarget.DrawGizmos(Color.blue);
        }
    }
}
