using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class ConvFuncs
    {
       
        public static float[,] fold(float[,] input, float[,] kernel,int output_w, int output_h, int k_w, int k_h)
        {
            float[,] foldresult = new float[output_w, output_h];
            float fold_cell = 0;
            for (int j = 0; j < output_h; j++)
            {
                for (int i = 0; i < output_w; i++)
                {
                    //summ k,l
                    for (int l = 0; l < k_h; l++)
                    {
                        for (int k = 0; k < k_w; k++)
                            fold_cell += input[i + k, j + l] * kernel[k, l];
                    }
                    foldresult[i, j] += fold_cell;
                    fold_cell = 0;
                }

            }
            return foldresult;
        }


        public static float[,] fold_with_transponed_kernel(float[,] input, float[,] kernel, int output_w, int output_h, int k_w, int k_h)
        {
            float[,] foldresult = new float[output_w, output_h];
            float fold_cell = 0;
            for (int j = 0; j < output_h; j++)
            {
                for (int i = 0; i < output_w; i++)
                {
                    //summ k,l
                    for (int l = 0; l < k_h; l++)
                    {
                        for (int k = 0; k < k_w; k++)
                            fold_cell += input[i + k, j + l] * kernel[l, k];
                    }
                    foldresult[i, j] += fold_cell;
                    fold_cell = 0;
                }

            }
            return foldresult;
        }


        public static float[,] back_fold(float[,] input, float[,] kernel, int output_w, int output_h,int k_w, int k_h)
        {
            float[,] foldresult = new float[output_w, output_h];
            //get rid of boundary "cutting" effect of fold;
            float[,] full_input = new float[output_w + k_w - 1, output_h + k_h - 1];

            for (int j = 0; j < output_h-k_h+1; j++)
            { //centering
                for (int i = 0; i < output_w-k_w+1; i++)
                { full_input[i + (k_h / 2), j + (k_w / 2)] = input[i, j]; }
            }


            float fold_cell = 0;
            for (int j = 0; j < output_h; j++)
            {
                for (int i = 0; i < output_w; i++)
                {
                    //summ k,l
                    for (int l = 0; l < k_w; l++)
                    {
                        for (int k = 0; k < k_h; k++)
                            fold_cell += full_input[k + i, l + j] * kernel[l, k];
                    }
                    foldresult[i, j] += fold_cell;
                    fold_cell = 0;
                }
            }
            return foldresult;
        }

        public static float[,] upsample(float[,] input,int output_w,int output_h)
        {
            float[,] result = new float[output_w, output_h];
            for (int j = 0; j < output_h - output_h % 2; j = j + 2)
            {
                for (int i = 0; i < output_w- output_w % 2; i = i + 2)
                {
                    result[i, j] = input[i / 2, j / 2];
                    result[i + 1, j] = input[i / 2, j / 2];
                    result[i, j + 1] = input[i / 2, j / 2];
                    result[i + 1, j + 1] = input[i / 2, j / 2];
                }
            }
            return result;
        }
    }
}
