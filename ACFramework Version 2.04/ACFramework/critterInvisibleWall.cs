using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACFramework
{
    class cCritterInvisibleWall : cCritterWall
    {
        public cCritterInvisibleWall(cVector3 enda, cVector3 endb, float thickness = THICKNESS,
            float height = WALLPRISMDZ, cGame pownergame = null) : base(enda, endb, thickness,
            height, pownergame)
        {
            Sprite = new cSpriteComposite();
        }

        public override bool collide(cCritter pcritter)
        {
            if (pcritter.IsKindOf("cCritterViewer")) 
                return false;
            else
                return base.collide(pcritter);
        }

        public override bool IsKindOf(string str)
        {
            return str == "cCritterInvisibleWall" || base.IsKindOf(str);
        }
    }
}
