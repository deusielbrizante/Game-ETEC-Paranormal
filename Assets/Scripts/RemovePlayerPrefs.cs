using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerPrefs : MonoBehaviour
{

    private void Awake()
    {

        float audio = PlayerPrefs.GetFloat("audio");

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetFloat("audio", audio);
        PlayerPrefs.Save();
    }

}
