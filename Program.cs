﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class Program
    {
        
        static void Main(string[] args)
        {

            //A
            float[] inPut1 = new float[]   { 
254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 
254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 
254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 
254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 
254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 
254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 
254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 
254 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 254 , 
0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 
};

            //код целевого выхода
            float[] target1 = new float[] { 0, 0, 1 };

            //DOG
            float[] inPut2 = new float[]   { 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
};

            


            float[] target2 = new float[] { 0, 1, 0 };

            // BIRD
            float[] inPut3 = new float[]   { 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 254 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 
};


            float[] target3 = new float[] { 1, 0, 0 };
            //


           float[,] inp1 = new float[24, 24];
            float[,] inp2 = new float[24, 24];
            float[,] inp3 = new float[24, 24];

            for (int j = 0; j < 24; j++)
            {
                for (int i = 0; i < 24; i++)
                {
                    inp1[i, j] = inPut1[i + 24 * j] / 256;
                    inp2[i, j] = inPut2[i + 24 * j] / 256;
                    inp3[i, j] = inPut3[i + 24 * j] / 256;
                }
            }

          List <LearningPair> all_lps = new List<LearningPair>();
          all_lps.Add(new LearningPair(24, 24, 3, inp1, target1));
          all_lps.Add(new LearningPair(24, 24, 3, inp2, target2));
          all_lps.Add(new LearningPair(24, 24, 3, inp3, target3));

          Network test_netw = new Network(24, 24, 1, 3, 3, 4, 2, 2);

          //  test_netw.Learn(all_lps, 0.001f, 5000);


             test_netw.send_front_signal(inp1);
        /*     test_netw.send_front_signal(inp1);
             test_netw.send_front_signal(inp1);

             test_netw.send_front_signal(inp2);
             test_netw.send_front_signal(inp2);
             test_netw.send_front_signal(inp2);

             test_netw.send_front_signal(inp3);
             test_netw.send_front_signal(inp3);
             test_netw.send_front_signal(inp3);
       //      test_netw.send_front_signal(inp2);
         //    test_netw.send_front_signal(inp3);
         
            /*
            int k_w=3;
            int k_h=3;
            float[,]kernel = new float[k_w,k_h];
            int counter = 1;

            for (int j = 0; j < k_h; j++)
            {
                for (int i = 0; i < k_w; i++)
                {
                    kernel[i, j] = counter;
                    counter++;
                }
            }

            float[,] foldresult = ConvFuncs.fold(inp2, kernel,22,22,k_w,k_h);
            float[,] unfoldresult = ConvFuncs.back_fold(foldresult, kernel,24, 24, k_h, k_w);

            MatrixOperations.save_matrix_to_file("kernel", kernel, k_w, k_h);
            MatrixOperations.save_matrix_to_file("input", inp2, 24, 24);
            MatrixOperations.save_matrix_to_file("fold", foldresult, 22, 22);
            MatrixOperations.save_matrix_to_file("unfold", unfoldresult, 24, 24);
             */
            Console.ReadLine();
        }

    }
}
