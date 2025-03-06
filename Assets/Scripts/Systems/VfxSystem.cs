namespace Systems
{
    using CartoonFX;
    using UnityEngine;

    public class VfxSystem : MonoBehaviour
    {
        public void PlayVfx()
        {
            var ps = GetComponent<ParticleSystem>();
            if (ps.isEmitting)
            {
                ps.Stop(true);
            }
            else
            {
                if (!gameObject.activeSelf)
                {
                    transform.gameObject.SetActive(true);
                }
                else
                {
                    ps.Play(true);
                    var pluginEffects = transform.GetComponentsInChildren<CFXR_Effect>();
                    foreach (var effect in pluginEffects)
                    {
                        effect.ResetState();
                    }
                }
            }
        }
    }
}