using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boat")
        SceneManager.LoadScene("Main Lobby");
    }
}
