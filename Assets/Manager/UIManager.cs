using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument mainMenuUI;
    [SerializeField] private UIDocument pauseUI;
    [SerializeField] private UIDocument gamePlayUI; 

       
   

    private void Awake() 
    {
        mainMenuUI = FindUIDocument("MainMenuUI");
        pauseUI = FindUIDocument("PauseUI");
        gamePlayUI = FindUIDocument("GameplayUI"); 
        

    }

    private void Start() 
    {
        DisabledMenuUI();
    }
        


    public void DisabledMenuUI() 
    {
        mainMenuUI.rootVisualElement.style.display = DisplayStyle.None;
        pauseUI.rootVisualElement.style.display = DisplayStyle.None;
        gamePlayUI.rootVisualElement.style.display = DisplayStyle.None;        
        
    }

    public void EnableMainMenu() 
    {
        DisabledMenuUI();
        mainMenuUI.rootVisualElement.style.display = DisplayStyle.Flex;
        
        
    }

    public void EnableGameplay() 
    {
        DisabledMenuUI();
        gamePlayUI.rootVisualElement.style.display = DisplayStyle.Flex;
        
    }

    public void EnablePause() 
    {
        DisabledMenuUI();
        pauseUI.rootVisualElement.style.display = DisplayStyle.Flex;
        
    }

    


    private UIDocument FindUIDocument(string name)
    {
        var documents = Object.FindObjectsByType<UIDocument>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var doc in documents)
        {
            if (doc.name == name)
            {
                return doc;
            }
        }
        Debug.LogWarning($"UIDocument '{name}' not found in scene.");
        return null;
    }

}
