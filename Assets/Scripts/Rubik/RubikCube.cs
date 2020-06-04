using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Rubik
{
    public class RubikCube
    {
        public RubikInfo[] Cube;
        public List<RubikCommand> obfuscationCommands;
        public List<RubikCommand> solutionCommands;

        private const int CountElementsRubik = 6 * 9;
        private Dictionary<string, RubikCommand> allCommands;


        public HeuristicSettings settings;
        public IRubikHeuristic Heuristic;
        public float H;

        public float Cost(Node successor)
        {
            return Heuristic.Cost(this, successor.Rubik);
        }


        #region Initializations

        public RubikCube()
        {
            Init();
            obfuscationCommands = new List<RubikCommand>();
            solutionCommands = new List<RubikCommand>();
            CubeInitialization();
        }

        public RubikCube(HeuristicSettings settings)
        {
            this.settings = settings;
            Init();
            obfuscationCommands = new List<RubikCommand>();
            solutionCommands = new List<RubikCommand>();
            CubeInitialization();
            Heuristic = RubikHeuristics.GetHeuristic(settings.heuristicType);
            H = Heuristic.Heuristic(this, settings);
        }

        public RubikCube(RubikCube copy, HeuristicSettings settings)
        {
            this.settings = settings;
            Init();
            obfuscationCommands = new List<RubikCommand>(copy.obfuscationCommands);
            solutionCommands = new List<RubikCommand>(copy.solutionCommands);
            CubeInitialization(copy.Cube);
            Heuristic = RubikHeuristics.GetHeuristic(settings.heuristicType);
            H = Heuristic.Heuristic(this, settings);
        }

        private static readonly int[] CubeMap =
        {
            0,1,2,
            3,4,5,
            6,7,8,
            
            0,3,6,
            9,12,15,
            18,21,24,
            
            6,7,8,
            15,16,17,
            24,25,26,
            
            8,5,2,
            17,14,11,
            26,23,20,
            
            2,1,0,
            11,10,9,
            20,19,18,
            
            24,25,26,
            21,22,23,
            18,19,20,
        };

        public static int CubeNum(int num)
        {
            return CubeMap[num];
        }
        
        public string Decision => solutionCommands.Aggregate("", (current, command) => current + command);

        private void Init()
        {
            allCommands = new Dictionary<string, RubikCommand>()
            {
                {"F", new RubikCommand(CommandType.FRONT, false, Front,"F")},
                {"F\'", new RubikCommand(CommandType.FRONT, true, Front,"F\'")},
                {"B", new RubikCommand(CommandType.BACK, false, Back, "B")},
                {"B\'", new RubikCommand(CommandType.BACK, true, Back, "B\'")},
                {"L", new RubikCommand(CommandType.LEFT, false, Left, "L")},
                {"L\'", new RubikCommand(CommandType.LEFT, true, Left, "L\'")},
                {"R", new RubikCommand(CommandType.RIGHT, false, Right, "R")},
                {"R\'", new RubikCommand(CommandType.RIGHT, true, Right, "R\'")},
                {"U", new RubikCommand(CommandType.UP, false, Up, "U")},
                {"U\'", new RubikCommand(CommandType.UP, true, Up, "U\'")},
                {"D", new RubikCommand(CommandType.DOWN, false, Down, "D")},
                {"D\'", new RubikCommand(CommandType.DOWN, true, Down, "D\'")},
            };
            Cube = new RubikInfo[CountElementsRubik];
        }

        private void CubeInitialization()
        {
            for (int i = 0; i < CountElementsRubik; i++)
            {
                Cube[i] = new RubikInfo(GetColorByInt(i / 9), i);
            }
        }

        private void CubeInitialization(RubikInfo[] copy)
        {
            for (int i = 0; i < CountElementsRubik; i++)
            {
                Cube[i] = new RubikInfo(copy[i]);
            }
        }

        #endregion
        
        public void UseCommand(string commands)
        {
            List<RubikCommand> commandList = StringToCommands(CorrectCommand(commands));
            UseCommands(commandList);
            obfuscationCommands.AddRange(commandList);
        }

        public void UseDecision(string commands)
        {
            List<RubikCommand> commandList = StringToCommands(CorrectCommand(commands));
            UseCommands(commandList);
            solutionCommands.AddRange(commandList);
        }

        private List<RubikCommand> StringToCommands(string correctCommand)
        {
            List<RubikCommand> commands = new List<RubikCommand>();
            correctCommand = CorrectCommand(correctCommand);
            for (int i = 0; i < correctCommand.Length; i++)
            {
                bool reverse = i + 1 < correctCommand.Length && correctCommand[i + 1] == '\'';
                string command = correctCommand[i] + (reverse ? "\'" : "");
                if (!allCommands.ContainsKey(command))
                    throw new Exception("Unknown command " + command);
                commands.Add(allCommands[command]);
                if (reverse) i++;
            }

            return commands;
        }

        private string CorrectCommand(string command)
        {
            command = command.Replace("2'", "2");
            command = command.Replace(" ", "");
            if (command.Length == 0) return command;
            string newCommand = command[0].ToString();
            for (int i = 1; i < command.Length; i++)
            {
                newCommand += command[i] != '2' ? command[i] : command[i - 1];
            }
            return newCommand;
        }

        private void UseCommands(List<RubikCommand> commands)
        {
            foreach (var command in commands)
                command.Function();
        }

        public void Reset()
        {
            CubeInitialization();
        }

        public bool IsCorrect()
        {
            for (int i = 0; i < Cube.Length - 1; i++)
            {
                if (Cube[i].num > Cube[i + 1].num)
                    return false;
            }

            return true;
        }

        #region Rotate

        private void Front(RubikCommand rubikCommand)
        {
            List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate = new List<(int fromLayer, int fromNum, int toLayer, int toNum)>();

            listRotate.Add((0, 6, 3, 0));
            listRotate.Add((0, 7, 3, 3));
            listRotate.Add((0, 8, 3, 6));
            
            listRotate.Add((0, 6, 5, 2));
            listRotate.Add((0, 7, 5, 1));
            listRotate.Add((0, 8, 5, 0));
            
            listRotate.Add((0, 6, 1, 8));
            listRotate.Add((0, 7, 1, 5));
            listRotate.Add((0, 8, 1, 2));
            
            listRotate.AddRange(GetFaceRotateList(2));

            RotateFace(listRotate, rubikCommand.reverse);
        }

        private void Up(RubikCommand rubikCommand)
        {
            List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate = new List<(int fromLayer, int fromNum, int toLayer, int toNum)>();

            listRotate.Add((4, 2, 3, 2));
            listRotate.Add((4, 1, 3, 1));
            listRotate.Add((4, 0, 3, 0));
            
            listRotate.Add((4, 2, 2, 2));
            listRotate.Add((4, 1, 2, 1));
            listRotate.Add((4, 0, 2, 0));
            
            listRotate.Add((4, 2, 1, 2));
            listRotate.Add((4, 1, 1, 1));
            listRotate.Add((4, 0, 1, 0));

            listRotate.AddRange(GetFaceRotateList(0));

            RotateFace(listRotate, rubikCommand.reverse);
        }

        private void Right(RubikCommand rubikCommand)
        {
            List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate = new List<(int fromLayer, int fromNum, int toLayer, int toNum)>();

            listRotate.Add((0, 8, 4, 0));
            listRotate.Add((0, 5, 4, 3));
            listRotate.Add((0, 2, 4, 6));
            
            listRotate.Add((0, 8, 5, 8));
            listRotate.Add((0, 5, 5, 5));
            listRotate.Add((0, 2, 5, 2));
            
            listRotate.Add((0, 8, 2, 8));
            listRotate.Add((0, 5, 2, 5));
            listRotate.Add((0, 2, 2, 2));

            listRotate.AddRange(GetFaceRotateList(3));

            RotateFace(listRotate, rubikCommand.reverse);
        }

        private void Back(RubikCommand rubikCommand)
        {
            List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate = new List<(int fromLayer, int fromNum, int toLayer, int toNum)>();

            listRotate.Add((0, 2, 1, 0));
            listRotate.Add((0, 2, 5, 6));
            listRotate.Add((0, 2, 3, 8));
            
            listRotate.Add((0, 1, 1, 3));
            listRotate.Add((0, 1, 5, 7));
            listRotate.Add((0, 1, 3, 5));
            
            listRotate.Add((0, 0, 1, 6));
            listRotate.Add((0, 0, 5, 8));
            listRotate.Add((0, 0, 3, 2));

            listRotate.AddRange(GetFaceRotateList(4));

            RotateFace(listRotate, rubikCommand.reverse);
        }

        private void Left(RubikCommand rubikCommand)
        {
            List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate = new List<(int fromLayer, int fromNum, int toLayer, int toNum)>();

            listRotate.Add((0, 0, 2, 0));
            listRotate.Add((0, 3, 2, 3));
            listRotate.Add((0, 6, 2, 6));
            
            listRotate.Add((0, 0, 5, 0));
            listRotate.Add((0, 3, 5, 3));
            listRotate.Add((0, 6, 5, 6));
            
            listRotate.Add((0, 0, 4, 8));
            listRotate.Add((0, 3, 4, 5));
            listRotate.Add((0, 6, 4, 2));

            listRotate.AddRange(GetFaceRotateList(1));

            RotateFace(listRotate, rubikCommand.reverse);
        }

        private void Down(RubikCommand rubikCommand)
        {
            List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate = new List<(int fromLayer, int fromNum, int toLayer, int toNum)>();

            listRotate.Add((2, 6, 3, 6));
            listRotate.Add((2, 7, 3, 7));
            listRotate.Add((2, 8, 3, 8));
            
            listRotate.Add((2, 6, 4, 6));
            listRotate.Add((2, 7, 4, 7));
            listRotate.Add((2, 8, 4, 8));
            
            listRotate.Add((2, 6, 1, 6));
            listRotate.Add((2, 7, 1, 7));
            listRotate.Add((2, 8, 1, 8));

            listRotate.AddRange(GetFaceRotateList(5));

            RotateFace(listRotate, rubikCommand.reverse);
        }
        
        private List<(int fromLayer, int fromNum, int toLayer, int toNum)> GetFaceRotateList(int faceNum)
        {
            List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate = new List<(int fromLayer, int fromNum, int toLayer, int toNum)>();

            listRotate.Add((faceNum, 0, faceNum, 2));
            listRotate.Add((faceNum, 0, faceNum, 8));
            listRotate.Add((faceNum, 0, faceNum, 6));
            
            listRotate.Add((faceNum, 1, faceNum, 5));
            listRotate.Add((faceNum, 1, faceNum, 7));
            listRotate.Add((faceNum, 1, faceNum, 3));

            return listRotate;
        }

        private void RotateFace(List<(int fromLayer, int fromNum, int toLayer, int toNum)> listRotate, bool reverse)
        {
            var from = reverse ? listRotate.Count - 1 : 0;
            var to = reverse ? -1 : listRotate.Count;
            var delta = reverse ? -1 : 1;
            for (var i = from; i != to; i += delta)
            {
                Swap(listRotate[i].fromLayer, listRotate[i].fromNum, listRotate[i].toLayer, listRotate[i].toNum);
            }
        }

        private void Swap(int fromLayer, int fromNum, int toLayer, int toNum)
        {
            Swap(fromLayer * 9 + fromNum, toLayer * 9 + toNum);
        }

        private void Swap(int from, int to)
        {
            var tmp = Cube[from];
            Cube[from] = Cube[to];
            Cube[to] = tmp;
        }

        #endregion

        #region Private functions

        private RubikSide GetColorByInt(int i)
        {
            switch (i)
            {
                case 0:
                    return RubikSide.UP;
                case 1:
                    return RubikSide.LEFT;
                case 2:
                    return RubikSide.FRONT;
                case 3:
                    return RubikSide.RIGHT;
                case 4:
                    return RubikSide.BACK;
                case 5:
                    return RubikSide.DOWN;
            }
            throw new Exception("Unknown color");
        }

        #endregion

        public override string ToString()
        {
            string cube = "";
            foreach (var color in Cube)
            {
                cube += color.side.ToString()[0];
            }

            return cube;
        }

        public IEnumerable<RubikCube> Successors()
        {
            List<RubikCube> successors = new List<RubikCube>();

            var commands = allCommands;
            foreach (var command in commands)
            {
                if (!IsNewSuccessor(command.Value)) continue;
                var cube = new RubikCube(this, settings);
                cube.UseDecision(command.Value.ToString());
                successors.Add(cube);
            }
            return successors;
        }

        private bool IsNewSuccessor(RubikCommand command)
        {
            if (solutionCommands.Count > 0 && IsReverseCommand(command, solutionCommands[solutionCommands.Count - 1]))
                return false;
            return true;
        }

        private bool IsReverseCommand(RubikCommand command1, RubikCommand command2)
        {
            return command1.type == command2.type && command1.reverse != command2.reverse;
        }

        public string GetRandomCommands(int len)
        {
            string commands = "";
            for (int i = 0; i < len; i++)
            {
                commands += allCommands.Keys.ToArray()[Random.Range(0, allCommands.Keys.Count)];
            }

            return commands;
        }
    }
}