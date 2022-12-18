using UnityEngine;

public class Paddle : MonoBehaviour {
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;

    // Update is called once per frame
    void Update() {
        float input = Input.GetAxis("Horizontal");

        Vector3 position = transform.position;
        position.x += input * Speed * Time.deltaTime;

        if (position.x > MaxMovement)
            position.x = MaxMovement;
        else if (position.x < -MaxMovement)
            position.x = -MaxMovement;

        transform.position = position;
    }
}
