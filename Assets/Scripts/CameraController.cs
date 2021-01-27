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

    public float cameraMoveSpeed = 0.1f;

    private float elapsedTime = 0;

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
        if (smoothMove)
        {
            SmoothMovement();
        }
        else
        {
            configCommon = new CameraConfiguration();

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
                        configCommon.yaw += tmpConfig.yaw;
                        configCommon.pitch += tmpConfig.pitch;
                        configCommon.roll += tmpConfig.roll;
                        configCommon.fov += tmpConfig.fov;
                        configCommon.pivot += tmpConfig.pivot;
                        configCommon.distance += tmpConfig.distance;

                        weight++;
                    }
                }
                else
                {
                    foreach (AView view in activeViews)
                    {
                        CameraConfiguration tmpConfig = view.GetConfiguration();
                        configCommon.yaw += tmpConfig.yaw * view.weight;
                        configCommon.pitch += tmpConfig.pitch * view.weight;
                        configCommon.roll += tmpConfig.roll * view.weight;
                        configCommon.fov += tmpConfig.fov * view.weight;
                        configCommon.pivot += tmpConfig.pivot * view.weight;
                        configCommon.distance += tmpConfig.distance * view.weight;
                    }
                }

                configCommon.yaw /= weight;
                configCommon.pitch /= weight;
                configCommon.roll /= weight;
                configCommon.fov /= weight;
                configCommon.pivot /= weight;
                configCommon.distance /= weight;
            }


            transform.rotation = configCommon.GetRotation();
            transform.position = configCommon.GetPosition();
            //cameraComponent.fieldOfView = configCommon.fov;
        }
    }

    private void SmoothMovement()
    {
        elapsedTime += Time.deltaTime;
        if (cameraMoveSpeed * elapsedTime < 1)
        {
            transform.position = Vector3.Lerp(configCommon.GetPosition(), configTarget.GetPosition(), cameraMoveSpeed * elapsedTime);
            transform.rotation = Quaternion.Lerp(configCommon.GetRotation(), configTarget.GetRotation(), cameraMoveSpeed * elapsedTime);
            cameraComponent.fieldOfView = Mathf.Lerp(configCommon.fov, configTarget.fov, cameraMoveSpeed * elapsedTime);
        }
        else
        {
            transform.position = configTarget.GetPosition();
            transform.rotation = configTarget.GetRotation();
            cameraComponent.fieldOfView = configTarget.fov;
        }
            
    }

    private void OnDrawGizmos()
    {
        configCommon.DrawGizmos(Color.white);
        configTarget.DrawGizmos(Color.white);
    }
}
