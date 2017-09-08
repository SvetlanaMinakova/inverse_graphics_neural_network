using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class UpSampleFeatureMap
    {
        public float[,] output;
        public float[,] non_activated_stage;
        public float[,] deriv_non_activated_stage;
        public float[,] error;
        public int outputwidth;
        public int outputheight;
        public float[,] input;
        //switchmaps?
        public float[,] switchmap;

        public UpSampleFeatureMap(int outp_w, int outp_h, float[,] input)
        {
            this.outputwidth = outp_w;
            this.outputheight = outp_h;
            this.output = new float[outputwidth, outputheight];
            this.error = new float[outputwidth, outputheight];
            this.non_activated_stage = new float[outputwidth, outputheight];
            this.deriv_non_activated_stage = new float[outputwidth, outputheight];
            this.input = input;
        }

        public void get_output()
        {
            float[,] temp = ConvFuncs.upsample(input, outputwidth, outputheight);
            for (int j = 0; j < outputheight; j++)
            {
                for (int i = 0; i < outputwidth; i++)
                {
                    non_activated_stage[i, j] = temp[i, j];
                    output[i, j] = ActFuncs.f_act_linear(non_activated_stage[i, j]);
                    deriv_non_activated_stage[i, j] = ActFuncs.f_act_linear_deriv(non_activated_stage[i, j]);
                }
            }
        }

    }
}