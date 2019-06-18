using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void Start()
    {
        ToggleAll(menu, false);

    }
    public MaskableGraphic menu;

    public static bool isPaused = false;

    public void ToggleMenu()
    {
        isPaused = !isPaused;
        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;

        ToggleAll(menu, isPaused);

    }

    public void RestartGame()
    {
        isPaused = true;
        Time.timeScale = 1f;

        Debug.Log("Yo");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleAll(MaskableGraphic element, bool value)
    {
        foreach (Transform t in element.gameObject.transform)
        {
            ToggleAll(t.gameObject.GetComponent<MaskableGraphic>(), value);
        }
        element.enabled = value;
    }
}
tis