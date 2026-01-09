using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelKeyboard : MonoBehaviour
{
    public WeaponManager weaponManager;

    [Header("References")]
    public RectTransform wheel;

    [Header("Settings")]
    public int weaponCount = 3;
    public float rotationSpeed = 10f;

    private int currentIndex = 0;

    void Update()
    {
        HandleInput();
        RotateWheel();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeapon(-1);
        }
    }

    void ChangeWeapon(int direction)
    {
        currentIndex += direction;

        if (currentIndex < 0)
            currentIndex = weaponCount - 1;

        if (currentIndex >= weaponCount)
            currentIndex = 0;

        weaponManager.SetWeapon((WeaponType)currentIndex);
    }


    void RotateWheel()
    {
        float anglePerWeapon = 360f / weaponCount;
        float targetAngle = currentIndex * anglePerWeapon;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        wheel.rotation = Quaternion.Lerp(
            wheel.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
