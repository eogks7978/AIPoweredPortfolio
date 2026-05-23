using UnityEngine;

public class SkyBoxCamera : MonoBehaviour
{
    [SerializeField] private Transform mainCameraTr;

    private void Start()
    {
        Initialize();
    }

    private void Update() 
    {
        FollowMainCameraRotation();
    }

    private void Initialize()
    {
        if (mainCameraTr == null)
            mainCameraTr = Camera.main.transform;
    }

    private void FollowMainCameraRotation()
    {
        if (mainCameraTr != null)
        {
            transform.rotation = mainCameraTr.rotation;
        }
    }
}
