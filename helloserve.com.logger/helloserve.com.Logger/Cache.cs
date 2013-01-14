﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

namespace helloserve.com
{
    internal class ALogRethrowException : Exception
    {
        public ALogRethrowException() : base() { }

        public ALogRethrowException(string message) : base(message) { }

        public ALogRethrowException(string message, Exception innerException) : base(message, innerException) { }
    }

    internal class CategoryCache
    {
        private List<LogElement> _elements;
        private long _size;

        internal CategoryCache()
        {
            _elements = new List<LogElement>();
            _size = 0;
        }

        internal long SizeInBytes
        {
            get { return _size; }
        }

        internal void PushElement(LogElement element)
        {
            _size += element.Size();
            _elements.Add(element);
        }

        internal void Clear()
        {
            _elements.Clear();
            _size = 0;
        }

        internal List<LogElement> Items
        {
            get { return _elements; }
        }
    }

    internal class Cache
    {
        private object _lockCache = new Object();

        private Dictionary<string, PropertyInfo[]> _propertyCache;
        private Dictionary<string, CategoryCache> _categoriesAlpha;
        private Dictionary<string, CategoryCache> _categoriesBeta;

        private Dictionary<string, CategoryCache> _categories;

        private long _size;

        private DateTime _lastPush;
        private double _elapsedMinutes;
        private int _pushCount;

        private double _pushesPerMinute;

        /// <summary>
        /// Event when the cache wants to dump immediately.
        /// </summary>
        internal event EventHandler DumpNow;

        internal Cache()
        {
            _propertyCache = new Dictionary<string, PropertyInfo[]>();

            _categoriesAlpha = new Dictionary<string, CategoryCache>();
            _categoriesBeta = new Dictionary<string, CategoryCache>();
            _categories = _categoriesAlpha;

            _lastPush = DateTime.Now;
        }

        internal long TotalSizeInBytes
        {
            get { return _size; }
        }

        internal double LogsPerMinutes
        {
            get { return _pushesPerMinute; }
        }

        internal double LogCapacity
        {
            get { return _pushesPerMinute / 60.0D; }
        }

        internal void PushElement(LogElement element)
        {
            lock (_lockCache)
            {
                string category = element.Category;
                if (!_categories.ContainsKey(category))
                    _categories.Add(category, new CategoryCache());
                else
                    _size -= _categories[category].SizeInBytes;

                _categories[category].PushElement(element);
                _size += _categories[category].SizeInBytes;

                //work out the logs per minutes
                _pushCount++;
                TimeSpan elapsed = DateTime.Now - _lastPush;
                _elapsedMinutes += elapsed.TotalMinutes;

                if (_elapsedMinutes >= 1)
                {
                    double ratio = 1 / _elapsedMinutes;
                    double pushes = (double)_pushCount * ratio;

                    _pushesPerMinute = pushes;
                    _pushCount -= (int)pushes;
                    _elapsedMinutes -= 1;
                }

                _lastPush = DateTime.Now;

                if (LogsPerMinutes < 30 && DumpNow != null)
                    DumpNow(this, EventArgs.Empty);
            }
        }

        internal void Dump(SqlConnection connection, string tableName, string category = null)
        {
            if (_categories == null)
                return;

            Dictionary<string, CategoryCache> cacheToDump = _categories;

            lock (_lockCache)
            {
                //first we need to switch over to the other cache
                if (_categories == _categoriesAlpha)
                    _categories = _categoriesBeta;
                else
                    _categories = _categoriesAlpha;
            }

            List<LogElement> _allItems = new List<LogElement>();

            foreach (string cat in cacheToDump.Keys)
            {
                if (cat == category || string.IsNullOrEmpty(category))
                    _allItems.AddRange(cacheToDump[cat].Items);
            }

            foreach (LogElement item in _allItems)
            {
                Type itemType = item.GetType();

                PropertyInfo[] properties = null;

                //check the cache before we do the expensive bit
                if (_propertyCache.ContainsKey(itemType.FullName))
                    properties = _propertyCache[itemType.FullName];
                else
                {
                    properties = itemType.GetProperties();
                    _propertyCache.Add(itemType.FullName, properties);
                }

                //we need to now build up the list of parameters
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (PropertyInfo prop in properties)
                {
                    if (prop.GetIndexParameters().Length == 0)
                        parameters.Add(prop.Name, prop.GetValue(item, null));
                }

                try
                {
                    //let the user do some stuff
                    item.FillParams(parameters);
                }
                catch (Exception ex)
                {
                    throw new ALogRethrowException("Exception in your implementation of 'LogElement.FillParams(Dictionary<string, object>)'", ex);
                }

                //finally save it
                Save(parameters, connection, tableName);
            }

            foreach (string cat in cacheToDump.Keys)
            {
                if (cat == category || string.IsNullOrEmpty(category))
                    cacheToDump[cat].Clear();
            }
        }

        internal void Save(Dictionary<string, object> parameters, SqlConnection connection, string tableName)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();
            List<SqlParameter> sqlParams = new List<SqlParameter>();

            foreach (string param in parameters.Keys)
            {
                columns.Append(param);
                columns.Append(",");

                values.Append("@");
                values.Append(param);
                values.Append(",");

                if (parameters[param] == null)
                    sqlParams.Add(new SqlParameter(param, DBNull.Value));
                else
                    sqlParams.Add(new SqlParameter(param, parameters[param]));
            }
            columns.Remove(columns.Length - 1, 1);
            values.Remove(values.Length - 1, 1);

            //build the SQL string
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append(tableName);
            sql.Append(" (");
            sql.Append(columns);
            sql.Append(") VALUES (");
            sql.Append(values);
            sql.Append(")");

            using (SqlCommand command = new SqlCommand(sql.ToString(), connection))
            {
                command.Parameters.AddRange(sqlParams.ToArray());
                int rows = command.ExecuteNonQuery();
                if (rows != 1)
                    throw new ArgumentException("Log affected zero rows while dumping");
            }
        }
    }
}
