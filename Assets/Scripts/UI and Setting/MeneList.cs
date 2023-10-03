using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeneList : MonoBehaviour
{
    public GameObject menuList;//�˵��б�

    [SerializeField] private bool menuKeys = true;
    [SerializeField] private AudioSource bgmSound;//��������

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
                bgmSound.Pause();//����������ͣ
                AudioListener.volume = 0;//ȫ������������СΪ0
            }
        }
        else if (menuKeys == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(false);
                Time.timeScale = 1;
                menuKeys = true;
                bgmSound.Play();//�������ֲ���
                AudioListener.volume = 1;//ȫ������������СΪ����
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
        SceneManager.LoadScene(10);//�ص���ȫ��
        Time.timeScale = 1;
        AudioListener.volume = 1;

    }

    public void MainInterface()
    {
        SceneManager.LoadScene(0);//�ص�������
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
}
