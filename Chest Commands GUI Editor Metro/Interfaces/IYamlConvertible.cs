namespace CCGE_Metro.Interfaces {
    interface IYamlConvertible {
        /// <summary>
        /// Returns an array of string that represents the current object.
        /// </summary>
        /// <param name="useYamlParser">Indicates whether a <see cref="YamlDotNet.Serialization.Serializer"/> is used.</param>
        /// <returns></returns>
        string[] ToYamlText(bool useYamlParser);
        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.Dictionary{YamlDotNet.RepresentationModel.YamlNode, YamlDotNet.RepresentationModel.YamlNode}"/> of <see cref="YamlDotNet.RepresentationModel.YamlNode"/>s.
        /// </summary>
        /// <returns></returns>
        System.Collections.Generic.Dictionary<YamlDotNet.RepresentationModel.YamlNode, YamlDotNet.RepresentationModel.YamlNode> ToYamlDictionary();
        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.KeyValuePair{YamlDotNet.RepresentationModel.YamlScalarNode, YamlDotNet.RepresentationModel.YamlMappingNode}"/> of <see cref="YamlDotNet.RepresentationModel.YamlScalarNode"/>s and <seealso cref="YamlDotNet.RepresentationModel.YamlMappingNode"/>s.
        /// </summary>
        /// <returns></returns>
        System.Collections.Generic.KeyValuePair<YamlDotNet.RepresentationModel.YamlScalarNode, YamlDotNet.RepresentationModel.YamlMappingNode> ToYamlSection();
    }
}
