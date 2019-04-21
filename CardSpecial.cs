using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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

        public override string ToFormat()
        {
            return String.Format("{0}\n\n{1} {2} card\n{3}\n\n{4}",
                name.ToUpper(), clan.ToString(), rarity.ToString(), tagsToString(), ToFormatAbilities());
        }
        public override string ToFormatCollection()
        {
            return ToFormat();
        }

        public override Card spawnDefaultCopy(Player newHost, Card sourceOfMakeingCopy)
        {
            Special copy = this.spawnCard() as Special;
            return SpawnSpecial.addSpecialToGame(copy, sourceOfMakeingCopy);
        }
        public override Card spawnCard()
        {
            string methodName = name, callMethodName = "";
            for (int i = 0; i < methodName.Length; ++i)
                if (alphabet.IndexOf(methodName[i]) >= 0)
                    callMethodName += methodName[i];
            SpawnSpecial spun = new SpawnSpecial();
            PropertyInfo m = spun.GetType().GetProperty(callMethodName);
            return m.GetMethod.Invoke(spun, null) as Special;
        }
    }
}
