using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeneList : MonoBehaviour
{
    public GameObject menuList;//菜单列表

    [SerializeField] private bool menuKeys = true;
    [SerializeField] private AudioSource bgmSound;//背景音乐

    void Start()
    {
        menuList.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (menuKeys == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(true);
                Time.timeScale = 0;
                menuKeys = false;
                bgmSound.Pause();//背景音乐暂停
                AudioListener.volume = 0;//全部音乐音量大小为0
            }
        }
        else if (menuKeys == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(false);
                Time.timeScale = 1;
                menuKeys = true;
                bgmSound.Play();//背景音乐播放
                AudioListener.volume = 1;//全部音乐音量大小为正常
            }
        }

    }
    public void ReturnGame()
    {
        menuList.SetActive(false);
        Time.timeScale = 1;
        menuKeys = true;
        AudioListener.volume = 1;
    }

    public void ReturnSafeHouse()
    {
        SceneManager.LoadScene(10);//回到安全屋
        Time.timeScale = 1;
        AudioListener.volume = 1;

    }

    public void MainInterface()
    {
        SceneManager.LoadScene(0);//回到主界面
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
}
