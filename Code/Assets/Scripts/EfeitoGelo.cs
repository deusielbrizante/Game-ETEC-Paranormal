using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoGelo : MonoBehaviour
{
    //componente da part�cula do jogador definida no inspetor
    [SerializeField] private ParticleSystem particula;

    public void AtivaEfeito()
    {
        //ativa as part�culas
        particula.Play();
    }
}
