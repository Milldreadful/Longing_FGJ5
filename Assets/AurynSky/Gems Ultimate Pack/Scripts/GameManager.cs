using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public int currentHappiness;
    public int maxHappiness = 50;
    public HappinessBarScript happinesBarScript;
    public PostProcessVolume PP;
    private ColorGrading colorGrading;
    public bool isMute = false;
    public Animator fadeScreen;
    public GameObject gameOverText;
    public GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
        happinesBarScript.UpdateHappiness(maxHappiness, currentHappiness);
        PP.profile.TryGetSettings(out colorGrading);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }

    public void MoveToLevel(int level)
    {
        fadeScreen.SetTrigger("Fade");
        StartCoroutine(LoadLevel(level, 1f));
    }

    public void AddHappiness()
    {
        currentHappiness += 1;
        happinesBarScript.UpdateHappiness(maxHappiness, currentHappiness);
        colorGrading.saturation.value += 5;

        if(currentHappiness == 37)
        {
            WinScreen();
        }
    }

    public void AddSadness()
    {
        currentHappiness -= 1;
        happinesBarScript.UpdateHappiness(maxHappiness, currentHappiness);
        colorGrading.saturation.value -= 5;

        if(currentHappiness == 0) 
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = Mathf.Lerp(1f, 0f, 0.7f);
        fadeScreen.SetTrigger("Fade");
        gameOverText.SetActive(true);
        AudioListener.volume = Mathf.Lerp(1f, 0f, 0.6f);
        StartCoroutine(LoadLevel(0, 1.5f));
    }

    public void WinScreen()
    {
        Time.timeScale = Mathf.Lerp(1f, 0f, 0.7f);
        fadeScreen.SetTrigger("Fade");
        winScreen.SetActive(true);
        StartCoroutine(LoadLevel(0, 1.5f));
    }

    public IEnumerator LoadLevel(int levelNum, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelNum);
    }
}
