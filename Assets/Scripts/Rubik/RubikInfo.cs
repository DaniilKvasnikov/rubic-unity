namespace Rubik
{
    public class RubikInfo
    {
        public RubikSide side;
        public int num;

        public RubikInfo(RubikSide side, int num)
        {
            this.side = side;
            this.num = num;
        }

        public RubikInfo(RubikInfo rubikInfo)
        {
            side = rubikInfo.side;
            num = rubikInfo.num;
        }
    }
}