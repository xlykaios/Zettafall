using System.Collections;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;

    [Header("Lock Rotation")]
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;

    [Header("Door Properties")]
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private float doorOpenDistance;
    [SerializeField] private float doorOpenSpeed;

    private Vector3 originalRotation;
    private bool doorOpened;
    private bool playerInsideTrigger;
    private float currentMinAngleThreshold;
    private float currentMaxAngleThreshold;

    public enum BillboardType { LookAtCamera, CameraForward };

    private void Awake()
    {
        originalRotation = transform.rotation.eulerAngles;
        doorOpened = false;
    }

    private void LateUpdate()
    {
        switch (billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up);
                break;
            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            default:
                break;
        }

        CheckAndOpenDoor();
    }

    private void CheckAndOpenDoor()
    {
        if (doorOpened || !playerInsideTrigger) return;

        float angle = Vector3.Angle(Camera.main.transform.forward, transform.forward);
        Debug.Log("Angle between camera forward and sprite: " + angle);

        if (angle > currentMinAngleThreshold && angle < currentMaxAngleThreshold)
        {
            StartCoroutine(OpenDoor());
            doorOpened = true;
        }
    }

    private IEnumerator OpenDoor()
    {
        Vector3 leftDoorInitialPosition = leftDoor.transform.position;
        Vector3 rightDoorInitialPosition = rightDoor.transform.position;

        Vector3 leftDoorTargetPosition = leftDoor.transform.position + leftDoor.transform.right * doorOpenDistance;
        Vector3 rightDoorTargetPosition = rightDoor.transform.position - rightDoor.transform.right * doorOpenDistance;

        float elapsedTime = 0f;

        while (elapsedTime < doorOpenSpeed)
        {
            elapsedTime += Time.deltaTime;

            leftDoor.transform.position = Vector3.Lerp(leftDoorInitialPosition, leftDoorTargetPosition, elapsedTime / doorOpenSpeed);
            rightDoor.transform.position = Vector3.Lerp(rightDoorInitialPosition, rightDoorTargetPosition, elapsedTime / doorOpenSpeed);

            yield return null;
        }
    }

    public void PlayerEnteredTrigger(float minAngleThreshold, float maxAngleThreshold)
    {
        playerInsideTrigger = true;
        currentMinAngleThreshold = minAngleThreshold;
        currentMaxAngleThreshold = maxAngleThreshold;
    }

    public void PlayerExitedTrigger()
    {
        playerInsideTrigger = false;
    }
}
