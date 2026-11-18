using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using MiftahFix.Models;

namespace MiftahFix.Services
{
    public sealed class HistoryService
    {
        private const string ApplicationFolderName =
            "MiftahFix";

        private const string DataFolderName =
            "Data";

        private const string HistoryFileName =
            "history.json";

        private const int MinimumItemsCount =
            10;

        private const int MaximumItemsCount =
            1000;

        private readonly object _syncRoot =
            new object();

        private readonly JsonSerializerOptions
            _jsonOptions;

        public HistoryService()
        {
            _jsonOptions =
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };
        }

        public string HistoryDirectoryPath =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder
                        .LocalApplicationData),
                ApplicationFolderName,
                DataFolderName);

        public string HistoryFilePath =>
            Path.Combine(
                HistoryDirectoryPath,
                HistoryFileName);

        public IReadOnlyList<HistoryEntry> Load()
        {
            lock (_syncRoot)
            {
                List<HistoryEntry> entries =
                    LoadInternal();

                return entries
                    .Select(
                        entry =>
                            entry.Clone())
                    .ToList()
                    .AsReadOnly();
            }
        }

        public HistoryEntry Add(
            HistoryEntry entry,
            int maxItems)
        {
            ArgumentNullException.ThrowIfNull(
                entry);

            lock (_syncRoot)
            {
                int normalizedMaxItems =
                    NormalizeMaxItems(
                        maxItems);

                HistoryEntry normalizedEntry =
                    entry.Clone();

                normalizedEntry.Normalize();

                List<HistoryEntry> entries =
                    LoadInternal();

                entries.RemoveAll(
                    item =>
                        item.Id ==
                        normalizedEntry.Id);

                entries.Insert(
                    0,
                    normalizedEntry);

                TrimToMaximumCount(
                    entries,
                    normalizedMaxItems);

                SaveInternal(
                    entries);

                return normalizedEntry.Clone();
            }
        }

        public HistoryEntry Add(
            string originalText,
            string convertedText,
            string conversionMode,
            string source,
            int maxItems)
        {
            HistoryEntry entry =
                HistoryEntry.Create(
                    originalText,
                    convertedText,
                    conversionMode,
                    source);

            return Add(
                entry,
                maxItems);
        }

        public bool Delete(
            Guid entryId)
        {
            if (entryId ==
                Guid.Empty)
            {
                return false;
            }

            lock (_syncRoot)
            {
                List<HistoryEntry> entries =
                    LoadInternal();

                int removedCount =
                    entries.RemoveAll(
                        entry =>
                            entry.Id ==
                            entryId);

                if (removedCount == 0)
                {
                    return false;
                }

                SaveInternal(
                    entries);

                return true;
            }
        }

        public void Clear()
        {
            lock (_syncRoot)
            {
                SaveInternal(
                    new List<HistoryEntry>());
            }
        }

        public void ApplyMaximumItems(
            int maxItems)
        {
            lock (_syncRoot)
            {
                int normalizedMaxItems =
                    NormalizeMaxItems(
                        maxItems);

                List<HistoryEntry> entries =
                    LoadInternal();

                int originalCount =
                    entries.Count;

                TrimToMaximumCount(
                    entries,
                    normalizedMaxItems);

                if (entries.Count !=
                    originalCount)
                {
                    SaveInternal(
                        entries);
                }
            }
        }

        public bool TryLoad(
            out IReadOnlyList<HistoryEntry> entries,
            out string errorMessage)
        {
            try
            {
                entries =
                    Load();

                errorMessage =
                    string.Empty;

                return true;
            }
            catch (Exception exception)
            {
                entries =
                    Array.Empty<HistoryEntry>();

                errorMessage =
                    exception.Message;

                return false;
            }
        }

        public bool TryAdd(
            HistoryEntry entry,
            int maxItems,
            out HistoryEntry? savedEntry,
            out string errorMessage)
        {
            try
            {
                savedEntry =
                    Add(
                        entry,
                        maxItems);

                errorMessage =
                    string.Empty;

                return true;
            }
            catch (Exception exception)
            {
                savedEntry =
                    null;

                errorMessage =
                    exception.Message;

                return false;
            }
        }

        public bool TryDelete(
            Guid entryId,
            out bool wasDeleted,
            out string errorMessage)
        {
            try
            {
                wasDeleted =
                    Delete(
                        entryId);

                errorMessage =
                    string.Empty;

                return true;
            }
            catch (Exception exception)
            {
                wasDeleted =
                    false;

                errorMessage =
                    exception.Message;

                return false;
            }
        }

        public bool TryClear(
            out string errorMessage)
        {
            try
            {
                Clear();

                errorMessage =
                    string.Empty;

                return true;
            }
            catch (Exception exception)
            {
                errorMessage =
                    exception.Message;

                return false;
            }
        }

        private List<HistoryEntry> LoadInternal()
        {
            EnsureHistoryDirectoryExists();

            if (!File.Exists(
                    HistoryFilePath))
            {
                List<HistoryEntry> emptyEntries =
                    new List<HistoryEntry>();

                SaveInternal(
                    emptyEntries);

                return emptyEntries;
            }

            try
            {
                string json =
                    File.ReadAllText(
                        HistoryFilePath,
                        Encoding.UTF8);

                List<HistoryEntry>? entries =
                    JsonSerializer
                        .Deserialize<List<HistoryEntry>>(
                            json,
                            _jsonOptions);

                if (entries == null)
                {
                    throw new InvalidDataException(
                        "ملف سجل التحويلات فارغ أو غير صالح.");
                }

                NormalizeEntries(
                    entries);

                entries =
                    entries
                        .OrderByDescending(
                            entry =>
                                entry.CreatedAt)
                        .ToList();

                return entries;
            }
            catch
            {
                BackupInvalidHistoryFile();

                List<HistoryEntry> emptyEntries =
                    new List<HistoryEntry>();

                SaveInternal(
                    emptyEntries);

                return emptyEntries;
            }
        }

        private void SaveInternal(
            IReadOnlyCollection<HistoryEntry> entries)
        {
            EnsureHistoryDirectoryExists();

            List<HistoryEntry> normalizedEntries =
                entries
                    .Select(
                        entry =>
                        {
                            HistoryEntry clone =
                                entry.Clone();

                            clone.Normalize();

                            return clone;
                        })
                    .OrderByDescending(
                        entry =>
                            entry.CreatedAt)
                    .ToList();

            string json =
                JsonSerializer.Serialize(
                    normalizedEntries,
                    _jsonOptions);

            string temporaryFilePath =
                HistoryFilePath +
                ".tmp";

            try
            {
                File.WriteAllText(
                    temporaryFilePath,
                    json,
                    new UTF8Encoding(
                        encoderShouldEmitUTF8Identifier:
                            false));

                File.Move(
                    temporaryFilePath,
                    HistoryFilePath,
                    overwrite: true);
            }
            finally
            {
                if (File.Exists(
                        temporaryFilePath))
                {
                    try
                    {
                        File.Delete(
                            temporaryFilePath);
                    }
                    catch
                    {
                        // لا نوقف البرنامج إذا تعذر حذف الملف المؤقت.
                    }
                }
            }
        }

        private static void NormalizeEntries(
            List<HistoryEntry> entries)
        {
            entries.RemoveAll(
                entry =>
                    entry == null);

            foreach (HistoryEntry entry
                in entries)
            {
                entry.Normalize();
            }

            HashSet<Guid> usedIds =
                new HashSet<Guid>();

            for (int index =
                    entries.Count - 1;
                index >= 0;
                index--)
            {
                if (!usedIds.Add(
                        entries[index].Id))
                {
                    entries.RemoveAt(
                        index);
                }
            }
        }

        private static void TrimToMaximumCount(
            List<HistoryEntry> entries,
            int maxItems)
        {
            if (entries.Count <=
                maxItems)
            {
                return;
            }

            entries.RemoveRange(
                maxItems,
                entries.Count -
                maxItems);
        }

        private static int NormalizeMaxItems(
            int maxItems)
        {
            return Math.Clamp(
                maxItems,
                MinimumItemsCount,
                MaximumItemsCount);
        }

        private void EnsureHistoryDirectoryExists()
        {
            Directory.CreateDirectory(
                HistoryDirectoryPath);
        }

        private void BackupInvalidHistoryFile()
        {
            if (!File.Exists(
                    HistoryFilePath))
            {
                return;
            }

            try
            {
                string timestamp =
                    DateTime.Now.ToString(
                        "yyyyMMdd-HHmmss");

                string backupFilePath =
                    Path.Combine(
                        HistoryDirectoryPath,
                        $"history.invalid-{timestamp}.json");

                File.Copy(
                    HistoryFilePath,
                    backupFilePath,
                    overwrite: false);
            }
            catch
            {
                // لا نوقف البرنامج إذا تعذر إنشاء النسخة الاحتياطية.
            }
        }
    }
}