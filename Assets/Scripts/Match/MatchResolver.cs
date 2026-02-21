using System;
using System.Collections;
using UnityEngine;
using CardMatch.Cards;

namespace CardMatch.Match
{
    public class MatchResolver : MonoBehaviour
    {
        public event Action<Card, Card> OnMatch;
        public event Action<Card, Card> OnMismatch;
        public event Action OnResolutionComplete;

        [SerializeField] private float resolveDelay = 1f;

        private MatchResolverState currentState = MatchResolverState.Idle;

        private Card firstSelected;
        private Card secondSelected;
        private Coroutine resolveRoutine;
        private bool isMatchPending;

        public MatchResolverState State => currentState;

        public void RegisterCard(Card card)
        {
            card.OnFlipRequested += HandleFlipRequest;
        }

        public void UnregisterCard(Card card)
        {
            card.OnFlipRequested -= HandleFlipRequest;
        }

        private void HandleFlipRequest(Card card)
        {
            if (card.State != CardState.FaceDown)
                return;

            switch (currentState)
            {
                case MatchResolverState.Idle:
                    firstSelected = card;
                    firstSelected.FlipUp();
                    currentState = MatchResolverState.OneSelected;
                    break;

                case MatchResolverState.OneSelected:
                    secondSelected = card;
                    secondSelected.FlipUp();
                    EvaluatePair();
                    break;

                case MatchResolverState.Resolving:
                    InterruptAndProcess(card);
                    break;
            }
        }

        private void EvaluatePair()
        {
            if (firstSelected.Data.Id == secondSelected.Data.Id)
            {
                isMatchPending = true;
                OnMatch?.Invoke(firstSelected, secondSelected);
            }
            else
            {
                isMatchPending = false;
                OnMismatch?.Invoke(firstSelected, secondSelected);
            }

            currentState = MatchResolverState.Resolving;
            resolveRoutine = StartCoroutine(ResolveAfterDelay());
        }

        private IEnumerator ResolveAfterDelay()
        {
            yield return new WaitForSeconds(resolveDelay);
            ResolvePair();
        }

        private void ResolvePair()
        {
            if (isMatchPending)
            {
                firstSelected.SetMatched();
                secondSelected.SetMatched();
            }
            else
            {
                firstSelected.FlipDown();
                secondSelected.FlipDown();
            }

            ClearSelection();
            OnResolutionComplete?.Invoke();
        }

        private void InterruptAndProcess(Card newCard)
        {
            if (resolveRoutine != null)
            {
                StopCoroutine(resolveRoutine);
                resolveRoutine = null;
            }

            ResolvePair();

            if (newCard.State == CardState.FaceDown)
            {
                firstSelected = newCard;
                firstSelected.FlipUp();
                currentState = MatchResolverState.OneSelected;
            }
        }

        private void ClearSelection()
        {
            firstSelected = null;
            secondSelected = null;
            currentState = MatchResolverState.Idle;
        }

        public void ForceResolveIfPending()
        {
            if (currentState == MatchResolverState.Resolving)
            {
                if (resolveRoutine != null)
                {
                    StopCoroutine(resolveRoutine);
                    resolveRoutine = null;
                }

                ResolvePair();
            }
        }
    }
}