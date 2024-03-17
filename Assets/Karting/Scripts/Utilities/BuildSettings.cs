using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;


namespace KartGame

{
    [CreateAssetMenu(menuName = "KartGame/BuildSettings")]
    public class BuildSettings : ScriptableObject
    {

        public WebGLBuildSettings settings = new WebGLBuildSettings();
        public string buildName;
        public BuildType buildType;
        public string shaderType;
    }

    
    public enum BuildType
    {
        DevelopmentUncompressed,
        ReleaseGzipped,
        ReleaseUncompressed,
        Profiling,
        CpuProfiler,
        MemoryProfiler
    }
    
    [Serializable]
    public class WebGLBuildSettings
    {
        public bool developmentBuild;
        [FormerlySerializedAs("autoconnectProfiler")] public bool autoConnectProfiler;
        public GraphicsDeviceType[] graphicsDeviceSettings;
        public bool halfResolution;
        
        public GraphicsDeviceType[] SetGraphicsDevices(bool webGL1, bool webGL2)
        {
            return webGL1 && webGL2
                ? new[] {GraphicsDeviceType.OpenGLES3, GraphicsDeviceType.Vulkan}
                : new[] {webGL1 ? GraphicsDeviceType.Vulkan : GraphicsDeviceType.OpenGLES3};
        }

        public string GetName()
        {
            string name = "";
            for (int i = 0; i < graphicsDeviceSettings.Length; i++)
            {
                name += "_" + graphicsDeviceSettings[i].ToString();
            }

            if (autoConnectProfiler) name += "_profiler";
            if (halfResolution) name += "_halfRes";
            return name;
        }
        
        public WebGLBuildSettings(bool developmentBuild = false,
            bool autoConnectProfiler = false, bool halfResolution = false,  GraphicsDeviceType[] graphicsDeviceSettings = null)
        {
            this.graphicsDeviceSettings = graphicsDeviceSettings ?? SetGraphicsDevices(true,true);
            this.developmentBuild = developmentBuild;
            this.halfResolution = halfResolution;
            this.autoConnectProfiler = autoConnectProfiler;
        }
    }
}