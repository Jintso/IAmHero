namespace ScriptableObjects.Sound
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Scriptable Objects/Sound Object Data")]
    public class SoundObjectData : ScriptableObject
    {
        public AudioClip MenuTheme;
        public AudioClip[] GameplayTheme;
        public AudioClip[] EndingTheme;
        public AudioClip BossTheme;
    }
}