using UnityEngine;

public interface IInteractable 
{
    // Call when the player interacts with this object
    void OnInteract();

    // Sets the focus state of the object
    void SetFocus(bool focused);


    string GetInteractionPrompt(); 

    
}
