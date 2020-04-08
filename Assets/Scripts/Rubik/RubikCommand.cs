using System;

namespace Rubik
{
    public struct RubikCommand
    {
        public CommandType type;
        public bool reverse;

        private string str;
        private Action<RubikCommand> function;

        public RubikCommand(CommandType type, bool reverse, Action<RubikCommand> function, string str)
        {
            this.type = type;
            this.reverse = reverse;
            this.function = function;
            this.str = str;
        }

        public void Function()
        {
            function(this);
        }

        public override string ToString()
        {
            return str;
        }
    }
}