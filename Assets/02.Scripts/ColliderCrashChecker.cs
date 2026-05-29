using UnityEngine;

public class ColliderCrashChecker : MonoBehaviour
{
    [SerializeField] private Collider col;
    [SerializeField] private LayerMask colliderLayer;
    public bool IsCrashed;
    private int groundCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((colliderLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            IsCrashed = true;
            groundCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((colliderLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            groundCount--;
            if (groundCount <= 0)
                IsCrashed = false;
        }
    }
}
