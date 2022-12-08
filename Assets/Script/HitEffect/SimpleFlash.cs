using System.Collections;
using UnityEngine;

namespace BarthaSzabolcs.Tutorial_SpriteFlash
{
    public class SimpleFlash : MonoBehaviour
    {
        [SerializeField] private Material _flashMaterial;
        [SerializeField] private float _duration;

        private SpriteRenderer _spriteRenderer;
        private Material _originalMaterial;
        private Coroutine _flashRoutine;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalMaterial = _spriteRenderer.material;
        }


        public void Flash()
        {
            if (_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
            }

            _flashRoutine = StartCoroutine(_FlashRoutine());
        }

        private IEnumerator _FlashRoutine()
        {
            _spriteRenderer.material = _flashMaterial;

            yield return new WaitForSeconds(_duration);

            _spriteRenderer.material = _originalMaterial;

            _flashRoutine = null;
        }
    }
}