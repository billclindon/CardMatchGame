using UnityEngine;
using CardMatch.Match;
using CardMatch.BoardSystem;
using CardMatch.Cards;

namespace CardMatch.Audio
{
    public class SoundSystem : MonoBehaviour
    {
        [Header("Audio Clips")]
        [SerializeField] private AudioClip flipClip;
        [SerializeField] private AudioClip matchClip;
        [SerializeField] private AudioClip mismatchClip;
        [SerializeField] private AudioClip gameOverClip;

        [Header("References")]
        [SerializeField] private MatchResolver matchResolver;
        [SerializeField] private Board board;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            matchResolver.OnMatch += HandleMatch;
            matchResolver.OnMismatch += HandleMismatch;
            board.OnBoardCompleted += HandleGameOver;
        }

        private void OnDisable()
        {
            matchResolver.OnMatch -= HandleMatch;
            matchResolver.OnMismatch -= HandleMismatch;
            board.OnBoardCompleted -= HandleGameOver;
        }

        public void RegisterCard(Card card)
        {
            card.OnFlipRequested += HandleFlip;
        }

        public void UnregisterCard(Card card)
        {
            card.OnFlipRequested -= HandleFlip;
        }

        private void HandleFlip(Card card)
        {
            Play(flipClip);
        }

        private void HandleMatch(Card a, Card b)
        {
            Play(matchClip);
        }

        private void HandleMismatch(Card a, Card b)
        {
            Play(mismatchClip);
        }

        private void HandleGameOver()
        {
            Play(gameOverClip);
        }

        private void Play(AudioClip clip)
        {
            if (clip == null)
                return;

            audioSource.PlayOneShot(clip);
        }
    }
}