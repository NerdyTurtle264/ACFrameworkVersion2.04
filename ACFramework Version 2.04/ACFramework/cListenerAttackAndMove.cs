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

        public cListenerAttackAndMove()
        {
            _hopStrength = 1000;
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


            if (!left && !right && !down && !up && !pagedown && !space)
            {
                pcritter.Velocity = new cVector3(0.0f, pcritter.Velocity.Y, 0.0f);
                pcritter.Acceleration = new cVector3(0.0f, pcritter.Acceleration.Y, 0.0f);
                return;
            }

            if (up)
                pcritter.Velocity = new cVector3(-pcritter.MaxSpeed/2, pcritter.Velocity.Y, 0.0f);
            if (down)
                pcritter.Velocity = new cVector3(pcritter.MaxSpeed/2, pcritter.Velocity.Y, 0.0f);
            if (space && !_hopping)
            {
                pcritter.Acceleration = new cVector3(0.0f, _hopStrength, 0.0f);
                _hopping = true;
            }
                
         //   if (pagedown)
           //     pcritter.Velocity = new cVector3(0.0f, pcritter.MaxSpeed, 0.0f);
            if (right)
                pcritter.Velocity = new cVector3(0.0f, pcritter.Velocity.Y, -pcritter.MaxSpeed/2);
            if (left)
                pcritter.Velocity = new cVector3(0.0f, pcritter.Velocity.Y, pcritter.MaxSpeed/2);

            //Now match the attitude.
            if (pcritter.AttitudeToMotionLock)
                /* Need this condition if you want
            to have a "spaceinvaders" type shooter that always points up as in 
            the textbook problem 3.11 */
                pcritter.copyMotionMatrixToAttitudeMatrix();
            //Note that if pcritter is cCritterArmed*, then the cCritterArmed.listen does more stuff.

            if (pcritter.Velocity.Y == 0)
            {
                _hopping = false;
            }

            if (lControl)
            {
                if (pcritter.IsKindOf("cCritter3DPlayer"))
                {
                    cCritter3DPlayer player = (cCritter3DPlayer)pcritter;
                    player.MeleeAttack();
                }
            }
        }
        public override bool IsKindOf(string str)
        {
            return str == "cListenerAttackAndMove" || base.IsKindOf(str);
        }
    }
}
