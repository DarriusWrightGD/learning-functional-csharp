﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    public class AuditManager
    {
        //private readonly int _maxEntriesPerFile;

        //public AuditManager(int maxEntriesPerFile)
        //{
        //    _maxEntriesPerFile = maxEntriesPerFile;
        //}

        //public void AddRecord(string currentFile, string vistorName, DateTime timeOfVisit)
        //{
        //    var lines = File.ReadAllLines(currentFile);

        //    if (lines.Length < _maxEntriesPerFile)
        //    {
        //        var lastIndex = int.Parse(lines.Last().Split(';')[0]);
        //        var newLine = $"{lastIndex + 1};{vistorName};{timeOfVisit.ToString("s")}";
        //        File.AppendAllLines(currentFile, new[] { newLine });
        //    }
        //    else
        //    {
        //        var newLine = $"1;{vistorName};{timeOfVisit.ToString("s")}";
        //        var newFileName = GetNewFileName(currentFile);
        //        File.WriteAllLines(newFileName, new[] { newLine });
        //    }
        //}

        //private string GetNewFileName(string existingFileName)
        //{
        //    string fileName = Path.GetFileNameWithoutExtension(existingFileName);
        //    int index = int.Parse(fileName.Split('_')[1]);
        //    return $"Audit_{index + 1}.txt";
        //}

        //public void RemoveMentionsAbout(string visitorName, string directoryName)
        //{
        //    foreach (var filename in Directory.GetFiles(directoryName))
        //    {
        //        string tempFile = Path.GetTempFileName();
        //        var linesToKeep = File
        //            .ReadLines(filename)
        //            .Where(line => line.Contains(visitorName))
        //            .ToList();

        //        if (linesToKeep.Count == 0)
        //        {
        //            File.Delete(filename);
        //        }
        //        else
        //        {
        //            File.WriteAllLines(tempFile, linesToKeep);
        //            File.Delete(filename);
        //            File.Move(tempFile, filename);
        //        }
        //    }
        //}

        private readonly int _maxEntriesPerFile;

        public AuditManager(int maxEntriesPerFile)
        {
            _maxEntriesPerFile = maxEntriesPerFile;
        }

        public FileAction AddRecord(FileContent currentFile, string vistorName, DateTime timeOfVisit)
        {
            var entries = Parse(currentFile.Content);

            if (entries.Count < _maxEntriesPerFile)
            {
                entries.Add(new AuditEntry(entries.Count + 1, vistorName, timeOfVisit));
                var newContent = Serialize(entries);

                return new FileAction(currentFile.FileName, newContent, ActionType.Update);
            }
            else
            {
                var entry = new AuditEntry(1, vistorName, timeOfVisit);
                var newContent = Serialize(new List<AuditEntry> { entry });
                var newFileName = GetNewFileName(currentFile.FileName);

                return new FileAction(newFileName, newContent, ActionType.Create);
            }
        }

        private List<AuditEntry> Parse(string[] content)
        {
            var result = new List<AuditEntry>();

            foreach (var line in content)
            {
                var data = line.Split(';');
                result.Add(new AuditEntry(int.Parse(data[0]), data[1], DateTime.Parse(data[2])));
            }

            return result;
        }

        private string [] Serialize(List<AuditEntry> entries)
        {
            return entries
                .Select(entry => $"{entry.Number};{entry.Visitor};{entry.TimeOfVisit.ToString("s")}")
                .ToArray();
        }

        private string GetNewFileName(string existingFileName)
        {
            string fileName = Path.GetFileNameWithoutExtension(existingFileName);
            int index = int.Parse(fileName.Split('_')[1]);
            return $"Audit_{index + 1}.txt";
        }

        public IReadOnlyList<FileAction> RemoveMentionsAbout(string visitorName, FileContent [] files)
        {
            return files
                .Select(file=> RemoveMentionsIn(file,visitorName))
                .ToList();
        }
        
        private FileAction RemoveMentionsIn(FileContent file, string visitorName)
        {
            var entries = Parse(file.Content);
            var newContent = entries
                .Where(x => !x.Visitor.Contains(visitorName))
                .Select((entry, index) => new AuditEntry(index + 1, entry.Visitor, entry.TimeOfVisit))
                .ToList();

            if(!newContent.Any())
            {
                return new FileAction(file.FileName, new string[0], ActionType.Delete);
            }

            return new FileAction(file.FileName, Serialize(newContent), ActionType.Update);
        }
    }

    public struct AuditEntry
    {
        public readonly int Number;
        public readonly string Visitor;
        public readonly DateTime TimeOfVisit;

        public AuditEntry(int number, string visitor, DateTime timeOfVisit)
        {
            Number = number;
            Visitor = visitor;
            TimeOfVisit = timeOfVisit;
        }

    }

    public struct FileAction
    {
        public readonly string FileName;
        public readonly string[] Content;
        public readonly ActionType Type;

        public FileAction(string fileName, string[] content, ActionType type)
        {
            FileName = fileName;
            Content = content;
            Type = type;
        }

    }

    public enum ActionType
    {
        Create,
        Update,
        Delete
    }

    public struct FileContent
    {
        public readonly string FileName;
        public readonly string[] Content;

        public FileContent(string fileName, string [] content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
