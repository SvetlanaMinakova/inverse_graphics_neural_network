using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    public class SubSampleFeatureMap
    {
        public float[,] output;
        public float[,] non_activated_stage;
        public float[,] deriv_non_activated_stage;
        public float[,] error;
        public int w;
        public int h;
        public float a = 1;
        public float b = 0;
        //can be different. depends on processing methods(valid/same/full) and boundary effects
        public int outputwidth;
        public int outputheight;
        public float[,] input;
        public List<ConvolutionFeatureMap> conv_maps_next_layer;

        public SubSampleFeatureMap(int w,int h,int output_w,int output_h,float[,]input)
        {
            this.w = w;
            this.h = h;
            //boundaries effect
            this.outputwidth = output_w;
            this.outputheight = output_h;
            this.output = new float[outputwidth, outputheight];
            this.error = new float[outputwidth, outputheight];
            this.non_activated_stage = new float[outputwidth, outputheight];
            this.deriv_non_activated_stage=new float[outputwidth, outputheight];
            this.input = input;
            conv_maps_next_layer = new List<ConvolutionFeatureMap>();
        }

        public void get_output()
        {
            float[,] temp = subsample(this.input);

            for (int j = 0; j < outputheight; j++)
            {
                for (int i = 0; i < outputwidth; i++)
                {
                    non_activated_stage[i, j] = a * temp[i, j] + b;
                    output[i, j] = ActFuncs.f_act_linear(non_activated_stage[i, j]);
                    deriv_non_activated_stage[i, j] = ActFuncs.f_act_linear_deriv(non_activated_stage[i, j]);
                }
            }
        }


        // used: max-pooling and 1/2-compression. it can be different.
        float[,] subsample(float[,] x_prev)
        {
            float[,] temp = new float[outputwidth, outputheight];
            for (int j = 0; j < outputheight; j++)
            {
                for (int i = 0; i < outputwidth; i++)
                {
                    temp[i, j] = Math.Max(Math.Max(Math.Max(x_prev[i * 2, j * 2], x_prev[i * 2 + 1, j * 2]), x_prev[i * 2, j * 2 + 1]), x_prev[i * 2 + 1, j * 2 + 1]);
                }
            }
            return temp;
        }


         #region Learning
        //all-to-all connection
        public void  getmapErrorFromMpl(float[,] sigma_next_layer)
        {
            //clear error
            error = new float[outputwidth, outputheight];

                //sigma naxt layer = summ(err_nexl_layer * weight_next_layer)
                for (int j = 0; j < outputheight; j++)
                {
                    for (int i = 0; i < outputwidth; i++)
                    {
                        error[i, j] += sigma_next_layer[i, j] * ActFuncs.f_act_linear_deriv(non_activated_stage[i, j]);
                        b += error[i, j];
                    }
                }
            
        }

        public void ChangeA()
        {
            float[,] subs_inp = subsample(this.input);

            for (int j = 0; j < outputheight; j++)
            {for (int i = 0; i < outputwidth; i++)
                { a += error[i, j] * subs_inp[i, j]; }
            }
        }

        //get summary error from connected maps
        public void get_map_error_from_convolution()
        {//W transp*sigma_prev*f_derived(ul)
            this.error = new float[outputwidth, outputheight];
            if (conv_maps_next_layer.Count > 0)
            {
                float[,] summfold = new float[outputwidth, outputheight];
                List<float[,]> part_folds = new List<float[,]>();
                ConvolutionFeatureMap cur_nl_fm;
                //1) fold of next layer's maps with their transponed kernels
                for (int k = 0; k < conv_maps_next_layer.Count; k++)
                {
                    cur_nl_fm = conv_maps_next_layer[k];
                    part_folds.Add(ConvFuncs.fold_with_transponed_kernel(cur_nl_fm.error, cur_nl_fm.weights,
                      cur_nl_fm.outputwidth, cur_nl_fm.outputheight, cur_nl_fm.w, cur_nl_fm.h));

                    for (int j = 0; j < outputheight; j++)
                    {
                        for (int i = 0; i < outputwidth; i++)
                        { summfold[i, j] += part_folds[k][i, j]; }
                    }
                }

                for (int j = 0; j < outputheight; j++)
                {
                    for (int i = 0; i < outputwidth; i++)
                    {
                        error[i, j] = ActFuncs.f_act_linear_deriv(non_activated_stage[i, j]) * summfold[i, j];
                        b += error[i, j];
                    }
                }
            }
        }
#endregion


    }
}
