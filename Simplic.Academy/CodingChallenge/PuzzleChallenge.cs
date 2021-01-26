using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace CodingChallenge
{
    public class PuzzleChallenge
    {
        [Fact]
        public void Puzzle_01()
        {
            var board =
@"
xxxxxxxxxxxxxx
x    x       x
x            x
x       x    x
x  x         x
xxxxxxxxxxxxxx";

            var form0 = 
@"
y
y
y
y";

var form1 =
@"
ww
ww";

var form2 =
@"
vvvv
   v";

            board = Regex.Replace(board, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form0 = Regex.Replace(form0, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form1 = Regex.Replace(form1, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form2 = Regex.Replace(form2, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);

            Debug.WriteLine(board);
            Debug.WriteLine(form0);
            Debug.WriteLine(form1);
            Debug.WriteLine(form2);

            var completedBoard = board;

            // Task: Write an algorithm, that places all forms inside the board.
            // Rules: 1. Place all forms in the board
            //        2. Different chars must not overlap
            //        3. Insert all forms into completedBoard

            Assert.Equal(board.Select(x => x == 'x').Count(), completedBoard.Select(x => x == 'x').Count());
            Assert.Contains(form0, board);
            Assert.Contains(form1, board);
            Assert.Contains(form2, board);

            // Your explanation: 
            // 
            // 
        }
    }
}
