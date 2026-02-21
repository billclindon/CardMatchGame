using System.IO;
using UnityEngine;
using CardMatch.BoardSystem;
using CardMatch.Match;
using CardMatch.Score;
using CardMatch.Cards;

namespace CardMatch.SaveLoad
{
    public class SaveLoadSystem : MonoBehaviour
    {
        [SerializeField] private Board board;
        [SerializeField] private MatchResolver matchResolver;
        [SerializeField] private ScoreSystem scoreSystem;

        private string SaveKey = "gameplaydata";//=> Path.Combine(Application.persistentDataPath, "save.json");
        private void Start()
        {
            LoadGame();
        }

        private void OnEnable()
        {
            matchResolver.OnResolutionComplete += SaveGame;
        }

        private void OnDisable()
        {
            matchResolver.OnResolutionComplete -= SaveGame;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                matchResolver.ForceResolveIfPending();
                SaveGame();
            }
        }

        private void OnApplicationQuit()
        {
            matchResolver.ForceResolveIfPending();
            SaveGame();
        }

        public void SaveGame()
        {
            SaveData data = new SaveData
            {
                preset = board.Preset,
                seed = board.Seed,
                score = scoreSystem.CurrentScore,
                combo = scoreSystem.CurrentCombo,
                matchedCount = board.matchedCount
            };

            var cards = board.GetActiveCards();

            for (int i = 0; i < cards.Count; i++)
            {
                data.cards.Add(new CardStateData
                {
                    index = i,
                    dataId = cards[i].Data.Id,
                    state = (int)cards[i].State
                });
            }

            string json = JsonUtility.ToJson(data, true);
            // File.WriteAllText(SavePath, json);
            PlayerPrefs.SetString(SaveKey, json);
        }

        public void LoadGame()
        {
            if (!PlayerPrefs.HasKey(SaveKey))//if (!File.Exists(SavePath))
            {
                Debug.Log("no player pref exists");
                return;
            }

            string json = PlayerPrefs.GetString(SaveKey);//File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            board.GenerateBoardFromSave(data);

            scoreSystem.ResetScore();
            scoreSystem.Restore(data.score, data.combo);
        }
    }
}