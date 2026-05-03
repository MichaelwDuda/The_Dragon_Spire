using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponScripts;
    [SerializeField] private GameObject[] weaponVisuals;

    private int currentIndex;

    private void Start()
    {
        SelectWeapon(0);
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SelectWeapon(0);

        if (Keyboard.current.digit2Key.wasPressedThisFrame && weaponScripts.Length > 1)
            SelectWeapon(1);

        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll > 0)
            SelectWeapon((currentIndex + 1) % weaponScripts.Length);
        else if (scroll < 0)
            SelectWeapon((currentIndex - 1 + weaponScripts.Length) % weaponScripts.Length);

        if (Keyboard.current.tabKey.wasPressedThisFrame)
            SelectWeapon((currentIndex + 1) % weaponScripts.Length);
    }

    private void SelectWeapon(int index)
    {
        currentIndex = index;

        for (int i = 0; i < weaponScripts.Length; i++)
            weaponScripts[i].SetActive(i == index);

        for (int i = 0; i < weaponVisuals.Length; i++)
            weaponVisuals[i].SetActive(i == index);
    }
}