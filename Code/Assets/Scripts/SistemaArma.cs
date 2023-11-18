using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SistemaArma : MonoBehaviour
{

    //chama o objeto de tiro e pede a velocidade
    public GameObject objTiro;
    public float tiroSpeed;

    //pega a velocidade de ataque e verifica se pode atacar depois de um tempo
    //quanto menor a velocidade de ataque, mais rápido é o ataque
    public float velocidadeDeAtaque;
    private float podeAtacar = 1f;

    private void FixedUpdate()
    {
        podeAtacar += Time.fixedDeltaTime;
    }

    public void Atacar(InputAction.CallbackContext valor)
    {
        if (velocidadeDeAtaque <= podeAtacar)
        {
            if (valor.performed)
            {
                DispararTiro();
            }
        }
    }

    private void DispararTiro()
    {
        //instancia o tiro na posição atual do jogador 
        GameObject tiro = Instantiate(objTiro, transform.position, Quaternion.identity);
        Bala tiroScript = tiro.GetComponent<Bala>();

        //verifica se o objeto da bala não é nulo
        if (tiroScript != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            //verifica se o sprite do jogador não é nulo
            if (spriteRenderer != null)
            {

                int tamanhoSprite = gameObject.name.Length - 7;
                string nomeSprite = gameObject.name.Remove(tamanhoSprite);
                Debug.Log(nomeSprite);
                Debug.Log(spriteRenderer.sprite.name);
                switch (spriteRenderer.sprite.name)
                {
                    case var s when s == nomeSprite + "_8" || s == nomeSprite + "_9" || s == nomeSprite + "_10" || s == nomeSprite + "_11":
                        tiroScript.direction = 0f;
                        break;

                    case var s when s == nomeSprite + "_4" || s == nomeSprite + "_5" || s == nomeSprite + "_6" || s == nomeSprite + "_7":
                        tiroScript.direction = 180f;
                        break;

                    case var s when s == nomeSprite + "_12" || s == nomeSprite + "_13" || s == nomeSprite + "_14" || s == nomeSprite + "_15":
                        tiroScript.direction = 90f;
                        break;

                    case var s when s == nomeSprite + "_0" || s == nomeSprite + "_1" || s == nomeSprite + "_2" || s == nomeSprite + "_3":
                        tiroScript.direction = -90f;
                        break;

                    default:
                        Debug.Log($"deu ruim");
                        break;
                }

                //reinicia o tempo de tiro
                tiroScript.speed = tiroSpeed;
                tiroScript.imageAngle = transform.eulerAngles.z;

                podeAtacar = 0f;

            }
        }
    }
}
