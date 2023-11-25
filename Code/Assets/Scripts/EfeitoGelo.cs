using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoGelo : MonoBehaviour
{
    //componente da partícula do jogador definida no inspetor
    [SerializeField] private ParticleSystem particula;

    public void AtivaEfeito()
    {
        //ativa as partículas
        particula.Play();
    }
}
