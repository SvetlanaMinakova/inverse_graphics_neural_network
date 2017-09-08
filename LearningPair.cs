using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
   public class LearningPair
    {
        public int input_w;
        public int input_h;
        public int output_lenght;
        public float[,] input;
        public float[] expected_output;

        public LearningPair(int inp_w, int inp_h, int outp_l, float[,] inp, float[] expected_outp)
        {
            this.input_w = inp_w;
            this.input_h = inp_h;
            this.output_lenght = outp_l;
            this.input = inp;
            this.expected_output = expected_outp;
        }
    }
}
