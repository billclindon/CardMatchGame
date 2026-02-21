using UnityEngine;
using CardMatch.BoardSystem;
using CardMatch.Match;
using CardMatch.Score;
using CardMatch.Core;

namespace CardMatch.Core
{
    public class GameController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Board board;
        [SerializeField] private MatchResolver matchResolver;
        [SerializeField] private ScoreSystem scoreSystem;
        [SerializeField] private InputController inputController;

        [Header("Game Over UI")]
        [SerializeField] private GameObject gameOverPanel;

        private bool isGameOver;

        private void OnEnable()
        {
            board.OnBoardCompleted += HandleBoardCompleted;
        }

        private void OnDisable()
        {
            board.OnBoardCompleted -= HandleBoardCompleted;
        }

        private void HandleBoardCompleted()
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            inputController.enabled = false;
        }

        public void OnReplayPressed()
        {
            if (!isGameOver)
                return;

            isGameOver = false;

            gameOverPanel.SetActive(false);

            scoreSystem.ResetScore();
            matchResolver.ForceResolveIfPending();
            board.GenerateBoard();

            inputController.enabled = true;
        }
    }
}