using NamedEntityRecognizer.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NamedEntityRecognizer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cancellationToken = new();

            // var modelPath = @"<root_folder>\bert-base-NER";
            var modelPath = @"finer_model";

            var textFilePath = @"sample_texts.txt";

            var outputFilePath = Path.Combine(Path.GetDirectoryName(textFilePath), Path.GetFileNameWithoutExtension(textFilePath) + "_predictions.txt");

            Console.WriteLine(modelPath);

            //var sentence = @"Revolving Facility The Revolving Facility has capacity of $ 400.0 million .".ToLower();

            var configuration = new Configuration(modelPath, numberOfTokens: 5)
            {
                HasTokenTypeIds = false
            };

            var textContent = await File.ReadAllTextAsync(textFilePath);

            var sentences = textContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Ensure the output file is empty before starting to write
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }


            var nerProcessor = new NerProcessor(configuration);

            using (var writer = new StreamWriter(outputFilePath, append: true))
            {

                foreach (var sentence in sentences)
                {
                    var cleanedSentence = sentence.Trim().ToLower();

                    

                    var result = await nerProcessor.ProcessAsync(cleanedSentence, cancellationToken.Token);


                    await writer.WriteLineAsync($"{cleanedSentence}");



                    Console.WriteLine("");
                    Console.WriteLine($"{cleanedSentence}");

                    foreach (var p in result)
                    {
                        await writer.WriteLineAsync($"{p.Token} = {p.Label}");

                        Console.WriteLine($"{p.Token} = {p.Label}");
                    }

                    await writer.WriteLineAsync(""); // Extra line for readability between sentences
                }
            }


            //var result = await new NerProcessor(configuration)
            //                    .ProcessAsync(sentence, cancellationToken.Token);

            //result?.ForEach(p =>
            //{
            //    Console.WriteLine("{0}={1}", p.Token, p.Label);
            //});

            Console.WriteLine("");
            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
