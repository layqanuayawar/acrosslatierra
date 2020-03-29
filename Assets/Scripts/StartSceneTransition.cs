using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneTransition : MonoBehaviour
{   public void StartTimeline()
    {
        SceneManager.LoadScene("Timeline");
    }
}
