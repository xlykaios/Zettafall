using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTrigger : MonoBehaviour
{

    [SerializeField]
    public string camera1;
    public string camera2;
    public InputAction action;
    public Animator animator;

    private bool didChangeHappen = true;


    


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

    // Start is called before the first frame update
    void Start()
    {

        /*
          action.performed += _ => SwitchState();
       

          cameraToSwitchTo.enabled = false;
          ActualCamera = Camera.current;
        */
    }

    public void SwitchState() {
        if (didChangeHappen) {
            animator.Play("Camera2State");
        } else {
            animator.Play("Camera2State");
        }
        didChangeHappen = !didChangeHappen;
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("triggered in");
            SwitchState();
           // cameraToSwitchTo.enabled = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("TriggerOut");
            SwitchState();
            //cameraToSwitchTo.enabled = false;
        }
    }
}


/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
 public Camera cam1;
 public Camera cam2;
    // Start is called before the first frame update
    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
    }
}
*/