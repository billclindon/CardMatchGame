using System;
using System.Collections.Generic;
using CardMatch.Config;

namespace CardMatch.SaveLoad
{
    [Serializable]
    public class SaveData
    {
        public BoardPreset preset;
        public int seed;
        public int score;
        public int combo;
        public int matchedCount;

        public List<CardStateData> cards = new();
    }

    [Serializable]
    public class CardStateData
    {
        public int index;
        public int dataId;
        public int state;
    }
}