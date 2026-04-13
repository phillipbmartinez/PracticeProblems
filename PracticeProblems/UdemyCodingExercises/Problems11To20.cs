using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UdemyCodingExercises
{
    internal class Problems11To20
    {
    }

    // #11: Read file content safely
    // Implement the TryReadFile method that takes a file name and attempts to read it using IFileSystem.ReadFile.
    // If the file exists, return its contents.
    // If the file does not exist (throws FileNotFoundException), log a warning (using ILogger) with the exception message and rethrow the exception.The warning can be a string in any format you like, but it must contain the exception's Message property.
    // Whether the file is found or not, always log "Attempted to read file: [filename]" using the ILogger.
    public class Exercise
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;

        public Exercise(IFileSystem fileSystem, ILogger logger)
        {
            _fileSystem = fileSystem;
            _logger = logger;
        }

        public string TryReadFile(string fileName)
        {
            try
            {
                return _fileSystem.ReadFile(fileName);
            }
            catch (FileNotFoundException ex)
            {
                _logger.Log(ex.Message);
                throw;
            }
            finally
            {
                _logger.Log($"Attempted to read file: {fileName}");
            }
        }
    }

    // For problem #11
    public interface IFileSystem
    {
        string ReadFile(string fileName);
    }

    // For problem #11
    public interface ILogger
    {
        void Log(string message);
    }
}
