using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace helloserve.com.Logger
{
    public abstract class Scribe : IDisposable
    {
        public abstract void ScribeElement(LogElement element);

        public virtual void Dispose()
        {
        }
    }

    /// <summary>
    /// A basic scribe to write log elements via SqlClient
    /// </summary>
    public sealed class SqlCommandScribe : Scribe
    {
        private SqlConnection _connection;

        public SqlCommandScribe(string connectionString)
        {
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }
            catch { }
        }

        public override void ScribeElement(LogElement element)
        {
            try
            {
                SqlCommand command = element.Scribe(this);
                if (command == null)
                    return;

                command.Connection = _connection;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch { }
        }

        #region IDISPOSABLE

        public override void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// A basic scribe to write log elements to a file
    /// </summary>
    public sealed class FileScribe : Scribe
    {
        private class FileScribeLocker
        {
            public string FileName { get; set; }
        }

        private static ConcurrentDictionary<string, FileScribeLocker> _lockerDictionary = new ConcurrentDictionary<string, FileScribeLocker>();

        private static FileScribeLocker GetLocker(string filename)
        {
            FileScribeLocker locker = null;
            _lockerDictionary.TryGetValue(filename, out locker);

            if (locker == null)
            {
                locker = new FileScribeLocker() { FileName = filename };
                _lockerDictionary.AddOrUpdate(filename, locker, (k, v) => { v = locker; return v; });
            }

            return locker;
        }

        private FileInfo _fileInfo;

        public FileScribe(string filePath)
        {
            _fileInfo = new FileInfo(filePath);

            try
            {
                if (!_fileInfo.Directory.Exists)
                    _fileInfo.Directory.Create();

                if (!_fileInfo.Exists)
                    _fileInfo.Create();
            }
            catch { }
        }

        public override void ScribeElement(LogElement element)
        {
            lock (GetLocker(_fileInfo.FullName))
            {
                try
                {
                    _fileInfo.Refresh();
                    if (_fileInfo.Length > 3145728) //3 MB
                    {
                        FileInfo[] files = _fileInfo.Directory.GetFiles(string.Format("{0}*",_fileInfo.Name));
                        int last = files.Length - 1;
                        if (files.Length >= 10)
                        {
                            File.Delete(files[9].FullName);
                            last--;
                        }

                        if (files.Length == 1)
                        {
                            string destName = _fileInfo.FullName.Replace(_fileInfo.Name, string.Format("{0}{1}", _fileInfo.Name, 1));
                            File.Copy(_fileInfo.FullName, destName);
                        }
                        else
                        {
                            for (int i = last; i >= 0; i--)
                            {
                                string destName = files[i].FullName.Replace(files[i].Name, string.Format("{0}{1}", _fileInfo.Name, i + 1));
                                File.Copy(files[i].FullName, destName, true);
                            }
                        }

                        File.WriteAllText(_fileInfo.FullName, string.Empty);
                    }

                    string line = element.Scribe(this);
                    File.AppendAllText(_fileInfo.FullName, line, Encoding.UTF8);
                }
                catch { }
            }
        }
    }
}
