using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomClique : MonoBehaviour
{

    [SerializeField] private AudioSource audioClique;

    public void SomDoClique()
    {
        audioClique.Play();
    }
}
