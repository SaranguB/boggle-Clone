using System;
using System.Collections.Generic;

namespace Utilities
{
    public abstract class GenericObjectPool<T> where T : class
    {
        public List<PooledItem> pooledItems = new();

        public virtual T GetItem() 
        {
            foreach (var item in pooledItems)
            {
                if (!item.isUsed)
                {
                    item.isUsed = true;
                    OnGet(item.Item);
                    return item.Item;
                }
            }
            
            var newItem = CreateItem();
            pooledItems.Add(new PooledItem { Item = newItem, isUsed = true });
            OnGet(newItem);
            return newItem;
        }

        public virtual void ReturnItem(T item)
        {
            var pooledItem = pooledItems.Find(i => ReferenceEquals(i.Item, item));
            if (pooledItem != null)
            {
                pooledItem.isUsed = false;
                OnRelease(item);
            }
        }

        protected abstract T CreateItem();
        
        protected virtual void OnGet(T item){}

        protected virtual void OnRelease(T item) { }

        public class PooledItem
        {
            public T Item;
            public bool isUsed;
        }
    }
}