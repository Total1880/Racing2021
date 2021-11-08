using System.Collections.Generic;

namespace Racing2021.Models
{
    public static class TextureNames
    {
        public static string CyclistBlue = "Cyclist_Blue";
        public static string CyclistGreen = "Cyclist_Green";
        public static string CyclistRed = "Cyclist_Red";
        public static string CyclistRoseGrey = "Cyclist_RoseGrey";
        public static string CyclistYellow = "Cyclist_Yellow";
        public static string CyclistBlackYellow = "Cyclist_BlackYellow";
        private static List<string> _list;


        public static List<string> List()
        {
            _list = new List<string>();

            _list.Add(CyclistBlue);
            _list.Add(CyclistGreen);
            _list.Add(CyclistRed);
            _list.Add(CyclistRoseGrey);
            _list.Add(CyclistYellow);
            _list.Add(CyclistBlackYellow);

            return _list;
        }
    }
}
