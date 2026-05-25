using UnityEngine;

public class CharacterGroundChecker : MonoBehaviour
{
    [SerializeField] private BoxCollider col;
    [SerializeField] private LayerMask groundLayer;
    public bool IsGround;
    private int groundCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            IsGround = true;
            groundCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            groundCount--;
            if (groundCount <= 0)
                IsGround = false;
        }
    }
}
