using UnityEngine;
using UnityEngine.InputSystem;

public class TowerDetector : MonoBehaviour
{
    public float detectRange = 10f;
    public LayerMask towerLayer;

    private TowerHighlight currentTower;
    public UpgradeUI upgradeUI;
    public int Epress = 0;

    void Update()
    {
        DetectTower();
        CheckInteract();
    }

    void DetectTower()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectRange, towerLayer))
        {
            TowerHighlight tower = hit.collider.GetComponentInParent<TowerHighlight>();

            if (tower != null)
            {
                if (currentTower != tower)
                {
                    ClearHighlight();
                    currentTower = tower;
                    currentTower.Highlight();
                }
                return;
            }
        }

        ClearHighlight();
    }

    void ClearHighlight()
    {
        if (currentTower != null)
        {
            currentTower.Unhighlight();
            currentTower = null;
        }
    }

    void CheckInteract()
    {
        if (currentTower != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Epress += 1;
            TowerBase baseTower = currentTower.GetComponent<TowerBase>();
            upgradeUI.SetTarget(baseTower);
        }

    }
}
