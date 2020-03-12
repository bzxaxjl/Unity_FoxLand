using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    //主菜单事件
    //开始游戏
    public void PlayGame()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;

    }
    public void Select()
    {
        SceneManager.LoadScene(1);
    }
    public void Some()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
    //结束游戏
    public void QuitGame()
    {
        Application.Quit();
    }
    //“SOME”菜单事件
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    //返回到主菜单
    public void BackGame()
    {
        SceneManager.LoadScene(0);
    }
    public void UIEnable()
    {
        GameObject.Find("Canvas/Menu/UI").SetActive(true);
    }
    //重启本章游戏
    public void ReLoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;

    }
    //暂停游戏
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    //返回游戏
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    //控制游戏音量
    public void SetAudio(float value)
    {
        audioMixer.SetFloat("MainVolume",value);
    }
}
