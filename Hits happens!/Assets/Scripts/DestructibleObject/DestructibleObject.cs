using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.DestructibleObject
{
    public class DestructibleObject : MonoBehaviour
    {
        public string ObjectName = string.Empty;
        public GameObject MeinObject = null;

        [HideInInspector]
        public List<GameObject> ConnectedObjects = null;
        public List<Collider> BallisticsColliders = new List<Collider>();

        protected virtual void Awake()
        {
            AddConnectedObjectsToList();
            FindBallisticsColliders();
        }

        private void AddConnectedObjectsToList()
        {
            if (MeinObject == null)
            {
                return;
            }

            ConnectedObjects = new List<GameObject>();

            foreach (Transform child in transform)
            {
                if (child.gameObject != MeinObject)
                {
                    ConnectedObjects.Add(child.gameObject);
                }
            }
        }

        private void FindBallisticsColliders()
        {
            throw new NotImplementedException();
        }
    }
}
