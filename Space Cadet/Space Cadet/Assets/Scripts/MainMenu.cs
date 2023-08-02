using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer mixer, effectMixer;
    private void Start() {
        effectMixer.SetFloat("effect", PlayerPrefs.GetFloat("effect"));
        mixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
