using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACFramework
{
    class cListenerAttackAndMove : cListenerArrow
    {
        private float _hopStrength;
        private bool _hopping;
        private bool _meleeAttacking;

        public cListenerAttackAndMove()
        {
            _hopStrength = 100;
            _meleeAttacking = false;
        }
        public override void listen(float dt, cCritter pcritter)
        {
            pcritter.Acceleration = new cVector3(0.0f, pcritter.Acceleration.Y, 0.0f);

            bool left = Framework.Keydev[vk.A];
            bool right = Framework.Keydev[vk.D];
            bool up = Framework.Keydev[vk.W];
            bool down = Framework.Keydev[vk.S];
            bool space = Framework.Keydev[vk.Space];
            bool pagedown = Framework.Keydev[vk.PageDown];
            bool lControl = Framework.Keydev[vk.ControlLeft];

            cCritter3DPlayer playerCritter = (cCritter3DPlayer)pcritter;
            if (!left && !right && !down && !up && !pagedown && !space && !lControl && !playerCritter.CountingFrames)
            {
                pcritter.Velocity = new cVector3(0.0f, pcritter.Velocity.Y, 0.0f);
                pcritter.Acceleration = new cVector3(0.0f, pcritter.Acceleration.Y, 0.0f);
                pcritter.Sprite.ModelState = State.Idle;
                return;
            }

            if ((up || down || right || left) && !_hopping)
                pcritter.Sprite.ModelState = State.Run;


            if (up)
                pcritter.Velocity = new cVector3(-pcritter.MaxSpeed / 4, pcritter.Velocity.Y, pcritter.Velocity.Z);

            else if (down)
                pcritter.Velocity = new cVector3(pcritter.MaxSpeed / 4, pcritter.Velocity.Y, pcritter.Velocity.Z);
            else {
                pcritter.Velocity = new cVector3(0, pcritter.Velocity.Y, pcritter.Velocity.Z);
                    }
            if (space && !_hopping)
            {
                pcritter.Acceleration = new cVector3(0.0f, _hopStrength, 0.0f);
                pcritter.Sprite.ModelState = State.Jump;
                Framework.snd.play(Sound.Blink);
                _hopping = true;
            }

            //   if (pagedown)
            //     pcritter.Velocity = new cVector3(0.0f, pcritter.MaxSpeed, 0.0f);
            if (right)
                pcritter.Velocity = new cVector3(pcritter.Velocity.X, pcritter.Velocity.Y, -pcritter.MaxSpeed / 4);
            else if (left)
                pcritter.Velocity = new cVector3(pcritter.Velocity.X, pcritter.Velocity.Y, pcritter.MaxSpeed / 4);
            else
                pcritter.Velocity = new cVector3(pcritter.Velocity.X, pcritter.Velocity.Y, 0);

            //Now match the attitude.
            if (pcritter.AttitudeToMotionLock)
                /* Need this condition if you want
            to have a "spaceinvaders" type shooter that always points up as in 
            the textbook problem 3.11 */
                pcritter.copyMotionMatrixToAttitudeMatrix();
            //Note that if pcritter is cCritterArmed*, then the cCritterArmed.listen does more stuff.

            if (pcritter.Position.Y <= pcritter.Game.Border.Loy + 2)
            {
                _hopping = false;
            }

            if (lControl)
            {
                if (pcritter.IsKindOf("cCritter3DPlayer"))
                {
                    _meleeAttacking = true;
                    cCritter3DPlayer player = (cCritter3DPlayer)pcritter;
                    player.MeleeAttack();
                    _meleeAttacking = false;
                }
            }
        }
        public override bool IsKindOf(string str)
        {
            return str == "cListenerAttackAndMove" || base.IsKindOf(str);
        }
    }
}
