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
        public int OptionsCount { get { return _choiseOptions.Count; } }

        public virtual string DescriptionForOption(int optionIndex) { return "..."; }
        public void HighlightSelected(int optionIndex) { for (int i = 0; i < OptionsCount; ++i) if (i == optionIndex) mark(i); else demark(i); }
        protected virtual void mark(int optionIndex) { }
        protected virtual void demark(int optionIndex){  }

        protected bool _hasExtraChoise = false;
        protected string _extraChoise;
        protected string _question;
        protected List<String> _choiseOptions;
        // . . . no descriptions
    }

    class ChoiseOptionContext : ChoiseContext
    {
        ChoiseOptionContext(List<string> variants, int nSelect)
        {
            _question = String.Format("Select {0} of {1}:", nSelect, variants.Count);
            _choiseOptions = variants;
            _hasExtraChoise = false;
            _extraChoise = "";
        }
        public static ChoiseOptionContext OneOfTwo(string a, string b)
        {
            return new ChoiseOptionContext(new List<string>() { a, b}, 1);
        }
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

        public RowChoiseContext(Player rowsHost, string Question)
        {
            _question = Question;
            _player = rowsHost;
            _choiseOptions = new List<string>();
            foreach (string rowName in Utils.allRows)
                _choiseOptions.Add(rowName);
        }
        public override string DescriptionForOption(int optionIndex)
        {
            return String.Format("{0}'s {1} row", _player.ToString(), Utils.allRows[optionIndex - (_hasExtraChoise ? 1 : 0)]);
        }
    }

    class CardChoiseContext : ChoiseContext
    {
        protected List<Card> _cards;

        CardChoiseContext(List<Card> cards, string Question, bool canSelectNone, string noneVariantName)
        {
            _question = Question;
            _cards = cards;
            _hasExtraChoise = canSelectNone;
            _extraChoise = noneVariantName;

            _choiseOptions = new List<string>();
            if (_hasExtraChoise) 
                _choiseOptions.Add(_extraChoise);

            foreach (Card c in cards)
                _choiseOptions.Add(c.ToString());
        }
        public override string DescriptionForOption(int optionIndex)
        {
            if (_hasExtraChoise && optionIndex == 0)
                return "";
            
            return _cards[optionIndex - (_hasExtraChoise ? 1 : 0)].ToFormat();
        }
        protected override void mark(int optionIndex)
        {
            markOrDemark(optionIndex, true);
        }
        protected override void demark(int optionIndex)
        {
            markOrDemark(optionIndex, false);
        }

        void markOrDemark(int optionIndex, bool high)
        {
            if (optionIndex == 0 && _hasExtraChoise) return;
            Card m = _cards[optionIndex - (_hasExtraChoise ? 1 : 0)];
            if (m._show == null)
                return;
            bool needRedraw = m._show._isSelected != high;
            m._show._isSelected = high;
            if (needRedraw) m._show.redrawSelectedInstant();
        }

        public static CardChoiseContext Default(List<Card> cards)
        {
            return new CardChoiseContext(cards, "", false, "");
        }

        public static CardChoiseContext Default(List<Card> cards, string Question)
        {
            return new CardChoiseContext(cards, Question, false, "");
        }

        public static CardChoiseContext WithNoneOption(List<Card> cards, string Question, string NoneName)
        {
            return new CardChoiseContext(cards, Question, true, NoneName);
        }

        public static CardChoiseContext WithNoneOption(List<Unit> units, string Question, string NoneName)
        {
            List<Card> cards = new List<Card>();
            foreach (Unit u in units)
                cards.Add(u);
            return new CardChoiseContext(cards, Question, true, NoneName);
        }
    }
}
