using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    [SerializeField] private GameObject[] Aliens;
    [SerializeField] private float tempoParaSpawnar;
    private float tempoCorrido;
    private int numeroAlien;

    private void Start()
    {
        tempoCorrido = tempoParaSpawnar - 3f;
    }

    private void Update()
    {

        if (tempoCorrido >= tempoParaSpawnar)
        {
            StartCoroutine(Spawner());
            tempoCorrido = 0f;
        }
        else
        {
            tempoCorrido += Time.deltaTime;
        }

    }

    private IEnumerator Spawner()
    {

        numeroAlien = Random.Range(0, 3);
        Instantiate(Aliens[numeroAlien], transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(2f);
        
        numeroAlien = Random.Range(0, 3);
        Instantiate(Aliens[numeroAlien], transform.position, Quaternion.identity);

    }

}
