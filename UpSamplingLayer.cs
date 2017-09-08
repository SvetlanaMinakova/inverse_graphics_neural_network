using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    class UpSamplingLayer
    {

        //number of feature_maps
        public int feature_maps_number;
        //can be different. depends on processing methods(valid/same/full) and boundary effects
        public int outputwidth;
        public int outputheight;
        public List<float[,]> outputs;
        public List<UpSampleFeatureMap> feature_maps;
        int inputw;
        int inputh;

        #region Creation

        public UpSamplingLayer(SubsamplingLayer inp_subsampling_layer)
        {
            this.feature_maps_number = inp_subsampling_layer.feature_maps_number;
            this.outputs = new List<float[,]>();
            this.feature_maps = new List<UpSampleFeatureMap>();
            this.inputh = inp_subsampling_layer.outputheight;
            this.inputw = inp_subsampling_layer.outputwidth;
            //decompression koef=2
            this.outputwidth = inputw * 2;
            this.outputheight = inputh * 2;
            //one-to-one-connecntion
            for (int j = 0; j < feature_maps_number; j++)
            {
                this.feature_maps.Add(new UpSampleFeatureMap(outputwidth,outputheight,inp_subsampling_layer.feature_maps[j].output));
            }
        }

        public UpSamplingLayer(DeconvolutionLayer inp_deconv_layer,int outputmaps_num)
        {
            this.feature_maps_number = outputmaps_num;
            this.outputs = new List<float[,]>();
            this.feature_maps = new List<UpSampleFeatureMap>();
            this.inputh = inp_deconv_layer.map_height;
            this.inputw = inp_deconv_layer.map_width;
            //decompression koef=2
            this.outputwidth = inp_deconv_layer.map_width* 2;
            this.outputheight = inp_deconv_layer.map_height * 2;
            int proportion = inp_deconv_layer.feature_maps_number/outputmaps_num ;
            //average-to-one-connection
            //averaging

                for (int j = 0; j < feature_maps_number*proportion; j++)
                {
                    this.feature_maps.Add(new UpSampleFeatureMap(outputwidth, outputheight, inp_deconv_layer.feature_maps[j].output));
                }

        }

        public void get_outputs()
       {
           this.outputs.Clear();
            for (int k = 0; k < feature_maps.Count; k++)
            {
                feature_maps[k].get_output();
                this.outputs.Add(feature_maps[k].output);

            }
         }
        #endregion
    }
}
