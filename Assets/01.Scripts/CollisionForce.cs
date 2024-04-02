using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float forceApplied = 10f;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.Normalize();

            rb.AddForce(direction * forceApplied, ForceMode.Impulse);
        }
    }
}
