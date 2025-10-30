using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Outline))]

public abstract class BaseInteractable : MonoBehaviour, IInteractable
{
    [Header("Highlight Effect Setting")]
    protected Outline outline;
    protected bool isFocused = false;

    public string interactionPromptText; 

    public virtual void Awake() 
    {
        if(gameObject.layer != LayerMask.NameToLayer("Interactable")) 
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }

        #region Initialize Highlight Effect
        outline = GetComponent<Outline>();
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 3f;
        outline.enabled = false;

        #endregion

    }

    public string GetInteractionPrompt()
    {
        if (string.IsNullOrWhiteSpace(interactionPromptText)) 
        {

        }

        throw new System.NotImplementedException();
    }

    public virtual void OnInteract() 
    {
        Debug.Log("Interacted with" + gameObject.name); 
    }

    public void SetFocus(bool focused) 
    {
        if(isFocused == focused) 
        {
            return; 
        }

        isFocused = focused;

        if (focused) 
        {
            // perform the focus logic 
            outline.enabled = true; 
        }
        else 
        {
            // Lose focus logic
            outline.enabled = false; 
        }
    }
}
