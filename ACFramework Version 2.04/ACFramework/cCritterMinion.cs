using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACFramework
{
    class cCritterMinion : cCritter3Dcharacter
    {
        public cCritterMinion(cGame pOwnerGame) :base(pOwnerGame)
        {
            ForceList.Add(new cForceMinionMovement(pOwnerGame.Player));
            Sprite = new cSpriteQuake(ModelsMD2.Robot);
            Sprite.Radius = 1;
        }
    }
}
 