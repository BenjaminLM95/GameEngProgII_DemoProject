using UnityEngine;
using UnityEngine.UIElements;

public class LoadingUIController : MonoBehaviour
{
    UIDocument loadingDoc => GetComponent<UIDocument>(); 
    private ProgressBar progressBar;

    private void OnEnable()
    {
        var root = loadingDoc.rootVisualElement;
        progressBar = root.Q<ProgressBar>("ProgressBar");
        if(progressBar == null) 
        {
            Debug.LogError("Progress Bar not found in Loading UI Document");
        }
    }

    public void UpdateProgressBar(float progress) 
    {
        progressBar.value = progress;
        progressBar.title = $"{(int)(progress * 100)}%"; 
    }

}
