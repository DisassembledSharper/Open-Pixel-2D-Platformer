using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class MobileControlsManager : MonoBehaviour
    {
        [SerializeField] private UserSettings playerSettings;
        [SerializeField] private GameObject joystick;
        [SerializeField] private GameObject dpad;

        private void Update()
        {
            if (playerSettings.Control == 0)
            {
                dpad.SetActive(true);
                joystick.SetActive(false);
            }
            else if (playerSettings.Control == 1)
            {
                dpad.SetActive(false);
                joystick.SetActive(true);
            }
        }
    }
}