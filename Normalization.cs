using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class Normalization
    {
        public static void n_linear(float[,] input,int w,int h)
        {
            float _max=0;
            float _min=1;

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (_max < input[i, j])
                        _max = input[i, j];
                    if (_min > input[i, j])
                        _min = input[i, j];
                }
            
            }
            float max_dif=_max-_min;

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    input[i,j]=(input[i,j]-_min)/max_dif;
                }

            }
        
        }

        public static void n_sigmoidal(float[,] input,int w,int h,float alpha)
        {
            
            float _max=0;
            float _min=1;

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (_max < input[i, j])
                        _max = input[i, j];
                    if (_min > input[i, j])
                        _min = input[i, j];
                }
            
            }
            float xc=(_min+_max)/2;

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    input[i,j]=(float)(1/(Math.Exp((-1)*alpha*(input[i,j]-xc))+1));
                }

            }
        
        }


 
        }

    }

