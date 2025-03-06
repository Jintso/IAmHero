using UnityEngine;

namespace ScriptableObjects.Abilities
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability Object Data")]
    public class AbilityObjectData : ScriptableObject
    {
        public string Name;
        public float Damage;
        public float Speed;
        public float Rate;
        public float Area;
        public int Quantity;
        public Sprite Sprite;
    }
}
