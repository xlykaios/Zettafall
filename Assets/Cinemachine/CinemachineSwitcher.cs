
using UnityEngine;
using UnityEngine.InputSystem;


public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField]

    public InputAction action;
    private Animator animator;

    private bool isMainCameraActive = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();   
    }

    void Start()
    {
        action.performed += _ => SwitchState();
    }

    public void SwitchState() {
        if (isMainCameraActive) {
            animator.Play("Camera2State");
        } else {
            animator.Play("Camera1State");
        }
        isMainCameraActive = !isMainCameraActive;
    }

  
}
