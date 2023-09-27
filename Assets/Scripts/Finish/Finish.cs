using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//����

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("����");
            finishSound.Play();
            Invoke("finishLevel", 1.0f);//һ������finishLevel�������г���+1�任
        }
    }

    private void finishLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void victoryChangeScene()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1.0f;
    }
}