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
            // Task: Write an algorithm, that places all forms inside the board.
            // Rules: 1. Place all forms in the board
            //        2. The positions must be calculated by an algorithm, do not set statix/fixed positions!
            //        3. Different chars must not overlap
            //        4. Insert all forms into completedBoard
            //        5. Attach the generated (completed) board as string below in your explanation

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

var form3 =
@"
aaaa
a";

var form4 =
@"
 g 
ggg";


            board = Regex.Replace(board, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form0 = Regex.Replace(form0, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form1 = Regex.Replace(form1, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form2 = Regex.Replace(form2, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form3 = Regex.Replace(form1, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            form4 = Regex.Replace(form2, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);

            var completedBoard = board;

            // <---- ---- ---- add code here ---- ---- ---->

            Assert.Equal(board.Select(x => x == 'x').Count(), completedBoard.Select(x => x == 'x').Count());
            Assert.True(ContainsForm(form0, completedBoard));
            Assert.True(ContainsForm(form1, completedBoard));
            Assert.True(ContainsForm(form2, completedBoard));
            Assert.True(ContainsForm(form3, completedBoard));
            Assert.True(ContainsForm(form4, completedBoard));

            // Your explanation: 
            // 
            // 
        }

        private bool ContainsForm(string form, string board)
        {
            form = form.Replace("\r", "");
            form = form.Replace("\n", "");
            form = new string(form.Where(c => !char.IsWhiteSpace(c)).ToArray());

            if (form.Any())
            {
                var letter = form[0];
                var letterCount = board.Where(x => x == letter).Count();
                return letterCount == form.Length;
            }

            return true;
        }
    }
}
