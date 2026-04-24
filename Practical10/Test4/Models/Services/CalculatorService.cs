using System;
using Test4.Models.Enums;
using Test4.Models.Models;

namespace Test4.Models.Services
{
    public class CalculatorService
    {
        public void Calculate(Calculator model)
        {

            switch (model.Operation)
            {
                case (CalcOperation.Add):
                    model.Answer = model.Number1 + model.Number2;
                    break;

                case (CalcOperation.Subtract):
                    model.Answer = model.Number1 - model.Number2;
                    break;

                case (CalcOperation.Multiply):
                    model.Answer = model.Number1 * model.Number2;
                    break;

                case (CalcOperation.Divide):
                    if (model.Number2 == 0)
                        throw new DivideByZeroException("Cannot divide by zero!");
                    model.Answer = model.Number1 / model.Number2;
                    break;

                case (CalcOperation.Modulus):
                    model.Answer = model.Number1 % model.Number2;
                    break;
            }

        }
    }
}