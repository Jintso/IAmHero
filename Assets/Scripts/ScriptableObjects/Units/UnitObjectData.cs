namespace ScriptableObjects.Units
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Scriptable Objects/Unit Object Data")]
    public class UnitObjectData : ScriptableObject
    {
        public string UnitName;
        public float Damage;
        public float Health;
        public float MoveSpeed;
        public float AttackSpeed;
        public float Experience;
        public int Level;
        public GameObject EnemyPrefab;
    }
}