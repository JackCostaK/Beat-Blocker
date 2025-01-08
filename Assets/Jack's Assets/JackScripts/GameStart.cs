using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
   

    public void SceneToGame(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
