using UnityEngine;
using UnityEngine.SceneManagement;

public class rotate_on_click : MonoBehaviour
{
    float rotationsPerMinute = 10;
    bool rotate = false;
    void OnMouseDown()
    {
        rotate = !rotate;
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (rotate && currentScene.name == "intro")
        {
            transform.Rotate(0, 6 * rotationsPerMinute * Time.deltaTime, 0);
        }
    }
}
