using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class DeconvolutionFeatureMap
    {

        public float[,] output;
        public float[,] non_activated_stage;
        public float[,] deriv_non_activated_stage;
        public int w;
        public int h;
        public int outputwidth;
        public int outputheight;
        public float[,] weights;
        public float[,] input;

        public DeconvolutionFeatureMap(float[,] input,ConvolutionFeatureMap base_conv_fm)
        {
            this.w = base_conv_fm.w;
            this.h = base_conv_fm.h;
            //boundaries effect
            this.outputwidth = base_conv_fm.w + base_conv_fm.outputwidth - 1;
            this.outputheight = base_conv_fm.h + base_conv_fm.outputheight - 1;
            this.weights = new float[w, h];
            this.input = input;
            this.weights = base_conv_fm.weights;
            this.output = new float[outputwidth, outputheight];
            this.non_activated_stage = new float[outputwidth, outputheight];
            this.deriv_non_activated_stage = new float[outputwidth, outputheight];
        }

        //deconvolution+normalization
        public void get_output()
        {
            deconvolution(input);
            Normalization.n_linear(output, outputwidth, outputheight);
        }


        void deconvolution(float[,] input)
        {
            float[,] f = ConvFuncs.back_fold(input, weights, outputwidth, outputheight, w, h);
            for (int j = 0; j < outputheight; j++)
            {
                for (int i = 0; i < outputwidth; i++)
                {
                    non_activated_stage[i, j] = f[i, j];
                    output[i, j] = ActFuncs.f_act_sigma(f[i, j]);
                    deriv_non_activated_stage[i, j] = ActFuncs.f_act_sigma_deriv(non_activated_stage[i, j]);
                }
            }
        }
    }
}