using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    [SerializeField] GameObject camHolder;
    [SerializeField] float speed, sensitivity, baseGravity = 9.8f, jumpForce, gravityReducer;
    private Vector2 move, look;
    private float actualGravity, lookRotation;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //when we are falling
        if (body.velocity.y < -0.1f)
        {
            //reset base gravity
            actualGravity = baseGravity;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(move.x, 0, move.y);
        body.AddRelativeForce(movement * speed);
        body.AddForce(Vector3.down * actualGravity);

    }

    private void LateUpdate()
    {
        //turn player
        transform.Rotate(Vector3.up * look.x * sensitivity);

        //look up and down
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //if we start holding the button
        if (context.phase == InputActionPhase.Performed)
        {
            //cancel out any negative velo
            body.velocity = new Vector3(body.velocity.x, 0.1f, body.velocity.z);
            //perform a 1 time impulse of force
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //reduce gravity while button held
            actualGravity = baseGravity / gravityReducer;
        }

        //when we release the button
        else if (context.phase == InputActionPhase.Canceled)
        {
            //reset gravity to normal
            actualGravity = baseGravity;
        }
    }

}
