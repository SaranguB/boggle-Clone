using GameMode.BaseMode;
using GameMode.Tiles;
using UnityEngine;
using Utilities;

namespace Tile
{
    public class TilePool : GenericObjectPool<TileViewController>
    {
        private BaseModeSo modeData;
        private Transform parentTransform;
        
        public TilePool(BaseModeSo modeData, Transform  parentTransform = null)
        {
            this.modeData = modeData;
            this.parentTransform = parentTransform;
        }
        
        protected override TileViewController CreateItem()
        {
            return Object.Instantiate(modeData.tilePrefab, parentTransform);
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