using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject btnPlay, btnExit, btnPause, btnResume, btnReplay, Panel, imgLose, imgWin;
    public static bool startControl = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void PlayButton()
    {
        Invoke(nameof(startTrue), 0.5f);
        btnExit.SetActive(false);
        btnPlay.SetActive(false);
        btnResume.SetActive(false);
        btnPause.SetActive(true);
        Panel.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }
    public void ResumeButton()
    {
        Invoke(nameof(startTrue), 0.5f);
        btnExit.SetActive(false);
        btnResume.SetActive(false);
        btnPause.SetActive(true);
        Panel.GetComponent<CanvasRenderer>().SetAlpha(0f);
    }
    public void PauseButton()
    {
        startControl = false;
        btnExit.SetActive(true);
        btnResume.SetActive(true);
        btnPause.SetActive(false);
        Panel.GetComponent<CanvasRenderer>().SetAlpha(1f);
    }
    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        btnReplay.SetActive(false);
        startFalse();
        PlayerController.Pause = false;
    }
    void startTrue()
    {
        startControl = true;
    }
    void startFalse()
    {
        startControl = false;
    }
    
    public void LoseScreen()
    {
        startFalse();
        btnPause.SetActive(false);
        Panel.GetComponent<CanvasRenderer>().SetAlpha(1f);
        btnExit.SetActive(true);
        btnReplay.SetActive(true);
        imgLose.SetActive(true);
    }
    public void WinScreen()
    {
        startFalse();
        btnPause.SetActive(false);
        Panel.GetComponent<CanvasRenderer>().SetAlpha(1f);
        btnExit.SetActive(true);
        btnReplay.SetActive(true);
        imgWin.SetActive(true);
    }
}
