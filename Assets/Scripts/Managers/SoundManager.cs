namespace Managers
{
    using System;
    using ScriptableObjects.Sound;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class SoundManager : MonoBehaviour
    {
        private const float MusicVolume = 1.1f;

        public static SoundManager Instance;

        [SerializeField] private SoundObjectData soundObjectData;

        private bool _isAActive;

        [NonSerialized] private AudioSource _musicSourceA;
        [NonSerialized] private AudioSource _musicSourceB;
        [NonSerialized] private AudioClip _targetAudioClip;

        private AudioSource ActiveMusicSource => _isAActive ? _musicSourceA : _musicSourceB;
        private AudioSource FadingMusicSource => _isAActive ? _musicSourceB : _musicSourceA;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            _musicSourceA = CreateMusicSource("Music Source A");
            _musicSourceB = CreateMusicSource("Music Source B");

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // TODO: Get from player settings
            //_musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

            WorldManager.Instance.OnMainMenu += WorldManagerOnMainMenu;
            WorldManager.Instance.OnGameplay += WorldManagerOnGameplay;
            WorldManager.Instance.OnGameOver += WorldManagerOnGameOver;
        }

        private void Update()
        {
            LerpMusic(ActiveMusicSource, ActiveMusicSource.clip ? MusicVolume : 0, 1);
            LerpMusic(FadingMusicSource, 0, 1);

            if (_targetAudioClip == false || _targetAudioClip == ActiveMusicSource.clip)
            {
                return;
            }

            if (_targetAudioClip == FadingMusicSource.clip)
            {
                SwapMusic();
            }
            else if (FadingMusicSource.volume < 0.01f)
            {
                SwapMusic();
            }
        }

        private void ChangeMusic(AudioClip newClip)
        {
            _targetAudioClip = newClip;
        }

        public void SilenceMusic()
        {
            _targetAudioClip = null;
        }

        private void SwapMusic()
        {
            _isAActive = !_isAActive;
            ActiveMusicSource.clip = _targetAudioClip;
            ActiveMusicSource.Play();
        }

        private static void LerpMusic(AudioSource src, float targetVolume, float speed)
        {
            var diff = src.volume - targetVolume;

            if (diff == 0)
            {
                return;
            }

            var portion = Mathf.Abs(speed * Time.deltaTime / diff);

            src.volume = Mathf.Lerp(src.volume, targetVolume, portion);
        }

        private AudioSource CreateMusicSource(string gameObjectName)
        {
            var src = new GameObject(gameObjectName).AddComponent<AudioSource>();
            src.transform.SetParent(transform);
            src.volume = 0;
            src.loop = true;

            return src;
        }

        private void WorldManagerOnMainMenu(object sender, EventArgs e)
        {
            ChangeMusic(soundObjectData.MenuTheme);
        }

        private void WorldManagerOnGameplay(object sender, EventArgs e)
        {
            ChangeMusic(soundObjectData.GameplayTheme[Random.Range(0, soundObjectData.GameplayTheme.Length)]);
        }

        private void WorldManagerOnGameOver(object sender, EventArgs e)
        {
            ChangeMusic(soundObjectData.EndingTheme[Random.Range(0, soundObjectData.EndingTheme.Length)]);
        }

        private static void PlayRandomSound(AudioClip[] clipArray, Vector3 position, float volume = 1f)
        {
            PlaySound(clipArray[Random.Range(0, clipArray.Length)], position, volume);
        }

        private static void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}