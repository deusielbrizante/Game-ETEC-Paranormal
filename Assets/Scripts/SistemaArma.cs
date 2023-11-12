using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SistemaArma : MonoBehaviour
{

    //chama o objeto de tiro e pede a velocidade
    public GameObject objTiro;
    public float tiroSpeed;

    //pega a velocidade de ataque e verifica se pode atacar depois de um tempo
    //quanto menor a velocidade de ataque, mais rápido é o ataque
    public float velocidadeDeAtaque;
    private float podeAtacar = 1f;

    private void Update()
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

                if(nomeSprite + "_8" == spriteRenderer.sprite.name || nomeSprite + "_9" == spriteRenderer.sprite.name || nomeSprite + "_10" == spriteRenderer.sprite.name || nomeSprite + "_11" == spriteRenderer.sprite.name)
                {
                    tiroScript.direction = 0f;
                }
                else if (nomeSprite + "_4" == spriteRenderer.sprite.name || nomeSprite + "_5" == spriteRenderer.sprite.name || nomeSprite + "_6" == spriteRenderer.sprite.name || nomeSprite + "_7" == spriteRenderer.sprite.name)
                {
                    tiroScript.direction = 180f;
                }
                else if (nomeSprite + "_12" == spriteRenderer.sprite.name || nomeSprite + "_13" == spriteRenderer.sprite.name || nomeSprite + "_14" == spriteRenderer.sprite.name || nomeSprite + "_15" == spriteRenderer.sprite.name)
                {
                    tiroScript.direction = 90f;
                }
                else if (nomeSprite + "_0" == spriteRenderer.sprite.name || nomeSprite + "_1" == spriteRenderer.sprite.name || nomeSprite + "_2" == spriteRenderer.sprite.name || nomeSprite + "_3" == spriteRenderer.sprite.name)
                {
                    tiroScript.direction = -90f;
                }

                //reinicia o tempo de tiro
                tiroScript.speed = tiroSpeed;
                tiroScript.imageAngle = transform.eulerAngles.z;
                Debug.Log(nomeSprite);

                podeAtacar = 0f;

            }
        }
    }
}
