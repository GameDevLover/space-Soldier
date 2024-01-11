using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Transform orientation;
    [SerializeField] SpeedController speedController;
    [SerializeField] float speedRotation;

    public bool checkStaying = false;

    float moveSpeed;
    Vector2 playerInput;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        moveSpeed = speedController.speed;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(playerInput.x, 0f, playerInput.y);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        RotatePlayer(movement);
        Animations(movement);
    }

    void Animations(Vector3 movement)
    {
        if (movement == Vector3.zero)
        {
            anim.SetTrigger("Idle");
        }
        else if (checkStaying == false && movement != Vector3.zero)
        {
            anim.SetTrigger("Run");
        }
    }

    void RotatePlayer(Vector3 movement)
    {
        if (!ZeroPlayerInput())
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), speedRotation);
        }
    }

    bool ZeroPlayerInput()
    {
        return playerInput.Equals(Vector3.zero);
    }

    public void Move(InputAction.CallbackContext _context)
    {
        playerInput = _context.ReadValue<Vector2>();
    }

    public Animator Anim()
    {
        return anim;
    }
}