using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;

    public static List<Card> cardList = new List<Card>();

    public static List<Card> starterDeck = new List<Card>();
    private int[] starterCards = { 0, 0, 1, 1, 3, 3, 4, 4, 2, 5 };

    public static List<Card> tier1Starter = new List<Card>();
    public static List<Card> tier1Basic = new List<Card>();
    public static List<Card> tier1Special = new List<Card>();
    public static List<Card> tier1Skills = new List<Card>();

    public static int tier1BasicChance = 50;
    public static int tier1SpecialChance = 30;
    public static int tier1SkillChance = 20;

    public static int tier1CardDrawRatio = tier1BasicChance + tier1SpecialChance + tier1SkillChance;

    void Awake()
    {

        Instance = this;

        //starter
        tier1Starter.Add(new Card(0, "White", "Starter", 1, 0, 1, 0, "Earn 1 mana", false, 0, 0));
        tier1Starter.Add(new Card(1, "Gold", "Starter", 1, 0, 0, 1, "Earn 1 gold", false, 0, 0));
        tier1Starter.Add(new Card(2, "Black", "Starter", 1, 1, 0, 0, "Draw 1 card", false, 0, 0));
        tier1Starter.Add(new Card(3, "White", "Starter", 2, 0, 1, 0, "Earn 1 mana", false, 0, 0));
        tier1Starter.Add(new Card(4, "Gold", "Starter", 2, 0, 0, 1, "Earn 1 gold", false, 0, 0));
        tier1Starter.Add(new Card(5, "Black", "Starter", 2, 1, 0, 0, "Draw 1 card", false, 0, 0));

        //tier 1
        //basic
        tier1Basic.Add(new Card(6, "White", "Summoning", 1, 1, 2, 0, "Draw 1 card and earn 2 mana", false, 0, 0));
        tier1Basic.Add(new Card(7, "Gold", "Summoning", 2, 1, 0, 2, "Draw 1 card and earn 2 gold", false, 0, 0));
        tier1Basic.Add(new Card(8, "Black", "Summoning", 2, 2, 0, 0, "Draw 2 card", false, 0, 0));
        tier1Basic.Add(new Card(9, "White", "Magical", 3, 0, 3, 0, "Earn 3 mana", false, 0, 0));
        tier1Basic.Add(new Card(10, "Gold", "Magical", 3, 0, 1, 2, "Earn 2 gold and 1 mana", false, 0, 0));
        tier1Basic.Add(new Card(11, "Black", "Magical", 2, 1, 2, 0, "Draw 1 card and earn 2 mana", false, 0, 0));
        tier1Basic.Add(new Card(12, "White", "Wealthy", 2, 0, 2, 1, "Earn 2 mana and 1 gold", false, 0, 0));
        tier1Basic.Add(new Card(13, "Gold", "Wealthy", 3, 0, 0, 3, "Earn 3 gold", false, 0, 0));
        tier1Basic.Add(new Card(14, "Black", "Wealthy", 1, 1, 0, 2, "Draw 1 card and earn 2 gold", false, 0, 0));

        //special
        tier1Special.Add(new Card(15, "White", "Ambitious", 3, 0, 2, 0, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 mana", false, 0, 0));
        tier1Special.Add(new Card(16, "Gold", "Ambitious", 3, 0, 0, 2, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 gold", false, 0, 0));
        tier1Special.Add(new Card(17, "Black", "Ambitious", 3, 1, 0, 0, "Every time this card is played first according to its priority, lower it's priority by 1 and permanently add +1 draw", false, 0, 0));
        tier1Special.Add(new Card(18, "White", "Jealous", 3, 0, 2, 0, "If the previous card played is white, draw 1 card", false, 0, 0));
        tier1Special.Add(new Card(19, "Gold", "Jealous", 3, 0, 1, 1, "If the previous card played is gold, draw 1 card", false, 0, 0));
        tier1Special.Add(new Card(20, "Black", "Jealous", 3, 0, 1, 1, "If the previous card played is black, draw 1 card", false, 0, 0));
        tier1Special.Add(new Card(21, "White", "Inspiring", 1, 0, 2, 0, "If the next card played is white, draw 1 card", false, 0, 0));
        tier1Special.Add(new Card(22, "Gold", "Inspiring", 1, 0, 1, 1, "If the next card played is gold, draw 1 card", false, 0, 0));
        tier1Special.Add(new Card(23, "Black", "Inspiring", 1, 1, 1, 0, "If the next card played is black, draw 1 card", false, 0, 0));
        tier1Special.Add(new Card(24, "White", "Skeptical", 2, 0, 2, 0, "If 10 card with a lower priority than this one have been played this turn, draw 2 card", false, 0, 0));
        tier1Special.Add(new Card(25, "Gold", "Skeptical", 2, 0, 0, 2, "If 10 card with a lower priority than this one have been played this turn, draw 2 card", false, 0, 0));
        tier1Special.Add(new Card(26, "Black", "Skeptical", 2, 0, 1, 1, "If 10 card with a lower priority than this one have been played this turn, draw 2 card", false, 0, 0));
        tier1Special.Add(new Card(27, "White", "Inquisitive", 1, 0, 2, 0, "Play one random white card from the deck with the same priority as this card<br>Plays nothing if no options available", false, 0, 0));
        tier1Special.Add(new Card(28, "Black", "Wise", 1, 0, 1, 0, "If your synergesis bar is 15 or higher when this card is played, draw a card", false, 0, 0));
        tier1Special.Add(new Card(29, "Colorless", "Medicinal", 1, 0, 0, 0, "Heal 1 heart and trash this card<br>Plays on draw<br>Still gives one meter charge<br>Counts as all colors", false, 0, 0));
        tier1Special.Add(new Card(30, "Gold", "Frugal", 3, 0, 0, 1, "Earn 1 gold for every card played after this one", false, 0, 0));

        //skills
        tier1Skills.Add(new Card(31, "Black", "Motivating", 3, 0, 1, 0, "Skill - Lower one card priority by 1 for your next turn", true, 2, 15));
        tier1Skills.Add(new Card(32, "Black", "Encouraging", 1, 0, 1, 0, "Skill - Raise one card priority by 1 for your next turn", true, 2, 15));
        tier1Skills.Add(new Card(33, "White", "Mystical", 2, 0, 0, 1, "Skill - Spend 40 mana to add 2 card to your starting hand next turn", true, 3, 40));
        tier1Skills.Add(new Card(34, "Gold", "Extravagant", 3, 0, 0, 1, "Skill - Spend up to 120 gold to add extra card to your starting hand in the next turn at 30 gold per card", true, 3, 10));
        tier1Skills.Add(new Card(35, "White", "Investigating", 2, 0, 1, 0, "Skill - Pick 1 card with a different priority than this card and put it into your starting hand on the next turn<br>Does not increase starting hand size", true, 3, 20));
        tier1Skills.Add(new Card(36, "White", "Speculative", 3, 0, 1, 0, "Skill - Add 1 extra card to your starting hand on the next turn", true, 3, 20));
        tier1Skills.Add(new Card(37, "Black", "Tidy", 2, 0, 0, 0, "Skill - Trash this card and another card from your deck and add 1 black starter card with priority 1", true, 0, 35));
        tier1Skills.Add(new Card(38, "Gold", "Thrifty", 1, 0, 0, 0, "Skill - Recieve 20 gold, but you must trash a card during the draft phase<br>Trashing costs 0 gold", true, 5, 25));
        tier1Skills.Add(new Card(39, "Colorless", "Neutral", 2, 0, 0, 0, "Skill - Skip the draft by not taking or trashing any card from your deck", true, 4, 30));

        cardList.AddRange(tier1Starter);
        cardList.AddRange(tier1Basic);
        cardList.AddRange(tier1Special);
        cardList.AddRange(tier1Skills);

        foreach (var x in starterCards)
        {
            starterDeck.Add(CardDatabase.cardList[x]);
        }
    }

    public Card DrawTier1Card()
    {
        Card tier1Card = new Card();

        int chance = Random.Range(1, tier1CardDrawRatio + 1);


        if ((chance -= tier1SkillChance) < 0)
        {
            tier1Card = CardDatabase.tier1Skills[Random.Range(0, tier1Skills.Count)];
        }
        else if ((chance -= tier1SpecialChance) < 0)
        {
            tier1Card = CardDatabase.tier1Special[Random.Range(0, tier1Special.Count)];
        }
        else
        {
            tier1Card = CardDatabase.tier1Basic[Random.Range(0, tier1Basic.Count)];
        }

        return tier1Card;
    }
}
