using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenOpener : MonoBehaviour
{
    public void OpenScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}

