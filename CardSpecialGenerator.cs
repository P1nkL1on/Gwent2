using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnSpecial
    {
        public static Special Swallow
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Swallow");
                spec.setSpecialAttributes(Tag.alchemy, Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyUnitInBattlefield()), s.QestionString());
                    if (t != null)
                        t.boost(s, 10);
                }, "Boost a unit by 10.");
                return spec;
            }
        }
        public static Special AlzursThunder
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Alzur's Thunder");
                spec.setSpecialAttributes(Tag.spell);
                spec.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyUnitInBattlefield()), s.QestionString());
                    if (t != null)
                        t.damage(s, 9);
                }, "Deal 9 damage.");
                return spec;
            }
        }
        public static Special StammelfordsTremor
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Stammelford's Tremor");
                spec.setSpecialAttributes(Tag.spell);
                spec.setOnDeploy((s, f) =>
                {
                    foreach (Unit t in Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s)))
                        t.damage(s, 1);
                }, "Deal 1 damage to all enemies.");
                return spec;
            }
        }
        public static Special WyvernScaleShield
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Wyvern Scale Shield");
                spec.setSpecialAttributes(Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    var possibleTargetsForBuff
                        = Select.Units(s.context.cards, Filter.anyUnitInBattlefield());
                    var possibleSourceOfBuff
                        = Select.Units(s.context.cards,
                            Filter.anyAllyUnitInHand(s),
                            Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver));
                    if (possibleSourceOfBuff.Count == 0 || possibleTargetsForBuff.Count == 0)
                        return; // no targets

                    Unit so = s.host.selectUnit(possibleSourceOfBuff, "Select a Bronze or Silver unit in your hand");
                    Unit to = s.host.selectUnit(possibleTargetsForBuff, "Select a unit to buff");

                    to.boost(s, so.basePower);

                }, "Boost a unit by the base power of a Bronze or Silver unit in your hand.");
                return spec;
            }
        }
        public static Special Reconnaissance
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Reconnaissance");
                spec.setSpecialAttributes(Tag.tactic);
                spec.setOnDeploy((s, f) =>
                {
                    List<Unit> randomBronzePair =
                        s.context._randomUnitFrom(
                            Select.Units(s.context.cards,
                            Filter.anyAllyUnitInDeck(s), 
                            Filter.anyUnitHasColor(Rarity.bronze)),
                            2);
                    if (randomBronzePair.Count == 0)
                        return;
                    s.host.playCard(
                        s.host.selectUnit(randomBronzePair, s.QestionString()));
                }, "Look at 2 random Bronze units in your deck, then play 1.");
                return spec;
            }
        }

        // hazzards
        public static Special TorrentialRain
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Torrential Rain");
                spec.setSpecialAttributes(Tag.hazzard);
                spec.setOnDeploy((s, f) =>
                {
                    Player enemy = s.host.chooseEnemy(s.context, HazzardQuestionPlayer(s.name));
                    int row = s.host.chooseEnemyRow(enemy, HazzardQuestionRow(s.name));

                    RowEffect rain = new RowEffect(s, enemy, row);
                    rain.SetBehaviour((r) => {
                        foreach (Unit t in s.context._randomUnitFrom(r.allRowUnits, 2))
                            t.damage(s, 1);
                    });

                }, "Apply a Hazard to an enemy row that deals 1 damage to 2 random units on turn start.");
                
                    
                
                return spec;
            }
        }
        static string HazzardQuestionPlayer(string name) { return String.Format("Select enemy player to apply {0}", name); }
        static string HazzardQuestionRow(string name) { return String.Format("Select enemy's row to apply {0}", name); }
    }
    class RowEffect {
        Card _source;
        Player _onPlayer;
        public Player PlayerUnderEffect { get { return _onPlayer; } }
        public int row;
        TriggerTurnRowEffect _onTurnStart;

        public RowEffect(Card source, Player player, int row)
        {
            this.row = row;
            _source = source;
            _onPlayer = player;
            _source.context._addRowEffect(this);
        }
        public void SetBehaviour(TriggerTurnRowEffect onTurnStart)
        {
            _onTurnStart = onTurnStart;
        }
        public void onTurnStart()
        {
            _onTurnStart(this);
        }
        public bool isConflictWith(RowEffect another)
        {
            return row == another.row && _onPlayer == another._onPlayer;
        }
        public List<Unit> allRowUnits
        {
            get
            {
                return Select.Units(_source.context.cards, 
                    Filter.anyUnitInBattlefield(), 
                    Filter.anyUnitHostBy(_onPlayer),
                    Filter.anyUnitInRow(row));
            }
        }
        public override string ToString()
        {
            return String.Format("<{0}>", _source.name);
        }
    }
}
