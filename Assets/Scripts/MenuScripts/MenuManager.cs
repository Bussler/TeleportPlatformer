using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject Menu;

    [SerializeField]
    AudioSource musicSource;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            openMenu();
    }

    void openMenu()
    {
        if (Menu.activeInHierarchy)
        {
            Time.timeScale = 1;//zeit auf 1 setzen
            Menu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;//zeit auf 0 setzen
            Menu.SetActive(true);
        }
    }

    public void closeMenu()
    {
        Time.timeScale = 1;//zeit auf 1 setzen
        Menu.SetActive(false);
    }

    public void openOptions() //TODO
    {

    }

    public void changeVolume()
    {

    }

    public void ExitToStart()
    {
        closeMenu();
        SceneManager.LoadScene(0);
    }

}
