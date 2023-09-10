using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Responsive_UI.View
{
    public class AnimatedWindowContainingItems : AnimatedWindow
    {
        [field: SerializeField] public List<ItemCellView> Cells { get; set; }
    }
}
