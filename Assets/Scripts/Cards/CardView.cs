using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CardMatch.Cards
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [SerializeField] private float flipDuration = 0.2f;

        private Card card;
        private Coroutine flipRoutine;
        private bool isFrontVisible;

        public void Initialize(Card owner)
        {
            card = owner;
            ResetVisual();
        }

        public void SetFrontSprite(Sprite sprite)
        {
            if (frontImage != null)
                frontImage.sprite = sprite;
        }

        public void PlayFlip(bool showFront)
        {
            if (flipRoutine != null)
                StopCoroutine(flipRoutine);

            flipRoutine = StartCoroutine(FlipAnimation(showFront));
        }

        private IEnumerator FlipAnimation(bool showFront)
        {
            float halfDuration = flipDuration * 0.5f;
            float time = 0f;

            while (time < halfDuration)
            {
                time += Time.deltaTime;
                float scale = Mathf.Lerp(1f, 0f, time / halfDuration);
                transform.localScale = new Vector3(scale, 1f, 1f);
                yield return null;
            }

            isFrontVisible = showFront;
            frontImage.gameObject.SetActive(isFrontVisible);
            backImage.gameObject.SetActive(!isFrontVisible);

            time = 0f;

            while (time < halfDuration)
            {
                time += Time.deltaTime;
                float scale = Mathf.Lerp(0f, 1f, time / halfDuration);
                transform.localScale = new Vector3(scale, 1f, 1f);
                yield return null;
            }

            transform.localScale = Vector3.one;
            flipRoutine = null;
        }

        public void DisableVisual()
        {
            // gameObject.SetActive(false);
            if (frontImage != null)
                frontImage.gameObject.SetActive(false);

            if (backImage != null)
                backImage.gameObject.SetActive(false);
        }

        public void ResetVisual()
        {
            transform.localScale = Vector3.one;
            isFrontVisible = false;

            if (frontImage != null)
                frontImage.gameObject.SetActive(false);

            if (backImage != null)
                backImage.gameObject.SetActive(true);
        }
    }
}