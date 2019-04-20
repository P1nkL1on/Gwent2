using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Leader : Unit
    {
        public virtual void setLeaderAttributes()
        {
            addTag(Tag.leader);
            place = Place.handLeader;
        }
    }
}
