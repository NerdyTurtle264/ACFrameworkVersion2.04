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
            MaxSpeed = 40;
            Health = 5;
            ForceList.RemoveAll();
            ForceList.Add(new cForceBossMovement(150000.0f));
            Sprite = new cSpriteQuake(ModelsMD2.Eva01);
            Radius = 3;
            moveTo(new cVector3(Game.Border.Midx, Game.Border.Loy, Game.Border.Midz));
            Target = Game.Player;
            AimToAttitudeLock = false;
            //copyMotionMatrixToAttitudeMatrix();
            AttitudeToMotionLock = true;
            BULLETRADIUS = 0.9f;
            setMoveBox(new cRealBox3(new cVector3(Game.Border.Lox, Game.Border.Loy, Game.Border.Loz + 3),
                                     new cVector3(Game.Border.Hix, Game.Border.Hiy, Game.Border.Hiz - 3)));
        }

        public override void update(ACView pactiveview, float dt)
        {
            //base.update(pactiveview, dt);
            aimAt(Target);
            shoot();
            feelforce();
            //copyMotionMatrixToAttitudeMatrix();
        }

        public override bool IsKindOf(string str)
        {
            return str == "cCritterBoss" || base.IsKindOf(str);
        }

        public override cCritterBullet shoot()
        {
            cCritterBullet newBullet = base.shoot();
            newBullet.Velocity = _aimvector.mult(15.0f);
            return newBullet;
        }
        public override void die()
        {
            base.die();
            Game.Player.addScore(100);
            Framework.snd.play(Sound.Crunch);
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
            cForceBossMovement f = new cForceBossMovement(speed);
            f.copy(this);
            return f;
        }
    }
}
