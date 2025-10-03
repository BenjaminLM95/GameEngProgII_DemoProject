using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{    
    public GameObject mainMenuUI;
    public GameObject gameplayUI;
    public GameObject pauseUI;
    public GameObject gameoverUI; 

    private void Awake() 
    {
        DisabledMenuUI();
    }


    public void DisabledMenuUI() 
    {
        mainMenuUI.SetActive(false);
        pauseUI.SetActive(false);
        gameplayUI.SetActive(false);
        gameoverUI.SetActive(false);
    }

    public void EnableMainMenu() 
    {
        DisabledMenuUI();
        mainMenuUI.SetActive(true);
    }

    public void EnableGameplay() 
    {
        DisabledMenuUI();
        gameplayUI.SetActive(true);
    }

    public void EnablePause() 
    {
        DisabledMenuUI();
        pauseUI.SetActive(true);
    }

    public void EnableGameover() 
    {
        DisabledMenuUI();
        gameoverUI.SetActive(true);
    }


}
