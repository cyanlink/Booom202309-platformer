using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//����
/// <summary>
/// ͨ�س����л����
/// </summary>
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

}