using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void PlaySound()
    {
        SoundManager.instance.Play(SoundManager.SoundName.ButtonClicked);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
