using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class Network
    {
        int iterations_number =0;
        int result_vector_lenght;
        string last_output = "";
        string last_error = "";
        int FL_feature_maps_number;
        int SL_feature_maps_number;
        ConvolutionLayer FirstLayer;
        SubsamplingLayer SecondLayer;
        ConvolutionLayer ThirdLayer;
        SubsamplingLayer FourthLayer;

        UpSamplingLayer FifthLayer;
        DeconvolutionLayer SixthLayer;
        UpSamplingLayer SeventhLayer;
        DeconvolutionLayer EightsLayer;


        //input is a picture's matrix
        public Network(int input_w, int input_h, int FL_feature_maps_number,
            int fl_kw, int fl_kh, int SL_feature_maps_number, int sl_kw, int sl_kh)
        {
            this.FL_feature_maps_number = FL_feature_maps_number;
            this.SL_feature_maps_number = SL_feature_maps_number;

            //create first convolutional layer(first layer)
            FirstLayer = new ConvolutionLayer(FL_feature_maps_number, fl_kw, fl_kh, input_w, input_h);

            //first subsampling layer(second layer)
            SecondLayer = new SubsamplingLayer(FirstLayer);

            //second convolutional layer(third layer).Consists of number of convolutional layers
            ThirdLayer = new ConvolutionLayer(SL_feature_maps_number, sl_kw, sl_kh, SecondLayer.outputwidth, SecondLayer.outputheight);
            //connect inputs with previous layer
                   //Parallel conctruction (like RGB) 2 cards of new layer for 1 card of previous layer
                   //like 6/3 = 2
                   //set topology for second layer
                   List<int> topology = new List<int>();
                   int counter = 0;
                   for (int i = 0; i < FL_feature_maps_number; i++)
                   {
                       for (int j = 0; j < SL_feature_maps_number / FL_feature_maps_number; j++)
                       {
                           topology.Add(counter);
                           counter++;
                       }
                       SecondLayer.set_link_with_conv_next_layer(ThirdLayer, i, topology);
                       topology.Clear();
                   }

                   //set topology for third layer
                   for (int i = 0; i < SL_feature_maps_number; i++)
                   {
                       int nextid = i * FL_feature_maps_number / SL_feature_maps_number;
                       ThirdLayer.feature_maps[i].add_input_full_connection(SecondLayer.feature_maps[nextid].output);
                   }



                   
/*
            //all-to-all connection
            List<int> topology = new List<int>();
            for (int i = 0; i < FL_feature_maps_number; i++)
            {
                for (int j = 0; j < SL_feature_maps_number; j++)
                {
                    topology.Add(j);
                }
                SecondLayer.set_link_with_conv_next_layer(ThirdLayer, i, topology);
                topology.Clear();
            }

            //set topology for third layer
            for (int j = 0; j < ThirdLayer.feature_maps_number; j++)
            {
                for (int i = 0; i < SecondLayer.feature_maps_number; i++)
                {
                    ThirdLayer.feature_maps[j].add_input_full_connection(SecondLayer.feature_maps[i].output);
                }
            }
            */

            FourthLayer = new SubsamplingLayer(ThirdLayer);

            FifthLayer = new UpSamplingLayer(FourthLayer);
            SixthLayer = new DeconvolutionLayer(FifthLayer, ThirdLayer);
            SeventhLayer = new UpSamplingLayer(SixthLayer, SecondLayer.feature_maps_number);
            EightsLayer = new DeconvolutionLayer(SeventhLayer, FirstLayer);

            
        }

        public void send_front_signal(float[,] input)
        {
            change_input(input);
        //    connect_input(input);
            send_signal_front();

            MatrixOperations.save_output_to_file("First layer", FirstLayer);
            MatrixOperations.save_output_to_file("Second layer", SecondLayer);
            MatrixOperations.save_output_to_file("Third layer", ThirdLayer);
            MatrixOperations.save_output_to_file("Fourth layer", FourthLayer);
            MatrixOperations.save_output_to_file("Fifth layer", FifthLayer);
            MatrixOperations.save_output_to_file("Sixth layer", SixthLayer);
            MatrixOperations.save_output_to_file("Seventh layer", SeventhLayer); 
            MatrixOperations.save_output_to_file("Eight layer", EightsLayer);

        }

        public void Learn(List<LearningPair> learning_pairs, float precision, float safe_counter)
        {
            iterations_number = 0;
            float prec = 1;
            string err = "";
            string outp = "";
            string expected = "";
            List<float[,]> errors_from_mpl = new List<float[,]>();
            for(int k=0; k< SL_feature_maps_number;k++)
            { errors_from_mpl.Add(new float[9,9]);
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                    errors_from_mpl[k][i, j] = ((-1)*j) / (k * (i + j)+1);
            }
            
            }

            while (iterations_number < safe_counter && prec > precision)
            {

                for (int lp = 0; lp < learning_pairs.Count; lp++)
                {
                    change_input(learning_pairs[lp].input);

          //          MatrixOperations.print_matrix("current input: " + lp.ToString(), FirstLayer.feature_maps[0].inputs[0], 16, 16);

         
                    iterations_number++;
                    send_signal_front();
                 /*   MatrixOperations.print_matrix("current input: " + lp.ToString(), FirstLayer.feature_maps[0].inputs[0], 8, 8);
                    MatrixOperations.print_matrix("current output[0]: " + lp.ToString(), FirstLayer.feature_maps[0].output, 6, 6);
                    MatrixOperations.print_matrix("subsampling input[0]: " + lp.ToString(), SecondLayer.feature_maps[0].input, 6, 6);
                    MatrixOperations.print_matrix("subsampled output[0]: " + lp.ToString(), SecondLayer.feature_maps[0].output, 3, 3);
                    MatrixOperations.print_matrix("current output[1]: " + lp.ToString(), FirstLayer.feature_maps[1].output, 6, 6);
                    MatrixOperations.print_matrix("subsampling input[1]: " + lp.ToString(), SecondLayer.feature_maps[1].input, 6, 6);
                    MatrixOperations.print_matrix("subsampled output[1]: " + lp.ToString(), SecondLayer.feature_maps[1].output, 3, 3);

                    MatrixOperations.print_matrix(" second convolution input1: " + lp.ToString(), ThirdLayer.feature_maps[0].inputs[0], 3, 3);
                    MatrixOperations.print_matrix(" second convolution input2: " + lp.ToString(), ThirdLayer.feature_maps[1].inputs[0], 3, 3);
                    MatrixOperations.print_matrix(" second convolution input3: " + lp.ToString(), ThirdLayer.feature_maps[2].inputs[0], 3, 3);
                    MatrixOperations.print_matrix(" second convolution input4: " + lp.ToString(), ThirdLayer.feature_maps[3].inputs[0], 3, 3);

                    MatrixOperations.print_matrix(" second convolution output1: " + lp.ToString(), ThirdLayer.feature_maps[0].output,2,2);
                    MatrixOperations.print_matrix(" second convolution output2: " + lp.ToString(), ThirdLayer.feature_maps[1].output, 2, 2);
                    MatrixOperations.print_matrix(" second convolution output3: " + lp.ToString(), ThirdLayer.feature_maps[2].output, 2, 2);
                    MatrixOperations.print_matrix(" second convolution output4: " + lp.ToString(), ThirdLayer.feature_maps[3].output, 2, 2);
                    MatrixOperations.print_matrix(" second subs input1: " + lp.ToString(), FourthLayer.feature_maps[0].input, 2, 2);
                    MatrixOperations.print_matrix(" second subs input2: " + lp.ToString(), FourthLayer.feature_maps[1].input, 2, 2);
                    MatrixOperations.print_matrix(" second subs input3: " + lp.ToString(), FourthLayer.feature_maps[2].input, 2, 2);
                    MatrixOperations.print_matrix(" second subs input4: " + lp.ToString(), FourthLayer.feature_maps[3].input, 2, 2);
                    MatrixOperations.print_matrix(" second subs output1: " + lp.ToString(), FourthLayer.feature_maps[0].output, 1, 1);
                    MatrixOperations.print_matrix(" second subs output2: " + lp.ToString(), FourthLayer.feature_maps[1].output, 1, 1);
                    MatrixOperations.print_matrix(" second subs output3: " + lp.ToString(), FourthLayer.feature_maps[2].output, 1, 1);
                    MatrixOperations.print_matrix(" second subs output4: " + lp.ToString(), FourthLayer.feature_maps[3].output, 1, 1);
                  */

                      send_signal_back(learning_pairs[lp].expected_output, errors_from_mpl);
                      correct_weights();
                  /*
                  err = "";
                    expected = "";
                    outp = "";

                    for (int i = 0; i < result_vector_lenght; i++)
                    {
                        err += output.error[i].ToString() + " | ";
                        outp += output.output[i].ToString() + " | ";
                        expected += learning_pairs[lp].expected_output[i].ToString() + " | ";

                    }
                    Console.WriteLine("\r\n + output: " + outp + "\r\n + expected: " + expected + "\r\n error: " + err+ "\r\n");
                    */

               
                }
            }

        }

        public void connect_input(float[,] input)
        {            //connect input with first layer
            for (int k = 0; k < FL_feature_maps_number; k++)
            {
                FirstLayer.feature_maps[k].add_input_full_connection(input);
            }
        }

        public void change_input(float[,] input)
        {            //connect input with first layer
            for (int k = 0; k < FL_feature_maps_number; k++)
            {
                FirstLayer.feature_maps[k].inputs.Clear();
                FirstLayer.feature_maps[k].add_input_full_connection(input);
            }
        }


        public void send_signal_front()
        {
            FirstLayer.get_feature_maps();
            SecondLayer.get_outputs();
            ThirdLayer.get_feature_maps();
            FourthLayer.get_outputs();
            FifthLayer.get_outputs();
            SixthLayer.get_outputs();
            SeventhLayer.get_outputs();
            EightsLayer.get_outputs();
        }

        public void send_signal_back(float[] expected_output,List<float[,]> errors_from_mpl)
        {
          
            FourthLayer.get_errors_from_mpl(errors_from_mpl);
       /*     MatrixOperations.print_matrix("fourth layer error[0] ", FourthLayer.errors[0], 1, 1);
            MatrixOperations.print_matrix("fourth layer error[1] ", FourthLayer.errors[1], 1, 1);
            MatrixOperations.print_matrix("fourth layer error[2] ", FourthLayer.errors[2], 1, 1);
            MatrixOperations.print_matrix("fourth layer error[3] ", FourthLayer.errors[3], 1, 1);
            MatrixOperations.print_matrix("fourth layer error[4] ", FourthLayer.errors[4], 1, 1);
            MatrixOperations.print_matrix("fourth layer error[5] ", FourthLayer.errors[5], 1, 1);
        */
          
            ThirdLayer.getError(FourthLayer);
            /*
            MatrixOperations.print_matrix("third layer error[0]: ", ThirdLayer.errors[0], 2, 2);
            MatrixOperations.print_matrix("third layer error[1]: ", ThirdLayer.errors[1], 2, 2);
            MatrixOperations.print_matrix("third layer error[2]: ", ThirdLayer.errors[2], 2, 2);
            MatrixOperations.print_matrix("third layer error[3]: ", ThirdLayer.errors[3], 2, 2);
            MatrixOperations.print_matrix("third layer error[4]: ", ThirdLayer.errors[4], 2, 2);
            MatrixOperations.print_matrix("third layer error[5]: ", ThirdLayer.errors[5], 2, 2);
             */

            SecondLayer.get_errors_from_convolution();
            
           /* MatrixOperations.print_matrix("sec layer error[0]: ", SecondLayer.errors[0], 3, 3);
            MatrixOperations.print_matrix("sec layer error[1]: ", SecondLayer.errors[1], 3, 3);
            */
             
            FirstLayer.getError(SecondLayer);
            
           /* MatrixOperations.print_matrix("first layer error[0]: ", FirstLayer.errors[0], 6, 6);
            MatrixOperations.print_matrix("first layer error[1]: ", FirstLayer.errors[1], 6, 6);
            */
              
        }

        public void correct_weights()
        {
            FirstLayer.correct_weights();
            ThirdLayer.correct_weights();

        }

    }
}
