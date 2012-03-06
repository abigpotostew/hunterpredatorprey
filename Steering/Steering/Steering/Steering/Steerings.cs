using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steering.Steering
{
    public static class Steerings
    {
        public static ISteering face, arriveGraze, arriveBush, velocityMatch, separationFromDeer, separationFromHunter,
            lookWhereGoing, flee200, cohesion, cohesionGraze, averageVelocityMatch, seek, bushSeparation,
            wander, eatWander, separationFromLion, seekPounce;

        public static void InitializeSteering()
        {
            face = new Face(0.1f, 2, 0.1f);
            arriveGraze = new Arrive(120, 100, 0.1f);
            arriveBush = new Arrive(50, 30, 2f);

            bushSeparation = new Separation(40);
            velocityMatch = new VelocityMatch(0.1f);
            separationFromDeer = new Separation(75);
            separationFromHunter = new Separation(300);
            separationFromLion = new Separation(300);
            lookWhereGoing = new LookWhereYourGoing(0.1f, 2, 0.1f);
            flee200 = new Flee(200, 10, 0.1f);
            cohesion = new Cohesion(50, 25, 50, 0.1f);
            cohesionGraze = new Cohesion(300, 100, 75, 0.1f);
            seek = new Seek(10, 50, 0.1f);
            seekPounce = new Seek(10, 20, 0.1f);
            wander = new Wander(50, 20, 0.1f, 0.1f, 2, 0.1f);
            eatWander = new Wander(10, 30, 0.001f, 10, 10, 0.01f);
        }
    }
}
