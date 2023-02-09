using System;
using System.Collections.Generic;

namespace RPNCalulator
{
    public class RPN
    {
        private Stack<int> _operators;
        Dictionary<string, Func<int, int, int>> _operationFunction;
        Dictionary<string, Func<int, int>> _operationFunction2;

        public int EvalRPN(string input)
        {
            _operationFunction = new Dictionary<string, Func<int, int, int>>
            {
                ["+"] = (fst, snd) => (fst + snd),
                ["-"] = (fst, snd) => (fst - snd),
                ["*"] = (fst, snd) => (fst * snd),
                ["/"] = (fst, snd) => (fst / snd)

            };
            _operationFunction2 = new Dictionary<string, Func<int, int>>
            {
                ["^2"] = fst => ((int)Math.Pow(fst, 2)),
                ["!"] = fst => silnia(fst),
                ["||"] = fst => wartBezw(fst),
                ["??"] = fst => fst
            };

            _operators = new Stack<int>();
            
            if(input == "")
                throw new Exception("Brak danych");

            var splitInput = input.Split(' ');

            foreach (var op in splitInput)
            {
                if (IsNumber(op))
                    _operators.Push(Int32.Parse(op));
               
                if (IsOperator(op))
                {
                    var num1 = _operators.Pop();
                    var num2 = _operators.Pop();

                    if(num1 == 0)
                    {
                        throw new Exception("Nie dziel przez zero");
                    }
                    _operators.Push(_operationFunction[op](num2, num1));
                    //_operators.Push(Operation(op)(num1, num2));
                }

                if (IsUnaryOperation(op))
                {
                    var num1 = _operators.Pop();
                    _operators.Push(_operationFunction2[op](num1));

                }

                if (op[0].Equals('B'))
                    _operators.Push((int)Convert.ToInt32(op.Substring(1), 2));
                
                if (op[0].Equals('D'))
                    _operators.Push((int)Convert.ToInt32(op.Substring(1), 10));
                
                if (op[0].Equals('#'))
                    _operators.Push((int)Convert.ToInt32(op.Substring(1), 16));
                
            }

            var result = _operators.Pop();
            if (_operators.IsEmpty)
            {
                return result;
            }
            throw new InvalidOperationException();
        }

        private int wartBezw(int input)
        {
            if (input < 0)
                return input * (-1);
            else
                return input;
            
        }

        private int silnia(int input)
        {
            var silnia_ = Convert.ToInt32(input);
            int result = 1;
            for (int i = 1; i <= silnia_; i++)
            {
                result *= i;
            }
            return result;
        }

        private bool IsNumber(String input) => Int32.TryParse(input, out _);

        private bool IsOperator(String input) =>
            input.Equals("+") || input.Equals("-") ||
            input.Equals("*") || input.Equals("/");

        private bool IsUnaryOperation(String input) =>
            input.Equals("!") || input.Equals("^2") || input.Equals("||")
            || input.Equals("??");

        private bool IsBinar(char input) =>
            input.Equals('B') || input.Equals('D') || input.Equals('#');

        private Func<int, int, int> Operation(String input) =>
            (x, y) =>
            (
                (input.Equals("+") ? x + y :
                    (input.Equals("*") ? x * y : int.MinValue)
                )
            );
    }
}