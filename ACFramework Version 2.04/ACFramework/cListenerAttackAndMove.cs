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

            bool left = Framework.Keydev[vk.Left];
            bool right = Framework.Keydev[vk.Right];
            bool up = Framework.Keydev[vk.Up];
            bool down = Framework.Keydev[vk.Down];
            bool pageup = Framework.Keydev[vk.PageUp];
            bool pagedown = Framework.Keydev[vk.PageDown];


            if (!left && !right && !down && !up && !pagedown && !pageup)
            {
                pcritter.Velocity = new cVector3(0.0f, pcritter.Velocity.Y, 0.0f);
                pcritter.Acceleration = new cVector3(0.0f, pcritter.Acceleration.Y, 0.0f);
                return;
            }

            if (up)
                pcritter.Velocity = new cVector3(-pcritter.MaxSpeed/2, pcritter.Velocity.Y, 0.0f);
            if (down)
                pcritter.Velocity = new cVector3(pcritter.MaxSpeed/2, pcritter.Velocity.Y, 0.0f);
            if (pageup && !_hopping)
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
        }
        public override bool IsKindOf(string str)
        {
            return str == "cListenerAttackAndMove" || base.IsKindOf(str);
        }
    }
}
