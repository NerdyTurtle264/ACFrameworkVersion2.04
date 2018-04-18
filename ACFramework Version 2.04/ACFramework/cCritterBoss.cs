using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACFramework
{
    class cCritterBoss : cCritterArmedRobot
    {
        public cCritterBoss(cGame pOwnerGame = null) : base(pOwnerGame)
        {
            MaxSpeed = 0;
            Health = 5;
            clearForcelist();
            addForce(new cForceBossMovement(1.0f));
            Sprite = new cSpriteQuake(ModelsMD2.TekkBlade);
            Radius = 3;
            moveTo(new cVector3(Game.Border.Midx, Game.Border.Loy, Game.Border.Midz));
            Target = Game.Player;
            AimToAttitudeLock = true;
            AttitudeToMotionLock = false;
            BULLETRADIUS = 0.5f;
        }

        public override void update(ACView pactiveview, float dt)
        {
            base.update(pactiveview, dt);
        }

        public override bool IsKindOf(string str)
        {
            return str == "cCritterBoss" || base.IsKindOf(str);
        }

        public override cCritterBullet shoot()
        {
            return base.shoot();
        }
    }

    class cForceBossMovement : cForce
    {
        int timer;
        float speed;

        public cForceBossMovement(float pSpeed) : base()
        {
            speed = pSpeed;
        }

        public override cVector3 force(cCritter pcritter)
        {
            timer++;
            
            if (timer < 400)
                return new cVector3(speed, 0, 0);
            else if (timer < 800)
                return new cVector3(-speed, 0, 0);
            else
            {
                timer = 0;
                return new cVector3(0, 0, 0);
            }
        }


        public override void copy(cForce pforce)
        {
            base.copy(pforce);
            if (!pforce.IsKindOf("cForceBossMovement"))
                return;
            cForceBossMovement pforcechild = (cForceBossMovement)(pforce);
            speed = pforcechild.speed;
        }

        public override cForce copy()
        {
            cForceGravity f = new cForceGravity();
            f.copy(this);
            return f;
        }
    }
}
