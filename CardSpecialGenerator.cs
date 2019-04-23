using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class SpawnSpecial : Spawner
    {

        // universal templates
        public static Special addSpecialToGame(Special preset, Card source)
        {
            preset.SetDefaultHost(source.host, source.context);
            source.context.AddCardToGame(preset);
            preset._show.setPosition(new System.Drawing.Point(source._show.position.X + source.ToString().Length - 1, source._show.position.Y));
            preset._show._doNotCleanLine = true;
            preset.makeVisibleAll();
            return preset;
        }
        public static void charm(Card source, Unit target)
        {
            source.context.Log(target, "charmed by " + source.ToString());
            target.setCharmHost(source.host);
        }

        // neutral
        // bronze
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
        public static Special Shrike
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Shrike");
                spec.setSpecialAttributes(Tag.alchemy, Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    foreach (Unit t in Filter.randomUnitsFrom(Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s)), 6))
                        t.damage(s, 2);
                }, "Deal 2 damage to 6 random enemies.");
                return spec;
            }
        }
        public static Special PetrisPhilter
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Petri's Philter");
                spec.setSpecialAttributes(Tag.alchemy, Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    foreach (Unit t in Filter.randomUnitsFrom(Select.Units(s.context.cards, Filter.anyAllyUnitInBattlefield(s)), 6))
                        t.boost(s, 2);
                }, "Boost 6 random allies by 2.");
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
        public static Special BloodcurdlingRoar
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Bloodcurdling Roar");
                spec.setSpecialAttributes(Tag.organic);
                spec.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyAllyUnitInBattlefield(s)), s.QestionString());
                    if (t != null)
                    {
                        t.destroy(s);
                        s.host.playCard(CommonFunc.createToken(SpawnUnit.TokenBear, s));
                    }
                }, "Destroy an Ally. Spawn a Bear.");
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
                    var possibleSourceOfBuff
                        = Select.Units(s.context.cards,
                            Filter.anyAllyUnitInHand(s),
                            Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver));
                    if (possibleSourceOfBuff.Count == 0)
                        return;
                    var possibleTargetsForBuff
                        = Select.Units(s.context.cards, Filter.anyUnitInBattlefield());
                    if (possibleTargetsForBuff.Count == 0)
                        return; // no targets

                    Unit so = s.host.selectUnit(possibleSourceOfBuff, "Select a Bronze or Silver unit in your hand");
                    Unit to = s.host.selectUnit(possibleTargetsForBuff, "Select a unit to boost");

                    to.boost(s, so.basePower);

                }, "Boost a unit by the base power of a Bronze or Silver unit in your hand.");
                return spec;
            }
        }
        public static Special MastercraftedSpear
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Mastercrafted Spear");
                spec.setSpecialAttributes(Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    var possibleSourceOfBuff
                        = Select.Units(s.context.cards,
                            Filter.anyAllyUnitInHand(s),
                            Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver));
                    if (possibleSourceOfBuff.Count == 0)
                        return;
                    var possibleTargetsForBuff
                        = Select.Units(s.context.cards, Filter.anyUnitInBattlefield());
                    if (possibleTargetsForBuff.Count == 0)
                        return; // no targets

                    Unit so = s.host.selectUnit(possibleSourceOfBuff, "Select a Bronze or Silver unit in your hand");
                    Unit to = s.host.selectUnit(possibleTargetsForBuff, "Select a unit to damage");

                    to.damage(s, so.basePower);

                }, "Deal damage equal to the base power of a Bronze or Silver unit in your hand.");
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
                        Filter.randomUnitsFrom(
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
        public static Special Spores
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Spores");
                spec.setSpecialAttributes(Tag.organic);
                spec.setOnDeploy((s, f) =>
                {
                    Player enemy = s.host.chooseEnemy(s.context, HazardQuestionPlayer(s.name));
                    int row = s.host.chooseEnemyRow(enemy, HazardQuestionRow(s.name));
                    s.context._removeRowEffect(enemy, row, Filter.anyCardHasTagAnyFrom(Tag.boon));
                    foreach (Unit t in Select.Units(s.context.cards, Filter.anyEnemyUnitInBattlefield(s), Filter.anyUnitInRow(row)))
                        t.damage(s, 2);

                }, "Deal 2 damage to all units on a row and clear a Boon from it.");
                return spec;
            }
        }
        public static Special MahakamAle
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Mahakam Ale");
                spec.setSpecialAttributes(Tag.alchemy);
                spec.setOnDeploy((s, f) =>
                {
                    for (int row = 0; row < 3; ++row)
                    {
                        Unit t = Filter.randomUnitFrom(
                            Select.Units(s.context.cards,
                            Filter.anyAllyUnitInBattlefield(s),
                            Filter.anyUnitInRow(row)));
                        if (t != null)
                            t.boost(s, 4);
                    }
                }, "Boost a random ally on each row by 4.");
                return spec;
            }
        }

        // silver
        public static Special Mandrake
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.silver, "Mandrake");
                spec.setSpecialAttributes(Tag.alchemy, Tag.organic);
                spec.setOnDeploy((s, f) =>
                {
                    if (s.host.selectOption(ChoiseOptionContext.OneOfTwo(
                        "Heal a unit and Strengthen it by 6",
                        "Reset a unit and Weaken it by 6."))
                        == 0)
                    {
                        Unit u = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyUnitInBattlefield()), s.QestionString());
                        if (u == null) return;
                        u.heal(s);
                        u.strengthen(s, 6);
                    }
                    else
                    {
                        Unit u = s.host.selectUnit(Select.Units(s.context.cards, Filter.anyUnitInBattlefield()), s.QestionString());
                        if (u == null) return;
                        u.reset(s);
                        u.weaken(s, 6);
                    }
                }, "Choose One: Heal a unit and Strengthen it by 6; or Reset a unit and Weaken it by 6.");
                return spec;
            }
        }

        // gold
        public static Special Muzzle
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.gold, "Muzzle");
                spec.setSpecialAttributes(Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    Unit t = s.host.selectUnit(
                        Select.Units(s.context.cards,
                            Filter.anyEnemyUnitInBattlefield(s),
                            Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver),
                            (u) => { return u.power <= 8; }),
                        s.QestionString());

                    if (t != null)
                        charm(s, t);
                }, "Charm a Bronze or Silver enemy with 8 power or less.");
                return spec;
            }
        }

        // hazzards
        public static TriggerTurnRowEffect rain = (r) => { foreach (Unit t in Filter.randomUnitsFrom(r.allRowUnits, 2))t.damage(r.Source, 1); };
        public static TriggerTurnRowEffect frost = (r) => { Unit t = Filter.lowestUnit(r.allRowUnits); if (t != null)t.damage(r.Source, 2); };
        public static TriggerTurnRowEffect fog = (r) => { Unit t = Filter.highestUnit(r.allRowUnits); if (t != null)t.damage(r.Source, 2); };
        public static TriggerTurnRowEffect storm = (r) =>
        {
            List<int> damageFromLeft = new List<int>() { 2, 1, 1 };
            List<Unit> ts = r.allRowUnits;
            for (int i = 0; i < Math.Min(damageFromLeft.Count, ts.Count); ++i)
                ts[i].damage(r.Source, damageFromLeft[i]);
        };

        public static Special TorrentialRain
        {
            get { return Hazard("Torrential Rain", "Apply a Hazard to an enemy row that deals 1 damage to 2 random units on turn start.", rain); }
        }
        public static Special BitingFrost
        {
            get { return Hazard("Biting Frost", "Apply a Hazard to an enemy row that deals 2 damage to the Lowest unit on turn start.", frost); }
        }
        public static Special ImpenetrableFog
        {
            get { return Hazard("Impenetrable Fog", "Apply a Hazard to an enemy row that deals 2 damage to the Highest unit on turn start.", fog); }
        }
        public static Special SkelligeStorm
        {
            get { return Hazard("Skellige Storm", "Apply a Hazard to an enemy row that deals 2, 1 and 1 damage to the leftmost units on the row on turn start.", storm, Rarity.silver); }
        }
        // boons
        public static Special GoldenFroth
        {
            get
            {
                return Boon(
                    "Golden Froth",
                    "Apply a Boon to an allied row that boosts 2 random units by 1 on turn start.",
                    (r) =>
                    {
                        foreach (Unit t in Filter.randomUnitsFrom(r.allRowUnits, 2))
                            t.boost(r.Source, 1);
                    });
            }
        }
        public static Special ClearSkies
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Clear Skies");
                spec.setSpecialAttributes(Tag.doomed);
                spec.setOnDeploy((s, f) =>
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        RowEffect r = s.context._rowEffectOn(i, s.host);
                        if (r == null || !r.Source.hasTag(Tag.hazard)) continue;    // no hazard found

                        s.context._removeRowEffect(s.host, i);
                        foreach (Unit u in Select.Units(s.context.cards, Filter.anyUnitInRow(i), Filter.anyUnitHostBy(s.host), Filter.anyUnitDamaged()))
                            u.boost(s, 2);
                    }
                }, "Boost all damaged allies under Hazards by 2 and clear all Hazards from your side.");
                return spec;
            }
        }
        public static Special Rally
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "Rally");
                spec.setSpecialAttributes(Tag.tactic, Tag.doomed);
                spec.setOnDeploy((s, f) =>
                {
                    Unit u = Filter.randomUnitFrom(Select.Units(s.context.cards, Filter.anyAllyUnitInDeck(s), Filter.anyUnitHasColor(Rarity.bronze)));
                    if (u != null)
                        s.host.playCard(u);
                }, "Play a random Bronze unit from your deck.");
                return spec;
            }
        }
        public static Special FirstLight
        {
            get
            {

                Special spec = new Special();
                spec.setAttributes(Clan.neutral, Rarity.bronze, "First Light");
                spec.setSpecialAttributes(Tag.tactic);
                spec.setOnDeploy((s, f) =>
                {
                    List<Card> vars = new List<Card>() { 
                        SpawnSpecial.ClearSkies,
                        SpawnSpecial.Rally
                    };
                    s.host.playCard(addSpecialToGame(s.host.selectCard(vars, s.QestionString()) as Special, s));
                }, "Choose One: Boost all damaged allies under Hazards by 2 and clear all Hazards from your side; or Play a random Bronze unit from your deck.");
                return spec;
            }
        }
        static string HazardQuestionPlayer(string name) { return String.Format("Select enemy player to apply {0}", name); }
        static string HazardQuestionRow(string name) { return String.Format("Select enemy's row to apply {0}", name); }
        static string BoonQuestionRow(string name) { return String.Format("Select row to apply {0}", name); }
        static Special Hazard(string name, string description, TriggerTurnRowEffect trigger, Rarity rarity = Rarity.bronze)
        {
            Special spec = new Special();
            spec.setAttributes(Clan.neutral, rarity, name);
            spec.setSpecialAttributes(Tag.hazard);
            spec.setOnDeploy((s, f) =>
            {
                Player enemy = s.host.chooseEnemy(s.context, HazardQuestionPlayer(s.name));
                int row = s.host.chooseEnemyRow(enemy, HazardQuestionRow(s.name));

                RowEffect hazz = new RowEffect(s, enemy, row);
                hazz.SetBehaviour(trigger);

            }, description);
            return spec;
        }
        static Special Boon(string name, string description, TriggerTurnRowEffect trigger)
        {
            Special spec = new Special();
            spec.setAttributes(Clan.neutral, Rarity.bronze, name);
            spec.setSpecialAttributes(Tag.boon);
            spec.setOnDeploy((s, f) =>
            {
                RowEffect boon = new RowEffect(s, s.host, s.host.chooseRow(BoonQuestionRow(s.name)));
                boon.SetBehaviour(trigger);

            }, description);
            return spec;
        }


        // skiellege
        public static Special BoneTalisman
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.skellige, Rarity.bronze, "Bone Talisman");
                spec.setSpecialAttributes(Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    if (s.host.selectOption(ChoiseOptionContext.OneOfTwo(
                        "Resurrect a Bronze Beast or Cultist.",
                        "Heal an ally and Strengthen it by 3."))
                        == 0)
                    {
                        Unit u = s.host.selectUnit(
                            Select.Units(s.context.cards,
                                Filter.anyAllyUnitInDiscard(s),
                                Filter.anyUnitHasTagAnyFrom(Tag.beast, Tag.cultist),
                                Filter.anyUnitHasColor(Rarity.bronze)),
                            s.QestionString());
                        if (u != null)
                            s.host.playCard(u);
                    }
                    else
                    {
                        Unit u = s.host.selectUnit(
                            Select.Units(s.context.cards,
                                Filter.anyAllyUnitInBattlefield(s)),
                            s.QestionString());
                        if (u != null)
                        {
                            u.heal(s);
                            u.strengthen(s, 3);
                        }
                    }
                }, "Choose One: Resurrect a Bronze Beast or Cultist; or Heal an ally and Strengthen it by 3.");
                return spec;
            }
        }
        public static Special Restore
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.skellige, Rarity.silver, "Restore");
                spec.setSpecialAttributes(Tag.spell);
                spec.setOnDeploy((s, f) =>
                {
                    Unit u = s.host.selectUnit(
                            Select.Units(s.context.cards,
                                Filter.anyAllyUnitInDiscard(s),
                                Filter.anyUnitHasClan(Clan.skellige),
                                Filter.anyUnitHasColor(Rarity.bronze, Rarity.silver)),
                            s.QestionString());
                    if (u != null)
                    {
                        // return it to hand, power -> 8, added doomed
                        u.setBasePowerTo(s, 8);
                        u.addTag(Tag.doomed);
                        u.move(Place.hand);
                    }
                    Card p = s.host.selectCard(Select.Cards(s.context.cards, Filter.anyCardInYourHand(s)), "Select a card to play");
                    if (p != null)
                        s.host.playCard(p);
                }, "Return a Bronze or Silver Skellige unit from your graveyard to your hand, add the Doomed category to it, and set its base power to 8. Then play a card.");
                return spec;
            }
        }
        public static Special StribogRunestone
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.skellige, Rarity.silver, "Stribog Runestone");
                spec.setSpecialAttributes(Tag.alchemy);
                spec.setOnDeploy((s, f) =>
                {
                    s.host.playCard(CommonFunc.createCard(s,
                        CommonFunc.allCards,
                        Filter.anyCardHasClan(Clan.skellige),
                        Filter.anyCardHasColor(Rarity.silver),
                        Filter.nonSpyingCard()));
                }, "Create and play a silver Skellige card.");
                return spec;
            }
        }
        public static Special OrnamentalSword
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.skellige, Rarity.silver, "Ornamental Sword");
                spec.setSpecialAttributes(Tag.item);
                spec.setOnDeploy((s, f) =>
                {
                    Unit soldier = CommonFunc.createCard(s,
                        CommonFunc.allCards,
                        Filter.anyCardHasClan(Clan.skellige),
                        Filter.anyCardHasTagAnyFrom(Tag.soldier),
                        Filter.anyCardHasColor(Rarity.silver, Rarity.bronze)) as Unit;
                    if (soldier == null) return;
                    soldier.strengthen(soldier, 3);
                    s.host.playCard(soldier);
                }, "Create a Bronze or Silver Skellige Soldier and Strengthen it by 3.");
                return spec;
            }
        }

        // nilfgaard
        // < > SPECIALS
        // < > GOLD
        public static Special Assassination
        {
            get
	        {
		        Special spec = new Special();
		        spec.setAttributes(Clan.nilfgaard, Rarity.gold, "Assassination");
		        spec.setSpecialAttributes(Tag.tactic);
		
		        // Do not forget to check and RECHECK correctence of current ability,
		        // its triggering condition and signature of delegate!
		        spec.setOnDeploy ((s, f) => 
		        {}, "Deal 8 damage to an enemy. Repeat once.");
		
		        return spec;
	        }
        }
        // < > SILVER
        public static Special Cadaverine
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.nilfgaard, Rarity.silver, "Cadaverine");
                spec.setSpecialAttributes(Tag.alchemy, Tag.item);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                spec.setOnDeploy((s, f) =>
                { }, "Choose One: Deal 2 damage to an enemy and all units that share its categories; or Destroy a Bronze or Silver Neutral unit.");

                return spec;
            }
        }
        public static Special DazhbogRunestone
        {
            get
            {
                Special spec = new Special();
                spec.setAttributes(Clan.nilfgaard, Rarity.silver, "Dazhbog Runestone");
                spec.setSpecialAttributes(Tag.alchemy, Tag.item);

                // Do not forget to check and RECHECK correctence of current ability,
                // its triggering condition and signature of delegate!
                spec.setOnDeploy((s, f) =>
                { }, "Create a Silver Nilfgaard card.");

                return spec;
            }
        }
        public static Special NilfgaardianGate
        {
            get
	{
		Special spec = new Special();
		spec.setAttributes(Clan.nilfgaard, Rarity.silver, "Nilfgaardian Gate");
		spec.setSpecialAttributes(Tag.tactic);
		
		// Do not forget to check and RECHECK correctence of current ability,
		// its triggering condition and signature of delegate!
		spec.setOnDeploy ((s, f) => 
		{}, "Play a Bronze or Silver Officer from your deck and boost it by 1.");
		
		return spec;
	}
        }

    }
    class RowEffect
    {
        Card _source;
        Player _onPlayer;
        public Card Source { get { return _source; } }
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
