using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent2
{
    class Special : Card
    {
        public override void move(Place to)
        {
            base.move(to);
            if (to == Place.battlefield)
                move(Place.graveyard);
        }
        public virtual void setSpecialAttributes(params Tag[] Tags)
        {
            _tags = new List<Tag>();
            _tags.Add(Tag.special);
            _tags.AddRange(Tags.ToList());
        }
        public override string ToString()
        {
            return String.Format("[* {0}]", name);
        }
    }
}
