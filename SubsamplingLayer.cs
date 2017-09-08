using System;
using System.Collections.Generic;
using System.Text;

namespace Convolution_testing
{
    public class SubsamplingLayer
    {
        //number of feature_maps
        public int feature_maps_number;
        //can be different. depends on processing methods(valid/same/full) and boundary effects
        public int outputwidth;
        public int outputheight;
        public float[] bj;
        float[] aj;
        public List<float[,]>inputs;
        public List<float[,]> errors;
        public List<float[,]> outputs;
        public List<SubSampleFeatureMap> feature_maps;
        int inputw;
        int inputh;

        #region Creation

        public SubsamplingLayer(ConvolutionLayer prev_conv_layer)
        {

            this.feature_maps_number = prev_conv_layer.feature_maps_number;
            bj = new float[feature_maps_number];
            aj = new float[feature_maps_number];
            this.inputs = new List<float[,]>();
            this.outputs = new List<float[,]>();
            //full connecntion
            for (int j = 0; j < feature_maps_number; j++)
            {
                this.inputs.Add(prev_conv_layer.feature_maps[j].output);

            }
            this.inputw = prev_conv_layer.map_width;
            this.inputh = prev_conv_layer.map_height;

            this.feature_maps = new List<SubSampleFeatureMap>();
            this.errors = new List<float[,]>();
            //compression koef=2
            //"valid" boundary mode
            this.outputwidth = inputw / 2;
            this.outputheight = inputh / 2;

            for (int j = 0; j < feature_maps_number; j++)
            {   //aj=bj=1 initially
                bj[j] = 0.01f;
                this.aj[j] = 1;
                feature_maps.Add(new SubSampleFeatureMap(inputw, inputh,outputwidth,outputheight,inputs[j]));
                errors.Add(new float[outputwidth, outputheight]);
            }
        }

       //the input of this layer is an output of a previous one,so
       //references mechanizm can be used to connect layer with it's input
       public void connect_inputs(List<float[,]> newinputs)
       { for(int i=0;i<feature_maps_number;i++)
           this.inputs[i] = newinputs[i];
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
        #region Learning
        public void get_errors_from_mpl(List<float[,]> packed_errors_from_mpl)
        {
            for (int j = 0; j < feature_maps_number; j++)
            {//h and w are swaped because the error matrix is transposed 
                feature_maps[j].getmapErrorFromMpl(packed_errors_from_mpl[j]);
                errors[j] = feature_maps[j].error;
                bj[j] = feature_maps[j].b;
                feature_maps[j].ChangeA();
                aj[j] = feature_maps[j].a;
            }
        
        }

        public void set_link_with_conv_next_layer(ConvolutionLayer next_layer,int currentLayerMapId, List<int> connectedMapsIds)
        {
            for (int i = 0; i < connectedMapsIds.Count; i++)
               feature_maps[currentLayerMapId].conv_maps_next_layer.Add(next_layer.feature_maps[connectedMapsIds[i]]);

        }

        public void get_errors_from_convolution()
        {

                for (int j = 0; j < this.feature_maps_number; j++)
                {
                    feature_maps[j].get_map_error_from_convolution();
                    errors[j] = feature_maps[j].error;
                    feature_maps[j].ChangeA();
                    bj[j] = feature_maps[j].b;
                    aj[j] = feature_maps[j].a;
            }
            
        }


        #endregion


    }
}
