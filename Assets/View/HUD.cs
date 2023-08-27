using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private InventoryWindow _inventoryWindow;
        [SerializeField] private PauseWindow _pauseWindow;

        private readonly List<IWindow> _openedWindows = new();

        private InputActionMap _defaultInputActionMap;
        private InputActionMap _inventoryInputActionMap;
        private InputActionMap _pauseInputActionMap;
        
        private const string HUDInputActionMapName = "HUD";
        private const string InventoryInputActionMapName = "Inventory Window";
        private const string PauseInputActionMapName = "Pause Window";
        
        private void Awake()
        {
            _defaultInputActionMap = _playerInput.actions.FindActionMap(HUDInputActionMapName);
            _inventoryInputActionMap = _playerInput.actions.FindActionMap(InventoryInputActionMapName);
            _pauseInputActionMap = _playerInput.actions.FindActionMap(PauseInputActionMapName);
        }

        public void OpenInventoryWindowInputAction(InputAction.CallbackContext context)
        {
            if (context.performed == false) 
                return;

            OpenInventoryWindow();

            _playerInput.currentActionMap = _inventoryInputActionMap;
        }

        public void OpenInventoryWindow()
        {
            if (_openedWindows.Count > 0)
                return;
            
            if (_inventoryWindow.IsOpened)
                return;

            _inventoryWindow.Open();

            _openedWindows.Add(_inventoryWindow);
        }

        public void OpenPauseWindowInputAction(InputAction.CallbackContext context)
        {
            if (_openedWindows.Count > 0)
                return;
            
            if (context.performed == false) 
                return;

            OpenPauseWindow();

            _playerInput.currentActionMap = _pauseInputActionMap;
        }

        private void OpenPauseWindow()
        {
            if (_pauseWindow.IsOpened)
                return;

            _pauseWindow.Open();

            _openedWindows.Add(_pauseWindow);
        }

        public void CloseWindowInputAction(InputAction.CallbackContext context)
        {
            if (context.performed == false) 
                return;

            CloseWindow();
        }

        public void CloseWindow()
        {
            if (_openedWindows.Count <= 0) 
                return;
            
            if (_openedWindows[^1].IsOpened == false)
                return;
            
            _openedWindows[^1].Close();
            _openedWindows.Remove(_openedWindows[^1]);
            
            _playerInput.currentActionMap = _defaultInputActionMap;
        }
    }
}