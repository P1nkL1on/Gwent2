using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class ChoiseContext
    {
        public string Question { get { return _question; } }
        public List<String> ChoiseOptions { get { return _choiseOptions; } }
        public int OptionCount { get { return _choiseOptions.Count; } }

        public virtual string DescriptionForOption(int optionIndex) { return "..."; }

        protected bool _hasExtraChoise = false;
        protected string _extraChoise;
        protected string _question;
        protected List<String> _choiseOptions;
        // . . . no descriptions
    }

    class PlayerChoiseContext : ChoiseContext
    {
        protected List<Player> _players;

        public PlayerChoiseContext(List<Player> variants, string Question)
        {
            _question = Question;
            _players = variants;

            _choiseOptions = new List<string>();
            foreach (Player p in _players)
                _choiseOptions.Add(p.ToString());
        }

        public override string DescriptionForOption(int optionIndex)
        {
            Player p = _players[optionIndex - (_hasExtraChoise? 1 : 0)];
            return String.Format("{2}{0}{1}",
                p.ToString(),
                "".PadLeft(p.roundsWin, '*') + (p.roundsWin > 0? String.Format("  ({0} rounds won)", p.roundsWin) : ""),
                (p as PlayerHuman != null)? "[human]" : "[botAI]");
        }
    }

    class RowChoiseContext : ChoiseContext
    {
        protected Player _player;

        public RowChoiseContext(Player rowsHost)
        {
            _player = rowsHost;
        }
        public override string DescriptionForOption(int optionIndex)
        {
            return String.Format("{0}'s {1} row", _player.ToString(), Utils.allRows[optionIndex - (_hasExtraChoise ? 1 : 0)]);
        }
    }

    class CardChoiseContext : ChoiseContext
    {
        protected List<Card> _cards;

        public CardChoiseContext(List<Card> cards)
        {
            _cards = cards;
        }
        public override string DescriptionForOption(int optionIndex)
        {
            return _cards[optionIndex - (_hasExtraChoise ? 1 : 0)].ToFormat();
        }
    }
}
