using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.DestructibleObject
{
    public class DestructibleObject : MonoBehaviour
    {
        public string ObjectName = string.Empty;
        public DestructibleObject MeinObject;
        public bool IsMain;

        public List<Element> elements = new List<Element>();
        public Dictionary<string, float> damagedElements = new Dictionary<string, float>();
        private readonly List<DestructibleObject> childObjectDestructionScripts = new List<DestructibleObject>();

        public List<Collider> Colliders = new List<Collider>();

        public bool Indestructible = false;
        public float DurabilityMultiplier = 1f;
        public float TotalDestructionPercentage = 1f;
        public bool Destroyed;

        [SerializeField] public float DestructionLevel = 0f;

        public float LocalDestructionLevel;

        [Serializable]
        public class Element
        {
            public string Name;
            public bool Destroyed { get; private set; }
            public float DestructionLevel { get; private set; }

            [Range(0f, 99999f)] public float MinEnergy;
            [Range(0f, 99999f)] public float MaxEnergy;

            [Range(0f, 1f)] public float MaxObjectDestructionAmount;

            public List<Collider> Colliders = new List<Collider>();
            public List<Action> Actions = new List<Action>();

            public Element()
            {
                DestructionLevel = 0f;
            }

            public float ApplyDestruction(float energy, float resistance)
            {
                float objectDestructionAmount = 0f;

                if (!Destroyed)
                {
                    if (energy >= MinEnergy)
                    {
                        float maxEnergyWithResistance = MaxEnergy * resistance;

                        if (energy > maxEnergyWithResistance)
                        {
                            energy = maxEnergyWithResistance;
                        }

                        float destructionAmount = Mathf.Clamp(energy / maxEnergyWithResistance, 0f, 1f - DestructionLevel);
                        DestructionLevel += destructionAmount;
                        if (DestructionLevel == 1f)
                        {
                            Destroyed = true;
                        }

                        objectDestructionAmount = destructionAmount * MaxObjectDestructionAmount;
                    }
                }

                return objectDestructionAmount;
            }

            public void Destroy()
            {
                ApplyDestruction(MaxEnergy, 1f);
            }

            public void ResetElementState()
            {
                Destroyed = false;
                DestructionLevel = 0f;

                foreach (var Action in Actions)
                {
                    if (Action != null)
                    {
                        Action.Performed = false;
                    }
                }
            }

        }

        [Serializable]
        public class Action : ICloneable
        {
            public bool Performed { get; internal set; }

            public object Clone()
            {
                throw new NotImplementedException();
            }
        }
    }
}
