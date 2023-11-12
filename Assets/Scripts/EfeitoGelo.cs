using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitoGelo : MonoBehaviour
{
    [SerializeField] private ParticleSystem particula;

    public void AtivaEfeito()
    {

        particula.Play();

    }
}
