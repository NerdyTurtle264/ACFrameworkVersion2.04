using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACFramework
{
    class cCritterBoss : cCritterArmedRobot
    {
        private bool _dead;
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
            AttitudeToMotionLock = false;
            BULLETRADIUS = 0.4f;
            setMoveBox(new cRealBox3(new cVector3(Game.Border.Lox, Game.Border.Loy, Game.Border.Loz + 3),
                                     new cVector3(Game.Border.Hix, Game.Border.Hiy, Game.Border.Hiz - 3)));

            //set upright
            rotateAttitude(new cSpin((float) Math.PI * 3f/2f, new cVector3(1,0,0)));
            //set looking left
            rotateAttitude(new cSpin((float)Math.PI *3f / 2f, new cVector3(0, 0, 1)));
            _dead = false;
        }

        public override void update(ACView pactiveview, float dt)
        {
            //base.update(pactiveview, dt);

            float varX;
            float varY;
            float varZ;
            Framework.randomOb.randomUnitTriple(out varX, out varY, out varZ);
            cVector3 aimingVariance = new cVector3(varX, varY, varZ);
            aimingVariance.multassign(8);
            aimAt(Target.Position.add(aimingVariance));
            shoot();
            feelforce();
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
            _dead = true;
        }

        public bool Dead
        {
            get
            {
                return _dead;
            }
        }

        public override bool collide(cCritter pother)
        {
            if (base.collide(pother))
            {
                if (pother.IsKindOf("cCritter3DPlayer"))
                {
                    Framework.snd.play(Sound.GlassBreaking);
                    pother.addScore(12);
                    pother.addVelocity(new cVector3(0, 0, -5));
                }
                return true;
            }
            return false;
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
            {
                return new cVector3(speed, 0, 0);
            }
            else if (timer < 800)
            {
                return new cVector3(-speed, 0, 0);
            }
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
