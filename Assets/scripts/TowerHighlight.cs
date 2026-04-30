using UnityEngine;

public class TowerHighlight : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;
    }

    public void Highlight()
    {
        rend.material.color = Color.yellow;
    }

    public void Unhighlight()
    {
        rend.material.color = originalColor;
    }
}
