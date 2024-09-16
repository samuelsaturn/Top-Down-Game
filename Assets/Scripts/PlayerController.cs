using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;  // Velocidade ao correr
    public Animator myAnimator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Captura a entrada do teclado
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Define a direção de movimento
        movement = new Vector2(moveX, moveY).normalized;

        // Atualiza a última direção de movimento
        if (movement != Vector2.zero)
        {
            lastDirection = movement;
        }

        // Checa se o Shift está pressionado e ajusta a velocidade de movimento
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        // Atualiza os parâmetros de animação
        myAnimator.SetFloat("moveX", lastDirection.x);
        myAnimator.SetFloat("moveY", lastDirection.y);
        myAnimator.SetFloat("Speed", movement.sqrMagnitude);

        // Atualiza a movimentação
        MovePlayer(movement, currentSpeed);
    }

    private void MovePlayer(Vector2 direction, float speed)
    {
        // Move o jogador com base na direção e velocidade
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void FixedUpdate()
    {
        // Atualize a posição do jogador
        MovePlayer(movement, Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed);
    }
}
