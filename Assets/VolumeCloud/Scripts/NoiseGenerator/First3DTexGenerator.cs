using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yangrc.VolumeCloud {
    [System.Serializable]
    public class First3DTexGenerator : ITextureGenerator {

        float RemapClamped(float original_value, float original_min, float original_max, float new_min, float new_max) {
            return new_min + (Mathf.Clamp01((original_value - original_min) / (original_max - original_min)) * (new_max - new_min));
        }

        public int texResolution = 32;
        public int perlinOctaves = 4;
        public int channel1PerlinPeriod = 16;
        public int channel2WorleyPeriod = 16;
        
        public Color Sample(Vector3 pos) {
            Color res = new Color();

            float perlin = 0.0f;
            perlin = PerlinNoiseGenerator.OctaveNoise(pos, channel1PerlinPeriod, perlinOctaves);

            float worley = 0.0f;
            worley += WorleyNoiseGenerator.OctaveNoise(pos, channel2WorleyPeriod, 3);
            
            var finalResult = RemapClamped(perlin, -worley, 1.0f, 0.0f, 1.0f);
            res.r = finalResult;
            return res;
        }
    }

    [System.Serializable]
    public class Second3DTexGenerator : ITextureGenerator {
        public int texResolution = 32;
        public int channel1WorleyFreq = 16;
        public int channel2WorleyFreq = 32;
        public int channel3WorleyFreq = 64;
        public Color Noise(Vector3 pos) {
            Color res = new Color();
            res.r = WorleyNoiseGenerator.OctaveNoise(pos, channel1WorleyFreq, 3);
            return res;
        }

        public Color Sample(Vector3 pos) {
            return Noise(pos);
        }
    }
}