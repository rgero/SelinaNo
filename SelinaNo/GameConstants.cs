using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SelinaNo
{
    public static class GameConstants
    {
        public const double MAX_SPEED = 0.35;
        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 720;
        public const int RESIZE_FACTOR = 2;

        //Scoreboard Information
        public const int SCOREBOARD_X = 50;
        public const int SCOREBOARD_Y = 20;
        public const string SCORE_PREFIX = "Score: ";

        public const int HEALTH_X = SCREEN_WIDTH - 250;
        public const int HEALTH_Y = 20;
        public const string HEALTH_PREFIX = "Health: ";
        public const int INITIAL_HEALTH = 100;

        public const int CONTROL_X = SCREEN_WIDTH/2;
        public const int CONTROL_Y = SCREEN_HEIGHT - 100;

        public const int MAX_SELINA_SPEED = 15;

    }
}
