using System;
using UnityEngine;

namespace CardMatch.Cards
{
    [RequireComponent(typeof(CardView))]
    public class Card : MonoBehaviour
    {
        public event Action<Card> OnFlipRequested;

        [SerializeField] private CardData cardData;

        private CardView cardView;
        private CardState currentState = CardState.FaceDown;

        public CardData Data => cardData;
        public CardState State => currentState;

        private void Awake()
        {
            cardView = GetComponent<CardView>();
            cardView.Initialize(this);
        }

        public void SetData(CardData data)
        {
            cardData = data;
            cardView.SetFrontSprite(cardData.FrontSprite);
        }

        public void RequestFlip()
        {
            if (currentState != CardState.FaceDown)
                return;

            OnFlipRequested?.Invoke(this);
        }

        public void FlipUp()
        {
            if (currentState != CardState.FaceDown)
                return;

            currentState = CardState.FaceUp;
            cardView.PlayFlip(true);
        }

        public void FlipDown()
        {
            if (currentState != CardState.FaceUp)
                return;

            currentState = CardState.FaceDown;
            cardView.PlayFlip(false);
        }

        public void SetMatched()
        {
            currentState = CardState.Matched;
            cardView.DisableVisual();
        }

        public void ResetState()
        {
            currentState = CardState.FaceDown;
            cardView.ResetVisual();
        }
    }
}