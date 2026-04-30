using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public GameObject uiPanel;
    private TowerBase targetTower;

    public Text damageText;
    public Text rangeText;
    public Text fireRateText;

    public Text damageCostText;
    public Text rangeCostText;
    public Text fireRateCostText;
    
    public void SetTarget(TowerBase tower)
    {
        targetTower = tower;
        uiPanel.SetActive(true);

        FindObjectOfType<FirstPersonController>().SetCursorLocked(false);

        UpdateUI();
    }

    public void Hide()
    {
        uiPanel.SetActive(false);
        // Lock cursor again
        FindObjectOfType<FirstPersonController>().SetCursorLocked(true);
    }

    public void UpgradeDamage()
    {
        targetTower.UpgradeDamage();
        UpdateUI();
    }

    public void UpgradeRange()
    {
        targetTower.UpgradeRange();
        UpdateUI();
    }

    public void UpgradeFireRate()
    {
        targetTower.UpgradeFireRate();
        UpdateUI();
    }

    void UpdateUI()
    {
        damageText.text = "Damage: " + targetTower.damage;
        rangeText.text = "Range: " + targetTower.range;
        fireRateText.text = "Fire Rate: " + targetTower.fireRate.ToString("F2");

        damageCostText.text = "$" + targetTower.damageUpgradeCost;
        rangeCostText.text = "$" + targetTower.rangeUpgradeCost;
        fireRateCostText.text = "$" + targetTower.fireRateUpgradeCost;
    }

    
}
