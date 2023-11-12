using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed;
    public float direction;
    public float imageAngle;

    private void Start()
    {
        // Rotacionar a imagem do tiro para o ângulo especificado
        transform.eulerAngles = new Vector3(0, 0, imageAngle);

        // Calcular o vetor de direção com base no ângulo
        Vector2 moveDirection = new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad));

        // Aplicar a velocidade ao tiro
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDirection * speed;
    }
}
