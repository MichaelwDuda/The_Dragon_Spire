using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public int damage = 10;
    public float range = 5f;
    public float fireRate = 1f;

    public int damageUpgradeCost = 50;
    public int rangeUpgradeCost = 40;
    public int fireRateUpgradeCost = 60;

    public void UpgradeDamage()
    {
        damage += 5;
        damageUpgradeCost += 25; // cost increases
    }

    public void UpgradeRange()
    {
        range += 1f;
        rangeUpgradeCost += 20;
    }

    public void UpgradeFireRate()
    {
        fireRate *= 0.9f; // shoots faster
        fireRateUpgradeCost += 30;
    }
}
