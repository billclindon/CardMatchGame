using System;
using UnityEngine;
using CardMatch.Match;
using CardMatch.Cards;

namespace CardMatch.Score
{
    public class ScoreSystem : MonoBehaviour
    {
        public event Action<int> OnScoreChanged;
        public event Action<int> OnComboChanged;

        [SerializeField] private int baseMatchScore = 10;
        [SerializeField] private MatchResolver matchResolver;

        private int currentScore;
        private int currentCombo;

        public int CurrentScore => currentScore;
        public int CurrentCombo => currentCombo;

        private void OnEnable()
        {
            matchResolver.OnMatch += HandleMatch;
            matchResolver.OnMismatch += HandleMismatch;
        }

        private void OnDisable()
        {
            matchResolver.OnMatch -= HandleMatch;
            matchResolver.OnMismatch -= HandleMismatch;
        }

        private void HandleMatch(Card a, Card b)
        {
            currentCombo++;
            int scoreToAdd = baseMatchScore * currentCombo;
            currentScore += scoreToAdd;

            OnComboChanged?.Invoke(currentCombo);
            OnScoreChanged?.Invoke(currentScore);
        }

        private void HandleMismatch(Card a, Card b)
        {
            currentCombo = 0;
            OnComboChanged?.Invoke(currentCombo);
        }

        public void ResetScore()
        {
            currentScore = 0;
            currentCombo = 0;

            OnScoreChanged?.Invoke(currentScore);
            OnComboChanged?.Invoke(currentCombo);
        }
        public void Restore(int score, int combo)
        {
            currentScore = score;
            currentCombo = combo;

            OnScoreChanged?.Invoke(currentScore);
            OnComboChanged?.Invoke(currentCombo);
        }
    }
}