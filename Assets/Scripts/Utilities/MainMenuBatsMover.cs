namespace Utilities
{
    using System.Collections.Generic;
    using DG.Tweening;
    using UnityEngine;

    public class MainMenuBatsMover : MonoBehaviour
    {
        [SerializeField] private GameObject bats;
        [SerializeField] private Transform[] batsPath;

        private void Start()
        {
            var batsPathList = new List<Vector3>
            {
                batsPath[0].position,
                batsPath[1].position,
            };

            bats.transform.DOPath(batsPathList.ToArray(), 10f).SetLoops(-1, LoopType.Restart);
        }

        private void OnDestroy()
        {
            _ = bats.transform.DOKill(true);
        }
    }
}