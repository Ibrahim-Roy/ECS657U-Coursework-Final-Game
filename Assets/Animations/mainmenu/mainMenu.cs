using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject options;

    public void Play(){
        SceneManager.LoadScene("Main World");
    }

    public void OptionsOpen(){
        options.SetActive(true);
    }

    public void OptionClose(){
        options.SetActive(false);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("closing the game");
    }
}
