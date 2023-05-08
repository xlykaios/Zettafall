using System.Collections;
//using System.Numerics;
using UnityEngine;

public class misonosmerdato : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float flipDuration = 0.2f;
    private CharacterController controller;
    //private Animator anim;
    private bool isGrounded;
    private Vector3 velocity;
    private float previousHorizontalInput;
    private bool isFlipping = false;
    Vector3 Cforward;
    Vector3 Cright;
   
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
    //    anim = GetComponent<Animator>();
        previousHorizontalInput = 0;
        Cforward = new Vector3(0,0,0);
        Cright = new Vector3(0, 0, 0);
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
      //  anim.SetFloat("Speed", Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical));

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        //    anim.SetBool("IsJumping", true);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Flip character
        if (!isFlipping)
        {
            Flip(moveHorizontal);
        }
    }

    void FixedUpdate() {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Cforward = Camera.current.transform.forward;
        Cright = Camera.current.transform.right;
        Cforward.y = 0;
        Cright.y = 0;
        Cforward = Cforward.normalized;
        Cright = Cright.normalized;

        Vector3 forwardRelativeVerticalInput = moveVertical * Cforward;
        Vector3 rightRelativeHorizontalInput = moveHorizontal * Cright;

        Vector3 cameraRealtiveMovement = forwardRelativeVerticalInput + rightRelativeHorizontalInput;

        controller.Move((cameraRealtiveMovement * moveSpeed + velocity) * Time.deltaTime);
    }

    
    private void Flip(float moveHorizontal)
    {
        if (moveHorizontal != 0 && Mathf.Sign(moveHorizontal) != Mathf.Sign(previousHorizontalInput))
        {
            StartCoroutine(FlipAnimation());
            previousHorizontalInput = moveHorizontal;
        }
    }

    public bool IsMoving()
    {
        return Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f;
    }

    IEnumerator HeadBob()
    {
        if (isFlipping) yield break;

        float bobSpeed = 2f;
        float bobAmount = 0.1f;
        Vector3 initialPosition = transform.localPosition;

        float newY = initialPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.localPosition = new Vector3(initialPosition.x, newY, initialPosition.z);
        yield return null;
    }

    IEnumerator FlipAnimation()
    {
        isFlipping = true;

        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(-startScale.x, startScale.y, startScale.z);

        float elapsedTime = 0;

        while (elapsedTime < flipDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / flipDuration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        transform.localScale = endScale;
        isFlipping = false;
    }
}

