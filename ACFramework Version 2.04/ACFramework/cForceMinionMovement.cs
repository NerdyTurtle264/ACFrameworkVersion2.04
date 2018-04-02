using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACFramework
{
    class cForceMinionMovement : cForce
    {
        cCritter moveTowards;
        public cForceMinionMovement(cCritter target) :base()
        {
            moveTowards = target;
        }

        public override cVector3 force(cCritter pcritter)
        {
            if (Math.Abs(moveTowards.Position.X - pcritter.Position.X) < 1)
                return new cVector3(0, 0, 150);//(pcritter.Position.Z - moveTowards.Position.Z) * 100);
            else
                return new cVector3((moveTowards.Position.X - pcritter.Position.X)*50, 0, 0);
        }

        public override void copy(cForce pforce)
        {
            base.copy(pforce);
            if (!pforce.IsKindOf("cForceMinionMovement"))
                return;
            cForceMinionMovement pforcechild = (cForceMinionMovement)pforce;
            moveTowards = pforcechild.moveTowards;
        }

        public override cForce copy()
        {
            cForceMinionMovement force = new cForceMinionMovement(null);
            force.copy(this);
            return force;
        }

        public override bool IsKindOf(string str)
        {
            return str == "cForceMinionMovement" || base.IsKindOf(str);
        }

        public cCritter MoveTowards
        {
            get
            {
                return moveTowards;
            }
            set
            {
                moveTowards = value;
            }
        }
    }
}
