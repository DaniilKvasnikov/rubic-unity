﻿namespace Rubik
{
    public class RubikInfo
    {
        public RubikColor color;
        public int num;

        public RubikInfo(RubikColor color, int num)
        {
            this.color = color;
            this.num = num;
        }

        public RubikInfo(RubikInfo rubikInfo)
        {
            color = rubikInfo.color;
            num = rubikInfo.num;
        }
    }
}