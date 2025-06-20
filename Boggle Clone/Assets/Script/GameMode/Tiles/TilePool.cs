using GameMode.EndlessMode;
using GameMode.Tiles;
using UnityEngine;
using Utilities;

namespace Tile
{
    public class TilePool : GenericObjectPool<TileViewController>
    {
        private EndlessModeSo endlessModeData;
        private Transform parentTransform;
        
        public TilePool(EndlessModeSo endlessModeData, Transform  parentTransform = null)
        {
            this.endlessModeData = endlessModeData;
            this.parentTransform = parentTransform;
        }
        
        protected override TileViewController CreateItem()
        {
            return Object.Instantiate(endlessModeData.tilePrefab, parentTransform);
        }

        protected override void OnGet(TileViewController item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnRelease(TileViewController item)
        {
            item.gameObject.SetActive(false);
        }
        
    }
}