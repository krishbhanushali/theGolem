using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 3.0f;
    public float gravity = -9.8f;

    private CharacterController _charController;
    public GameObject Player;
    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.PLAYER_SPEED_CHANGED,
        ChangeSpeed);
    }
    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.PLAYER_SPEED_CHANGED,
        ChangeSpeed);
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        ChangeSpeed(speed);
    }

    void Update()
    {
        if (speed < 5.0f)
        {
            Player.GetComponent<Animation>().CrossFade("walk", 1.0f);
        }
        else
        {
            Player.GetComponent<Animation>().CrossFade("run", 1.0f);
        }
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);

    }

    public void ChangeSpeed(float value)
    {
        speed = value;
    }
}
