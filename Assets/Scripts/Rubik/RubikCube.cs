using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Rubik
{
    public class RubikCube
    {
        public RubikInfo[] Cube;
        private int countElementsRubik = 6 * 9;
        public static string Commands = "FURBLD";

        public RubikCube()
        {
            Cube = new RubikInfo[countElementsRubik];
            CubeInitialization();
        }

        public void UseCommands(string commands)
        {
            CheckCommands(commands);
            for (var i = 0; i < commands.Length; i++)
            {
                var command = commands[i];
                var reverse = i + 1 < commands.Length && commands[i + 1] == '\'';
                UseCommand(command, reverse);
                if (reverse)
                    i++;
            }
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

        public void Front(bool reverse)
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

            RotateFace(listRotate, reverse);
        }
        
        public void Up(bool reverse)
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

            RotateFace(listRotate, reverse);
        }
        
        public void Right(bool reverse)
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

            RotateFace(listRotate, reverse);
        }
        
        public void Back(bool reverse)
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

            RotateFace(listRotate, reverse);
        }
        
        public void Left(bool reverse)
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

            RotateFace(listRotate, reverse);
        }
        
        public void Down(bool reverse)
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

            RotateFace(listRotate, reverse);
        }

        #region Private functions

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
                swap(listRotate[i].fromLayer, listRotate[i].fromNum, listRotate[i].toLayer, listRotate[i].toNum);
            }
        }

        private void swap(int fromLayer, int fromNum, int toLayer, int toNum)
        {
            swap(fromLayer * 9 + fromNum, toLayer * 9 + toNum);
        }

        private void swap(int from, int to)
        {
            var tmp = Cube[from];
            Cube[from] = Cube[to];
            Cube[to] = tmp;
        }

        private void CubeInitialization()
        {
            for (int i = 0; i < countElementsRubik; i++)
            {
                Cube[i] = new RubikInfo(GetColorByInt(i / 9), i);
            }
        }

        private RubikColor GetColorByInt(int i)
        {
            switch (i)
            {
                case 0:
                    return RubikColor.WHITE;
                case 1:
                    return RubikColor.ORANGE;
                case 2:
                    return RubikColor.GREEN;
                case 3:
                    return RubikColor.RED;
                case 4:
                    return RubikColor.BLUE;
                case 5:
                    return RubikColor.YELLOW;
            }
            throw new Exception("Unknown color");
        }

        private void CheckCommands(string commands)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i] != '\'' && !IsCommands(commands[i]))
                    throw new Exception("Unknown command " + commands[i] + " use [FURBLD]");
                if (commands[i] == '\'' && i == 0)
                    throw new Exception("\' cant be first ");
                if (commands[i] == '\'' && !IsCommands(commands[i - 1]))
                    throw new Exception("\' must be under [FURBLD]");
            }
        }

        private bool IsCommands(char command)
        {
            return Commands.Contains(command.ToString());
        }

        private void UseCommand(char command, bool reverse)
        {
            switch (command)
            {
                case 'F':
                    Front(reverse);
                    break;
                case 'U':
                    Up(reverse);
                    break;
                case 'R':
                    Right(reverse);
                    break;
                case 'L':
                    Left(reverse);
                    break;
                case 'B':
                    Back(reverse);
                    break;
                case 'D':
                    Down(reverse);
                    break;
            }
        }
        
        #endregion

        public override string ToString()
        {
            string cube = "";
            foreach (var color in Cube)
            {
                cube += color.color.ToString()[0];
            }

            return cube;
        }
    }
}