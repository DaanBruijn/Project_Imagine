using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // - Variables
    [SerializeField] private float _xRotation;
    
    void FixedUpdate()
    {
        LookAtPlayerObject();
    }

    void LookAtPlayerObject()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;
        direction.y = 0f;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(_xRotation, angle, 0f);
    }
}
