using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukaDraw.Core
{
    static class Constants
    {
        /* ***** DRAWING SHEET ***** */
        //
        //Size
        //
        public const int canvasDsX = 260;
        public const int canvasDsY = 185;
        //
        //margin
        //
        public const int marginX = 10;
        public const int marginY = 10;
        //
        //scalling
        //
        public const int scalling = 4;

        /* ***** DRAWING WINDOWS ***** */
        public const int canvasDwX = canvasDsX * scalling;
        public const int canvasDwY = canvasDsY * scalling;

        /* ***** BEZIER ***** */
        public const int bezierSegment = 4;

        /* ***** ORDER ***** */
        public const string start = "START";
        public const string stop = "STOP";

        /* ***** SVG TAG ***** */
        public const string path = "path";
        public const string pathValue = "d";


        /* ***** GLOBAL CONSTANTS ***** */
        public const string stringIsEmpty = "";
        public const string stringSpace = " ";

        public const int AnswertotheUltimateQuestionofLifeTheUniverseAndEverything = 42; 
        /* https://en.wikipedia.org/wiki/Phrases_from_The_Hitchhiker%27s_Guide_to_the_Galaxy#Answer_to_the_Ultimate_Question_of_Life.2C_the_Universe.2C_and_Everything_.2842.29 */

        /* ***** IHM ***** */
        //
        //Menu
        //
        public const string Disconect = "Déconnexion";
        public const string Conect = "Connexion";
        //
        //Open Svg
        public const string initialDirectory = "c:\\";
        public const string defaultExt = ".svg";
        public const string filter = "SVG document (.svg) |* .svg";
        //
        //Painter
        //
        public const float brushSize = 1.0f;
        public const float minSizeLine = 0.8f;

        /* ***** LOG ****** */
        public const string logPainterRealTime = "LogPainterRealTime.txt";
        public const string logPainter = "LogPainter.txt";
        public const string logOrder = "LogOrder.txt";
    }
}
