namespace ScriptableObjects.Vfx
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Scriptable Objects/Vfx Object Data")]
    public class VfxObjectData : ScriptableObject
    {
        public AudioClip AudioClip;
        public GameObject Prefab;
    }
}