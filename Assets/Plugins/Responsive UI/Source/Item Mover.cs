using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.Responsive_UI.Models;
using Plugins.Responsive_UI.View;
using UnityEngine;
using UnityEngine.InputSystem;
using Cursor = Plugins.Responsive_UI.View.Cursor;

namespace Plugins.Responsive_UI.Source
{
    public class ItemMover : MonoBehaviour
    {
        public MovingItem MovingItem { get; } = new();
        [field:SerializeField] public List<AnimatedWindowContainingItems> Windows { get; set; }

        [SerializeField] private Cursor _cursor;
        [SerializeField] private Camera _camera;
        [SerializeField] private InputAction _leftMouseButtonInputAction;
        [SerializeField] private InputAction _rightMouseButtonInputAction;

        private void Start()
        {
            _leftMouseButtonInputAction.Enable();
            _rightMouseButtonInputAction.Enable();
        }

        private void Update()
        {
            if (_leftMouseButtonInputAction.triggered)
            {
                var closestCell = GetClosestItemCell();

                if (closestCell == null)
                    return;

                if (MovingItem.Definition is null)
                {
                    TakeAllItems(closestCell);
                }
                else
                {
                    PullAllItems(closestCell);
                }
            }

            if (_rightMouseButtonInputAction.triggered)
            {
                var closestCell = GetClosestItemCell();

                if (closestCell == null)
                    return;

                if (MovingItem.Definition is null)
                {
                    TakeHalfItems(closestCell);
                }
                else
                {
                    PutOneItem(closestCell);
                }
            }
        }

        private void TakeAllItems(ItemCellView closestCell)
        {
            if (closestCell.Definition is null)
                return;

            MovingItem.Definition = closestCell.Definition;
            MovingItem.Quantity = closestCell.Quantity;
            closestCell.Clear();
            _cursor.Show(MovingItem.Definition);
        }

        private void TakeHalfItems(ItemCellView closestCell)
        {
            if (closestCell.Definition is null && closestCell.Definition == MovingItem.Definition == false) 
                return;

            if (MovingItem.Quantity > 1)
            {
                var quantity = Convert.ToInt32(MovingItem.Quantity / 2);
                
                if (closestCell.Definition == MovingItem.Definition == false)
                {
                    closestCell.SetDefinition(MovingItem.Definition);
                    closestCell.Quantity = quantity;
                }
                else
                {
                    closestCell.Quantity += quantity;
                }

                MovingItem.Quantity -= quantity;
            }
            else
            {
                if (closestCell.Definition == MovingItem.Definition == false)
                {
                    closestCell.SetDefinition(MovingItem.Definition);
                    closestCell.Quantity = 1;
                }
                else
                {
                    closestCell.Quantity += 1;
                }

                MovingItem.Quantity = 0;
                MovingItem.Definition = null;
            }
        }

        private void PullAllItems(ItemCellView closestCell)
        {
            if (closestCell.Definition is not null) 
                return;
                    
            closestCell.SetDefinition(MovingItem.Definition);
            closestCell.Quantity = MovingItem.Quantity;
                
            MovingItem.Definition = null;
            MovingItem.Quantity = 0;
            _cursor.Hide();
        }

        private void PutOneItem(ItemCellView closestCell)
        {
            if (closestCell.Definition is not null && closestCell.Definition == MovingItem.Definition == false) 
                return;
            
            const int quantity = 1;

            if (closestCell.Definition == MovingItem.Definition == false)
            {
                closestCell.SetDefinition(MovingItem.Definition);
                closestCell.Quantity = quantity;
            }
            else
            {
                closestCell.Quantity += 1;
            }

            MovingItem.Quantity -= quantity;

            if (MovingItem.Quantity == 0)
                MovingItem.Definition = null;
        }

        private ItemCellView GetClosestItemCell()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            foreach (var window in Windows)
            {
                foreach (var cell in window.Cells.Where(cell => RectTransformUtility.RectangleContainsScreenPoint(cell.transform.GetComponent<RectTransform>(), mousePosition)))
                {
                    return cell;
                }
            }

            return null;
        }
        
        private void OnDestroy()
        {
            _leftMouseButtonInputAction.Disable();
            _rightMouseButtonInputAction.Disable();
        }
    }
    
    public class MovingItem
    {
        public ItemDefinition Definition { get; set; }
        public int Quantity { get; set; }
    }
}
