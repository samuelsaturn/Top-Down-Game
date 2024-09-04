using UnityEngine;

public class ChickenController : MonoBehaviour
{
    public Transform player;       // Referência ao transform do personagem
    public float fleeDistance = 4f;  // Distância mínima para que a galinha comece a fugir
    public float moveSpeed = 1.5f;   // Velocidade de movimento da galinha

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveAwayFromPlayer();
    }

    private void MoveAwayFromPlayer()
    {
        Vector2 direction = (Vector2)(transform.position - player.position);  // Direção oposta ao jogador
        float distance = direction.magnitude;

        if (distance < fleeDistance)
        {
            direction.Normalize();  // Normaliza a direção para manter a velocidade constante
            rb.velocity = direction * moveSpeed;  // Aplica a velocidade para mover a galinha

            animator.SetBool("isWalking", true);  // Ativa a animação de walk

            // Flip do sprite baseado na direção do movimento
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;  // Virar para a esquerda
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;  // Virar para a direita
            }
        }
        else
        {
            rb.velocity = Vector2.zero;  // Para o movimento
            animator.SetBool("isWalking", false);  // Volta para a animação de idle
        }
    }
}
