using UnityEngine;

public class MobileAttackButton : MonoBehaviour
{
    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
    }

    public void Attack()
    {
        if(playerAttack != null)
        {
            playerAttack.Attack();
        }
    }
}
