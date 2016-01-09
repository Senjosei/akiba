﻿using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Akiba
{
    class Configuration
    {
        public const string ConfigurationName = "configuration.yaml";

        public ushort FramesPerSecond { get; private set; } = 60;
        public ushort RenderingResolutionWidth { get; private set; } = 1920;
        public ushort RenderingResolutionHeight { get; private set; } = 1080;
        public bool Fullscreen { get; private set; } = true;
        public bool VerticalSynchronization { get; private set; } = false;
        public bool AntiAliasing { get; private set; } = false;
        public bool HideCursor { get; private set; } = false;

        public Configuration Save()
        {
            var serializer = new Serializer(SerializationOptions.EmitDefaults, new CamelCaseNamingConvention());

            using (var streamWriter = new StreamWriter(ConfigurationName))
            {
                serializer.Serialize(streamWriter, this);
            }

            return this;
        }

        public static Configuration LoadFromFile()
        {
            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());

            using (var streamReader = new StreamReader(ConfigurationName))
            {
                return deserializer.Deserialize<Configuration>(streamReader);
            }
        }
    }   
}
