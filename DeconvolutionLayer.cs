using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class DeconvolutionLayer
    {
        public int feature_maps_number;
        //can be different. depends on processing methods(valid/same/full) and boundary effects
        public int map_width;
        public int map_height;
        //one filter's size
        public int kwidth;
        public int kheight;
        public int proportion;
        public int inp_h;
        public int inp_w;
        //feature maps-maps,uses one weight matrix
        //kernels called also filters or neurons
        public List<DeconvolutionFeatureMap> feature_maps;
        public List<float[,]> inputs;
        public List<float[,]> avg_inputs;

        #region Creation

        public DeconvolutionLayer(UpSamplingLayer prev_upsampling_l, ConvolutionLayer base_convolution_layer)
        {

            this.feature_maps_number = prev_upsampling_l.feature_maps_number;
            this.feature_maps = new List<DeconvolutionFeatureMap>();
            this.inputs = new List<float[,]>();
            this.avg_inputs = new List<float[,]>();
            this.proportion = prev_upsampling_l.feature_maps_number / base_convolution_layer.feature_maps_number;

            this.kwidth = base_convolution_layer.kwidth;
            this.kheight = base_convolution_layer.kheight;
            //boundaries effect
            this.map_width = kwidth + base_convolution_layer.map_width - 1;
            this.map_height = kheight+base_convolution_layer.map_height - 1;
            this.inp_w = prev_upsampling_l.outputwidth;
            this.inp_h = prev_upsampling_l.outputwidth;
            //create kernels
            //previous layer must contain more feature maps or same number of feature maps
            for (int k = 0; k < base_convolution_layer.feature_maps_number; k++)
            {
                avg_inputs.Add(new float[inp_w, inp_h]);
                feature_maps.Add(new DeconvolutionFeatureMap(avg_inputs[k],base_convolution_layer.feature_maps[k]));
            }
               for (int k = 0; k < prev_upsampling_l.feature_maps_number; k++)
                {
                    inputs.Add(prev_upsampling_l.feature_maps[k].output);
                }

        }


        #endregion

        public void get_outputs()
        {
            if(feature_maps.Count>0)
            {
            int counter = 0;

            for (int k = 0; k < feature_maps_number; k++)
            {
                for (int l = 0; l < proportion; l++)
                {

                    for (int j = 0; j <inp_h; j++)
                    {
                        for (int i = 0; i < inp_w; i++)
                        {
                            avg_inputs[k][i, j] += inputs[counter][i, j]; 
                        }
                    }
                    counter++;
                }

                for (int j = 0; j < inp_h; j++)
                {
                    for (int i = 0; i < inp_w; i++)
                    {
                        avg_inputs[k][i, j] = avg_inputs[k][i, j] / proportion;
                    }
                }

             feature_maps[k].get_output();
            }
        
        }
        }
    }
}

      