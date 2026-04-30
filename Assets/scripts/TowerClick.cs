using UnityEngine;

public class TowerClick : MonoBehaviour
{
    public UpgradeUI towerUI;

    void OnMouseDown()
    {
        Debug.Log("Tower clicked!");
        towerUI.SetTarget(GetComponent<TowerBase>());
    }
}
