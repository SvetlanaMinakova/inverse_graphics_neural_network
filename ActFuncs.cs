using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class ActFuncs
    {
        // activation function. It «presses» its input and produces output for neuron
        //this kind of activation function is sigmoidal, but it can be different
        public static float f_act_sigma(float input)
        {//input for act func is [-inf;+inf] and our input is [0;1]
            float result= (float)(1 / (1 + Math.Pow(Math.E, (-1) * (input))));
            return result;
            //SOFTPLUS INSTEAD OF SIGMA!
          //  return (float)(Math.Log(1 + Math.Exp(input)));

        }

        public static  float f_act_sigma_deriv(float input)
        {
           return f_act_sigma(input) * (1 - f_act_sigma(input));
          //  return (float)(1 / (1 + Math.Exp((-1) * input)));
        }

        public static float f_act_linear(float input)
        {//input for act func is [-inf;+inf] and our input is [0;1]
            float k = 1;
            float result = k * input;
            return result;
        }

        public static float f_act_linear_deriv(float input)
        {
            float k = 1;
            return k;
        }

        public static float f_act_softplus(float input)
        {
            return (float)(Math.Log10(1 + Math.Exp(input)));
        }

        public static float f_act_softplus_deriv(float input)
        {
            return (float)(1 / (1 + Math.Exp((-1) * input)));
        }

    }


}
