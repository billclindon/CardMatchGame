using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CardMatch.Cards;
using CardMatch.Config;
using CardMatch.Match;
using Unity.VisualScripting;

namespace CardMatch.BoardSystem
{
    public class Board : MonoBehaviour
    {
        public event Action OnBoardCompleted;

        [Header("Board Config")]
        [SerializeField] private BoardPreset preset;
        [SerializeField] private Transform cardParent;
        [SerializeField] private Card cardPrefab;
        [SerializeField] private List<CardData> availableCardData;

        [Header("References")]
        [SerializeField] private MatchResolver matchResolver;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private readonly List<Card> activeCards = new List<Card>();

        private int seed;
        private int totalMatchesRequired;
        [HideInInspector] public int matchedCount;

        public int Seed => seed;
        public BoardPreset Preset => preset;

        private void Awake()
        {
            GenerateBoard();
        }

        public void GenerateBoard()
        {
            ClearBoard();

            seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            System.Random rng = new System.Random(seed);
            preset += 1;
            if(preset > BoardPreset.XL_5x6)
            {
                preset = BoardPreset.Small_2x2;
            }

            (int rows, int columns) = GetDimensions(preset);

            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = columns;

            int totalCards = rows * columns;
            totalMatchesRequired = totalCards / 2;
            matchedCount = 0;

            List<CardData> selectedPairs = GeneratePairs(totalMatchesRequired, rng);
            Shuffle(selectedPairs, rng);

            foreach (CardData data in selectedPairs)
            {
                Card cardInstance = Instantiate(cardPrefab, cardParent);
                cardInstance.SetData(data);

                activeCards.Add(cardInstance);
                matchResolver.RegisterCard(cardInstance);
            }

            matchResolver.OnMatch += HandleMatch;
        }

        private void HandleMatch(Card a, Card b)
        {
            matchedCount++;
            Debug.Log("Match found: " + a.name + " and " + b.name + " matchedCount: " + matchedCount + "/" + totalMatchesRequired);

            if (matchedCount >= totalMatchesRequired)
            {
                OnBoardCompleted?.Invoke();
            }
        }

        private List<CardData> GeneratePairs(int pairCount, System.Random rng)
        {
            List<CardData> pool = new List<CardData>(availableCardData);
            Shuffle(pool, rng);

            List<CardData> result = new List<CardData>();

            for (int i = 0; i < pairCount; i++)
            {
                CardData data = pool[i % pool.Count];
                result.Add(data);
                result.Add(data);
            }

            return result;
        }

        private void Shuffle<T>(List<T> list, System.Random rng)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                (list[i], list[swapIndex]) = (list[swapIndex], list[i]);
            }
        }

        private (int, int) GetDimensions(BoardPreset preset)
        {
            return preset switch
            {
                BoardPreset.Small_2x2 => (2, 2),
                BoardPreset.Medium_2x3 => (2, 3),
                BoardPreset.Large_4x4 => (4, 4),
                BoardPreset.XL_5x6 => (5, 6),
                _ => (2, 2)
            };
        }

        private void ClearBoard()
        {
            foreach (Card card in activeCards)
            {
                matchResolver.UnregisterCard(card);
                Destroy(card.gameObject);
            }
            matchResolver.OnMatch -= HandleMatch;
            activeCards.Clear();
        }
        public List<Card> GetActiveCards()
        {
            return activeCards;
        }

        public void GenerateBoardFromSave(CardMatch.SaveLoad.SaveData data)
        {
            ClearBoard();

            preset = data.preset;
            seed = data.seed;

            System.Random rng = new System.Random(seed);

            (int rows, int columns) = GetDimensions(preset);

            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = columns;

            int totalCards = rows * columns;
            totalMatchesRequired = totalCards / 2;
            matchedCount = data.matchedCount;

            List<CardData> selectedPairs = GeneratePairs(totalMatchesRequired, rng);
            Shuffle(selectedPairs, rng);

            for (int i = 0; i < selectedPairs.Count; i++)
            {
                Card cardInstance = Instantiate(cardPrefab, cardParent);
                cardInstance.SetData(selectedPairs[i]);

                activeCards.Add(cardInstance);
                matchResolver.RegisterCard(cardInstance);

                var savedState = data.cards[i];

                if (savedState.state == (int)CardState.Matched)
                {
                    cardInstance.SetMatched();
                }
            }
            matchResolver.OnMatch += HandleMatch;
            if (matchedCount >= totalMatchesRequired)
            {
                OnBoardCompleted?.Invoke();
            }
        }
    }
}