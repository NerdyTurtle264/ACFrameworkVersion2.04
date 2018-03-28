using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACFramework
{
    class cListenerAttackAndMove : cListenerArrow
    {
        public override void listen(float dt, cCritter pcritter)
        {


            bool left = Framework.Keydev[vk.Left];
            bool right = Framework.Keydev[vk.Right];
            bool up = Framework.Keydev[vk.Up];
            bool down = Framework.Keydev[vk.Down];
            bool pageup = Framework.Keydev[vk.PageUp];
            bool pagedown = Framework.Keydev[vk.PageDown];


            if (!left && !right && !down && !up && !pagedown && !pageup)
            {
                pcritter.Velocity = new cVector3(0.0f, 0.0f, 0.0f);
                return;
            }

            if (up)
                pcritter.Velocity = new cVector3(-pcritter.MaxSpeed, 0.0f, 0.0f);
            if (down)
                pcritter.Velocity = new cVector3(pcritter.MaxSpeed, 0.0f, 0.0f);
           // if (down)
               // pcritter.Velocity = new cVector3(0.0f, -pcritter.MaxSpeed, 0.0f);
          //  if (up)
             //   pcritter.Velocity = new cVector3(0.0f, pcritter.MaxSpeed, 0.0f);
            if (right)
                pcritter.Velocity = new cVector3(0.0f, 0.0f, -pcritter.MaxSpeed);
            if (left)
                pcritter.Velocity = new cVector3(0.0f, 0.0f, pcritter.MaxSpeed);

            //Now match the attitude.
            if (pcritter.AttitudeToMotionLock)
                /* Need this condition if you want
            to have a "spaceinvaders" type shooter that always points up as in 
            the textbook problem 3.11 */
                pcritter.copyMotionMatrixToAttitudeMatrix();
            //Note that if pcritter is cCritterArmed*, then the cCritterArmed.listen does more stuff.
        }
    }
}
