using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using YamlDotNet.RepresentationModel;

namespace YAMLDotNet_temporary_project {
    class Program {
        static void Main() {
            string path = @"D:\Me\Menu\main.yml";
            StreamReader reader = new StreamReader(path, Encoding.UTF8);
            YamlStream stream = new YamlStream();
            stream.Load(reader);
            YamlDocument doc = stream.Documents[0];

            // One single-line node is a scalar node
            // The whole content of a single-line node is a mapping node
            // Each single-line node in a mapping node is a scalar node
            // A scalar node has key and value
            // 
            // Example:
            //   example1:
            //     type: 'example'
            //     number: 1
            //     content:
            //     - ex
            //   example2:
            //     type: 'example'
            //     number: 2
            //   exercise1:
            //     type: 'exercise'
            //     number: 1
            //     content:
            //     - assignment
            //   exercise2:
            //     type: 'exercise'
            //     number: 2
            //     
            // "Example" is a scalar node
            // "example1: {type: example, number: 1}, example2: {type: example, number: 2}, exercise1: {type: exercise, number: 1}, exercise2: {type: exercise, number: 2}"
            // is a mapping node (of the scalar node "Example")
            // 
            // "type" is a scalar node (of the mapping node "example1")
            // "example" is a scalar node (of the mapping node "example1")
            // "number" is a scalar node (of the mapping node "example1")
            // "1" is a scalar node (of the mapping node "example1")
            // The whole content of node "content" (of the mapping node "example1") is a sequence node
            // 
            // Root node of a YamlDocument object is a mapping node, containing the whole content of the document

            short aliasNodes = 0, mappingNodes = 0, scalarNodes = 0, sequenceNodes = 0;

            foreach (YamlNode yamlNode in doc.RootNode.AllNodes)
                switch (yamlNode.NodeType) {
                    case YamlNodeType.Alias:
                        aliasNodes++;
                        break;
                    case YamlNodeType.Mapping:
                        mappingNodes++;
                        break;
                    case YamlNodeType.Scalar:
                        scalarNodes++;
                        break;
                    case YamlNodeType.Sequence:
                        sequenceNodes++;
                        break;
                }

            Console.WriteLine($"{aliasNodes} alias nodes");
            Console.WriteLine($"{mappingNodes} mapping nodes");
            Console.WriteLine($"{scalarNodes} scalar nodes");
            Console.WriteLine($"{sequenceNodes} sequence nodes");
            Console.WriteLine(Environment.NewLine);

            YamlMappingNode root = (YamlMappingNode)doc.RootNode; // Root node is actually a mapping node, but it still needs casting
            foreach (var child in root.Children) {
                YamlScalarNode scalarNode = (YamlScalarNode)child.Key;
                Console.WriteLine($"(Key)\t[{child.Key.NodeType}] {child.Key}");
                Console.WriteLine($"(Value)\t[{child.Value.NodeType}] {child.Value}");
                Console.WriteLine(Environment.NewLine);

                // A child of a mapping node is like a line of scalar nodes
                // It has key and value
                // Example: 'like this' <== a child, whose key is "Example" and value is "like this"

                YamlMappingNode mappingNode = (YamlMappingNode) child.Value;
                foreach (var node in mappingNode.Children) {
                    Console.WriteLine($"(Key)\t\t[{node.Key.NodeType}] {node.Key}");
                    Console.WriteLine($"(Value)\t\t[{node.Value.NodeType}] {node.Value}");
                }

                Console.WriteLine(Environment.NewLine + Environment.NewLine);
            }

            Console.ReadKey();
        }
    }
}

