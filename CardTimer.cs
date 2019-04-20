using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Timer
    {
        bool _enabled = false;
        bool _repeatable = false;
        
        int _max = 0;
        int _current = 0;

        Card _host = null;

        TriggerTurn _actionOnFinish = (s) => { s.context.Log(s, "timer finished."); };

        // can be used for Single-Use abilities
        public Timer(Card Host, TriggerTurn Action, int Max, bool Repeatable)
        {
            _max = Max;
            _enabled = true;
            _repeatable = Repeatable;
            _host = Host;
            _actionOnFinish = Action;
        }
        public Timer(Card Host, TriggerTurn Action, int Max)
        {
            _max = Max;
            _enabled = true;
            _host = Host;
            _actionOnFinish = Action;
        }
        public Timer(Card Host)
        {
            // used for abilities that could be used several times
            _max = int.MaxValue;
            _enabled = true;
            _host = Host;
        }
        public bool Untick()
        {
            if (_current <= 0)
                return false;
            _current--;
            return true;
        }
        public Timer()
        {
            // disabled
        }
        public void Tick()
        {
            if (!_enabled)
                return;
            _current++;
            if (_host == null)
                return;
            if (_current >= _max)
            {
                _actionOnFinish(_host);
                _host.context.Log(_host, 
                    !_repeatable? 
                    "Single-Use ability activated" 
                    : String.Format("Ability activated. Repeatance: every {0} turn(s)", _max));
                if (_repeatable)
                    _current = 0;
                else
                    _enabled = false;
            }
        }
        public bool IsEverTicked()
        {
            return _current != 0;
        }
    }
}
