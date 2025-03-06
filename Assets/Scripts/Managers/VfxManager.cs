namespace Managers
{
    using System;
    using ScriptableObjects.Vfx;
    using UnityEngine;

    public class VfxManager : MonoBehaviour
    {
        [SerializeField] public VfxObjectData swordTrailVfxData;
        [SerializeField] public VfxObjectData healingVfxData;
        [SerializeField] public VfxObjectData levelUpVfxData;

        [NonSerialized] public GameObject HealingVfx;
        [NonSerialized] public GameObject LevelUpVfx;
        [NonSerialized] public GameObject SwordTrailVfx;

        public static VfxManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SwordTrailVfx = Instantiate(swordTrailVfxData.Prefab, Vector3.zero, Quaternion.identity, transform);
            SwordTrailVfx.SetActive(false);

            HealingVfx = Instantiate(healingVfxData.Prefab, Vector3.zero, Quaternion.identity, transform);
            HealingVfx.SetActive(false);

            LevelUpVfx = Instantiate(levelUpVfxData.Prefab, Vector3.zero, Quaternion.identity, transform);
            LevelUpVfx.SetActive(false);
        }

        public static void RunVfx(GameObject vfxEffect, Transform transform)
        {
            vfxEffect.transform.position = transform.position;
            vfxEffect.transform.parent = transform;
            vfxEffect.SetActive(true);
        }
    }
}