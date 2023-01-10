using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    [SerializeField] GameObject camHolder;
    [SerializeField] Camera cam;
    [SerializeField] float speed, sensitivity, groundDistance = .6f, baseGravity = 9.8f;
    [SerializeField] float jumpForce, gravityReducer;
    [SerializeField] float dashForce = 10f, dashRechargeTime = 2f, maxAdditiveDashAngle = 45f;
    [SerializeField] int multiJumpMax = 1, dashMax = 1;
    private Vector2 move, look;
    private float actualGravity, lookRotation;
    private int multiJumpCount, dashCount;
    bool isGrounded = false;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Currently Locking Cursor in the PlayerController Start");
        Cursor.lockState = CursorLockMode.Locked;
        multiJumpCount = multiJumpMax;
        dashCount = dashMax;
        actualGravity = baseGravity;
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
        //Check if we are grounded
        isGrounded = CheckGround();
    }

    private bool CheckGround()
    {
        //Make a raycast
        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        RaycastHit hitData;
        //if our raycast hits an object
        if (Physics.Raycast(groundCheckRay, out hitData, groundDistance))
        {
            //we are grounded
            //reset our multijumps
            multiJumpCount = multiJumpMax;
            return true;
        }
        //otherwise we arent
        else return false;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(move.x, 0, move.y);
        body.AddRelativeForce(movement * speed);
        body.AddForce(Vector3.down * actualGravity, ForceMode.Acceleration);
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
            //we check for grounding or multijumps remaining
            if (isGrounded || multiJumpCount > 0)
            {
                //when we arent grounded, we decrement the multijump count
                if (!isGrounded) multiJumpCount--;
                //cancel out any negative velo
                body.velocity = new Vector3(body.velocity.x, 0.1f, body.velocity.z);
                //perform a 1 time impulse of force
                body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                //reduce gravity while button held
                actualGravity = baseGravity / gravityReducer;
            }
        }
        //when we release the button
        else if (context.phase == InputActionPhase.Canceled)
        {
            //reset gravity to normal
            actualGravity = baseGravity;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        //when we hit the button and have a dash available
        if (context.phase == InputActionPhase.Started && dashCount > 0)
        {
            /*An interesting mechanic idea is that we go even faster if we dash in the direction we are facing, 
            but to ensure a minimum level of dash power, if we are facing in another direction we will first remove
            all velocity to ensure the dash is still powerful. this allows for escalating speed limited by physics*/

            //If we the angle between our velo and the direction we are facing is not within the range
            if (Vector3.Angle(cam.transform.forward, body.velocity) > maxAdditiveDashAngle)
            {
                //we will first reduce the velo to 0
                body.velocity = (Vector3.zero);
            }
            //we add our dash force to the player
            body.AddForce(cam.transform.forward * dashForce, ForceMode.Impulse);
            Debug.Log("applying force" + cam.transform.forward * dashForce + " from direction " + cam.transform.forward);
            //and decrement the dash count
            dashCount--;
            //then begin a coroutine to recharge our dash
            StartCoroutine(RechargeDash());
        }
    }

    IEnumerator RechargeDash()
    {
        //we wait for the recharge time
        yield return new WaitForSeconds(dashRechargeTime);
        //then we add a charge, up to the max count. technically clamp shouldnt be needed, as we only
        //add a charge when we remove one, but we should still be safe. this also allows for pickups
        //that would refresh dash.
        dashCount = Mathf.Clamp(dashCount + 1, 0, dashMax);
    }
}
