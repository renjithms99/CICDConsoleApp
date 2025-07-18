using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; // For String.Join

public class FileCombiner
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to File Combiner!");

        // --- Configuration ---
        string inputFolderPath = @"E:\FailedEvents\FailedEventsUpTo20250711133812201"; // IMPORTANT: Change this to your folder path
        string outputFilePath = @"C:\OutPut\CombinedOutput.csv"; // IMPORTANT: Change this to your desired output file path
        string fileExtensionFilter = "*.json"; // e.g., "*.txt", "*.csv", or "*" for all files
        string delimiter = ","; // The character to use between file contents

        Console.WriteLine($"Input Folder: {inputFolderPath}");
        Console.WriteLine($"Output File: {outputFilePath}");
        Console.WriteLine($"Looking for files with pattern: {fileExtensionFilter}");
        Console.WriteLine($"Combining with delimiter: '{delimiter}'");
        Console.WriteLine("\n--- Starting Process ---");

        try
        {
            // 1. Validate input folder
            if (!Directory.Exists(inputFolderPath))
            {
                Console.WriteLine($"Error: Input folder '{inputFolderPath}' does not exist.");
                return;
            }

            // Ensure output directory exists
            string? outputDirectory = Path.GetDirectoryName(outputFilePath);
            if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
                Console.WriteLine($"Created output directory: {outputDirectory}");
            }

            // 2. Get all files matching the filter
            string[] files = Directory.GetFiles(inputFolderPath, fileExtensionFilter, SearchOption.TopDirectoryOnly);

            if (files.Length == 0)
            {
                Console.WriteLine($"No files found in '{inputFolderPath}' matching '{fileExtensionFilter}'.");
                // Optional: Delete previous output file if no new files
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                    Console.WriteLine("Deleted existing output file as no new files were found.");
                }
                return;
            }

            // 3. Read content of each file
            List<string> allFileContents = new List<string>();
            foreach (string filePath in files)
            {
                Console.WriteLine($"Reading file: {Path.GetFileName(filePath)}");
                string content = File.ReadAllText(filePath);
                allFileContents.Add(content);
            }

            // 4. Combine contents with the specified delimiter
            string combinedContent = string.Join(delimiter, allFileContents);

            // 5. Write to the output file
            File.WriteAllText(outputFilePath, combinedContent);

            Console.WriteLine("\n--- Process Completed Successfully ---");
            Console.WriteLine($"Combined content written to: {outputFilePath}");
            Console.WriteLine($"Total files processed: {files.Length}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }

        Console.WriteLine("\nPress any key to exit.");
        Console.ReadKey();
    }
}