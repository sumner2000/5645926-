using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]
    private GameController gameController;

    private MovementRigidbody2D movement2D;
    private PlayerHP playerHP;
    private void Awake()
    {
        movement2D = GetComponent<MovementRigidbody2D>();
        playerHP = GetComponent<PlayerHP>();
    }

    private void Update()
    {
        if (gameController.IsGamePlay == false) return; // IsGamePlay가 false일 때 플레이어의 이동, 점프 불가능
        UpdateMove();
        UpdateJump();
    }

    private void UpdateMove()
    {
        // left, a = -1  /  none = 0  /  right, d = +1
        float x = Input.GetAxisRaw("Horizontal");

        // 좌우 이동
        movement2D.MoveTo(x);
    }

    private void UpdateJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            movement2D.JumpTo();
        }
        else if (Input.GetKey(jumpKey))
        {
            movement2D.IsLongJump = true;
        }
        else if (Input.GetKeyUp(jumpKey))
        {
            movement2D.IsLongJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            bool isDie = playerHP.TakeDamage();
            if (isDie == true)
            {
                GetComponent<Collider2D>().enabled = false;
                gameController.GameOver();
            }
        }
        else if (collision.CompareTag("HPPotion"))
        {
            collision.gameObject.SetActive(false);
            playerHP.RecoveryHP();
        }
    }
}
