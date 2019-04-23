using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Gwent2
{
    class Leader : Unit
    {
        public virtual void setLeaderAttributes()
        {
            addTag(Tag.leader);
            place = Place.leader;
        }
        public override Card spawnDefaultCopy(Player newHost, Card sourceOfMakeingCopy)
        {
            Leader copy = this.spawnCard() as Leader;
            copy.SetDefaultHost(newHost, sourceOfMakeingCopy.context);
            return CommonFunc.createToken(copy, sourceOfMakeingCopy);
        }
        public override Card spawnCard()
        {
            string methodName = name, callMethodName = "";
            for (int i = 0; i < methodName.Length; ++i)
                if (alphabet.IndexOf(methodName[i]) >= 0)
                    callMethodName += methodName[i];
            SpawnLeader spun = new SpawnLeader();
            PropertyInfo m = spun.GetType().GetProperty(callMethodName);
            return m.GetMethod.Invoke(spun, null) as Leader;
        }
    }
}
