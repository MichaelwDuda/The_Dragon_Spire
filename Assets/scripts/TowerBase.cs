using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public int damage;
    public float range;
    public float fireRate;

    public int damageUpgradeCost;
    public int rangeUpgradeCost;
    public int fireRateUpgradeCost;

    public virtual void UpgradeDamage()
    {
        damage += 5;
        damageUpgradeCost += 20;
    }

    public virtual void UpgradeRange()
    {
        range += 1f;
        rangeUpgradeCost += 15;
    }

    public virtual void UpgradeFireRate()
    {
        fireRate *= 0.9f;
        fireRateUpgradeCost += 25;
    }
}
