using UnityEngine;

public class Ball : MonoBehaviour {
    [Header("Components")]
    [SerializeField] private Rigidbody m_Rigidbody;
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip brickSound;
    [SerializeField] private AudioClip hitSound;
    
    private void OnCollisionExit(Collision other) {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f) {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f) {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Brick")) {
            audioSource.PlayOneShot(brickSound);
        } else if (collision.gameObject.CompareTag("Untagged")) {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
