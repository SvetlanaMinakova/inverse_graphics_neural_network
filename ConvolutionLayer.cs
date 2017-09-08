using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    public class ConvolutionLayer
    {
        public int feature_maps_number;
        //can be different. depends on processing methods(valid/same/full) and boundary effects
        public int map_width;
        public int map_height;
        float[] bj;
        //one filter's size
        public int kwidth;
        public int kheight;
        //feature maps-maps,uses one weight matrix
        //kernels called also filters or neurons
        public List<ConvolutionFeatureMap> feature_maps;
        public List<float[,]> errors;
        public string next_l_type;

        #region Creation

        public ConvolutionLayer(int f_maps_number, int k_w, int k_h, int map_w, int map_h)
        {
            //default value
            this.next_l_type = "subsampling";

            this.feature_maps_number = f_maps_number;
            this.bj = new float[f_maps_number];
            //bj=1 initially
            for (int j = 0; j < f_maps_number; j++)
                bj[j] = 0.01f;
            this.feature_maps = new List<ConvolutionFeatureMap>();
            this.errors = new List<float[,]>();
            this.kwidth = k_w;
            this.kheight = k_h;
            //if filter<map =>"valid" bounadary mode, else - full-mode
            if (map_w > kwidth)
                this.map_width = map_w - kwidth + 1;
            if (map_h > kheight)
                this.map_height = map_h - kheight + 1;

            //create kernels
            for (int k = 0; k < feature_maps_number; k++)
            {
                ConvolutionFeatureMap fm = new ConvolutionFeatureMap(kwidth, kheight, map_width, map_height);
                MatrixOperations.init_matrix_random(fm.weights, kwidth, kheight);
                feature_maps.Add(fm);

                errors.Add(new float[map_width, map_height]);
            }

        }

        public ConvolutionLayer(ConvolutionLayer prev_convolution, int k_w, int k_h)
        {
            //default value
            this.next_l_type = "convolution";
            this.feature_maps_number = prev_convolution.feature_maps_number;
            this.bj = new float[this.feature_maps_number];
            //bj=1 initially
            for (int j = 0; j < this.feature_maps_number; j++)
                bj[j] = 0.01f;
            this.feature_maps = new List<ConvolutionFeatureMap>();
            this.errors = new List<float[,]>();
            this.kwidth = k_w;
            this.kheight = k_h;
            //if filter<map =>"valid" bounadary mode, else - full-mode
            this.map_width = prev_convolution.map_width;
            this.map_height = prev_convolution.map_height;
            if (this.map_width > kwidth)
                this.map_width = -(kwidth - 1);
            if (this.map_height > kheight)
                this.map_height -= (kheight - 1);

            //create kernels
            for (int k = 0; k < feature_maps_number; k++)
            {
                ConvolutionFeatureMap fm = new ConvolutionFeatureMap(kwidth, kheight, map_width, map_height);
                MatrixOperations.init_matrix_random(fm.weights, kwidth, kheight);
                feature_maps.Add(fm);

                errors.Add(new float[map_width, map_height]);
            }
            //link with previous layer
            //all-to-all connection

            for (int i = 0; i < this.feature_maps_number; i++)
            {   //link prev layers output
                this.feature_maps[i].add_input_full_connection(prev_convolution.feature_maps[i].output);
                //link this error input path
            }


        }

        #endregion

        public void get_feature_maps()
        {
            for (int j = 0; j < feature_maps_number; j++)
                feature_maps[j].get_output();

        }

        #region Learning
        public void getError(SubsamplingLayer next_layer)
        {
            for (int j = 0; j < feature_maps.Count; j++)
            {
                feature_maps[j].get_map_error_from_subsampling(next_layer.errors[j]);
                errors[j] = feature_maps[j].error;
                bj[j] = feature_maps[j].b;

            }
        }

        public void getError(ConvolutionLayer next_layer)
        {
            //GETTING ERROR FROM NEXT CONVOLUTINAL LAYER!
            for (int j = 0; j < feature_maps.Count; j++)
            {
                // feature_maps[j].get_map_error_from_convolution();
                errors[j] = feature_maps[j].error;
                bj[j] = feature_maps[j].b;

            }
        }

        public void correct_weights()
        {
            for (int j = 0; j < feature_maps.Count; j++)
            {//all to all link
                feature_maps[j].correct_weights();
            }
        }
        #endregion

    }
}