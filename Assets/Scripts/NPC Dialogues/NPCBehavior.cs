using System.Collections;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D npcRigidbody;
    private bool isFlipping;
    private float movementDelay = 2f;
    private float movementDuration = 0.5f;
    private float timeSinceLastMovement;

    private void Awake()
    {
        // Get the required components
        npcRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeSinceLastMovement += Time.deltaTime;

        // NPC moves and flips every few seconds
        if (timeSinceLastMovement >= movementDelay + movementDuration && !isFlipping)
        {
            MoveAndFlipNPC();
            timeSinceLastMovement = 0;
        }
    }

    private void MoveAndFlipNPC()
    {
        MoveNPC();
        StartCoroutine(FlipSpriteOverTime());
    }

    private void MoveNPC()
    {
        Vector2 movementDirection = new Vector2(Random.Range(-1f, 1f), npcRigidbody.velocity.y);
        npcRigidbody.velocity = movementDirection * moveSpeed;
    }

    IEnumerator FlipSpriteOverTime()
    {
        isFlipping = true;

        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);

        float flipDuration = 0.5f; // Duration of the flip in seconds
        float elapsedTime = 0;

        while (elapsedTime < flipDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / flipDuration;

            transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, t);

            yield return null;
        }

        // Make sure the rotation ends up exactly at the final rotation
        transform.rotation = finalRotation;

        isFlipping = false;
    }
}
