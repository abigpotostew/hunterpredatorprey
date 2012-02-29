using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.Steering
{
    public static class Steerings
    {
        public static ISteering face, arrive, velocityMatch, separation, separationFromHunter,
            lookWhereGoing, flee200, cohesion, averageVelocityMatch, seek, bushSeparation,
            wander, separationFromLion;

        public static void InitializeSteering()
        {
            face = new Face(0.1f, 2, 0.1f);
            arrive = new Arrive(10, 100, 0.1f);

            bushSeparation = new Separation(40);
            velocityMatch = new VelocityMatch(0.1f);
            separation = new Separation(100);
            separationFromHunter = new Separation(300);
            separationFromLion = new Separation(300);
            lookWhereGoing = new LookWhereYourGoing(0.1f, 2, 0.1f);
            flee200 = new Flee(200, 10, 0.1f);
            cohesion = new Cohesion(50, 10, 50, 0.1f);
            seek = new Seek(10, 50, 0.1f);
            wander = new Wander(50, 10, 0.5f, 0.1f, 2, 0.1f);
        }
    }
}
