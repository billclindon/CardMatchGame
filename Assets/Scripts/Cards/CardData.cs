using UnityEngine;

namespace CardMatch.Cards
{
    [CreateAssetMenu(menuName = "CardMatch/Card Data", fileName = "CardData")]
    public class CardData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private Sprite frontSprite;

        public int Id => id;
        public Sprite FrontSprite => frontSprite;
    }
}