using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    public class ConvolutionFeatureMap
    {
        public float[,] output;
        public float[,] non_activated_stage;
        public float[,] deriv_non_activated_stage;
        public float[,] error;
        public float b = 0;
        public int w;
        public int h;

        //can be different. depends on processing methods(valid/same/full) and boundary effects
        public int outputwidth;
        public int outputheight;
        public float[,] weights;
        public List<float[,]> inputs;

        public ConvolutionFeatureMap(int w, int h, int output_w, int output_h)
        {
            this.w = w;
            this.h = h;
            //boundaries effect
            this.outputwidth = output_w;
            this.outputheight = output_h;
            this.weights = new float[w, h];
            this.inputs = new List<float[,]>();
            this.output = new float[outputwidth, outputheight];
            this.error = new float[outputwidth, outputheight];
            this.non_activated_stage = new float[outputwidth, outputheight];
        }

        //the input of this layer is an output of a previous one,so
        //references mechanizm can be used to connect layer with it's input
        public void add_input_full_connection(float[,] newinput)
        {
            inputs.Add(newinput);
        }

        public void connect_input_with_dropout(float[,] new_input)
        {
            float[,] dropped_input = new float[w, h];

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; j++)
                {
                    if (MatrixOperations.random_generator.NextDouble() > 0.5)
                    {
                        dropped_input[i, j] = new_input[i, j];
                    }

                }
            }

            add_input_full_connection(dropped_input);
        }

        public void connect_input_with_topology(float[,] new_input, float[,] topology)
        {
            float[,] dropped_input = new float[w, h];

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; j++)
                {
                    dropped_input[i, j] = new_input[i, j] * topology[i, j];

                }
            }

            add_input_full_connection(dropped_input);
        }

        //convolution+normalization
        public void get_output()
        {
            foreach (var inp in inputs)
            { 
                convolution(inp);
                Normalization.n_linear(output, outputwidth, outputheight);
                Normalization.n_sigmoidal(output, outputwidth, outputheight, 5);
 
            }
        }


        void convolution(float[,] input)
        {
            float[,] f = ConvFuncs.fold(input, weights, outputwidth, outputheight, w, h);
            for (int j = 0; j < outputheight; j++)
            {
                for (int i = 0; i < outputwidth; i++)
                {
                    non_activated_stage[i, j] = f[i, j] + b;
                    output[i, j] = ActFuncs.f_act_sigma(f[i, j] + b);
                }
            }
        }


        #region Learning

        public void get_map_error_from_subsampling(float[,] sigma_next_layer)
        {//W transp*sigma_prev*f_derived(ul)
            float[,] upsampled_next = ConvFuncs.upsample(sigma_next_layer, outputwidth, outputheight);
            for (int j = 0; j < outputheight; j++)
            {
                for (int i = 0; i < outputwidth; i++)
                {
                    error[i, j] = upsampled_next[i, j] * ActFuncs.f_act_sigma_deriv(non_activated_stage[i, j]);

                }
            }
        }

        //get error from only connected next convolutional map
        public void get_map_error_from_convolution(ConvolutionFeatureMap next_l_fm)
        {//W transp*sigma_prev*f_derived(ul)
            this.error = new float[outputwidth, outputheight];
            //1) get deconvolution (back fold) of next layer's error
            float[,] summfold = ConvFuncs.back_fold(next_l_fm.error, next_l_fm.weights, next_l_fm.outputwidth, next_l_fm.outputheight, next_l_fm.w, next_l_fm.h);
            for (int j = 0; j < outputheight; j++)
            {
                for (int i = 0; i < outputwidth; i++)
                {
                    error[i, j] = ActFuncs.f_act_linear_deriv(non_activated_stage[i, j]) * summfold[i, j];
                    b += error[i, j];
                }
            }
        }

        public void correct_weights()
        {
            foreach (var input in inputs)
            {
                float[,] folderr = ConvFuncs.fold_with_transponed_kernel(input, error, w, h, outputwidth, outputheight);
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        weights[i, j] += folderr[j, i];
                        b += error[i, j];
                    }
                }
            }
        }

        #endregion


    }
}
