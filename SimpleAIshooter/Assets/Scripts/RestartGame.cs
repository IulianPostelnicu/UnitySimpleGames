using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{

    public void RestartGameFunction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
