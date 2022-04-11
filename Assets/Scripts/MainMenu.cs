using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool inInventory = false;
    public static bool paused = false;

    public Toggle fullscreen;


    void Start(){
        if(PlayerPrefs.GetInt("fullScreen") == 0) fullscreen.isOn = false;
        else fullscreen.isOn = true;

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        #if UNITY_EDITOR
        //Debug.Log("Unity Editor");
        PlayerPrefs.SetString("control", "PC");
        #elif UNITY_STANDALONE_WIN
        //Debug.Log("Unity Windows");
        PlayerPrefs.SetString("control", "PC");
        # elif UNITY_ANDROID || UNITY_IOS
        //Debug.Log("Unity Mobile");
        PlayerPrefs.SetString("control", "MOBILE");
        # else
        //Debug.Log("Any other platform");
        # endif
    }

    void Update(){

        Debug.Log(PlayerPrefs.GetInt("fullScreen"));

        if(fullscreen.isOn){
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("fullScreen", 1);
        }
        else {
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("fullScreen", 0);
        }
    }



    public void exitScene(){
        Application.Quit();
    }

    public static void gameScene(){
        DontDestroyOnLoad(GameObject.Find("GameManagerMenu"));
        SceneManager.LoadScene("Game");
        inInventory = false;
        paused = false;
        Time.timeScale = 1f;
    }

    public void settingsScene(){
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Settings");
    }

    public static void MainMenuScene(){
        Destroy(GameObject.Find("GameManagerMenu"));
        SceneManager.LoadScene("MainMenu");
    }
    
    public static void End(){
        DontDestroyOnLoad(GameObject.Find("GameManagerMenu"));
        paused = true;
        Time.timeScale = 0f;
        SceneManager.LoadScene("EndScene");
    }

    public static void Pause(GameObject pausePanel){
        paused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public static void Resume(GameObject pausePanel){
        paused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }
}
