using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenOpenerOnColision : ScenOpener
{
    public string NextLevelName;

    private void OnTriggerEnter2D(Collider2D Colision)
    {
        OpenScene(NextLevelName);
    }
}
