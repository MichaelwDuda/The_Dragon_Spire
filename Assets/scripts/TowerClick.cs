using UnityEngine;

public class TowerClick : MonoBehaviour
{
    public UpgradeUI towerUI;

    void OnMouseDown()
    {
        towerUI.SetTarget(GetComponent<TowerBase>());
    }
}
