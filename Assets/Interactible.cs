using TMPro;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] LayerMask interactableMask;

    [SerializeField] private Color highlightColor = Color.yellow;
    private SpriteRenderer SpriteRenderer;
    private Color originalColor;

    TextMeshPro interactionText;

    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out SpriteRenderer);
        originalColor = GetComponent<SpriteRenderer>().color;

        interactionText = GetComponent<TextMeshPro>();
    }

    public void HighLight( bool isActive)
    {
        if (isActive)
        {
            SpriteRenderer.color = highlightColor;
            interactionText.text = "Press E to Interact"; //Interaction prompt
        }
        else
        {
            SpriteRenderer.color = originalColor;
            interactionText.text = ""; // clear the prompt
        }
    }
    public void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);
        gameObject.SetActive(false); //deactivation
    }

}
