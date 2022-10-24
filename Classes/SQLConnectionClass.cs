using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace FileReadToSQLDB
{
    class SQLConnectionClass
    {
        private string _dataSource;
        private string _initialCatalog;
        private string _userID;
        private string _password;
        private bool _useIPAdress;

        public SQLConnectionClass(string dataSource, string initialCatalog, string userID, string password, bool useIPAdress)
        {
            this._dataSource = dataSource;
            this._initialCatalog = initialCatalog;
            this._userID = userID;
            this._password = password;
            this._useIPAdress = useIPAdress;
        }

        public string dataSource
        {
            get { return _dataSource; }
            set { this._dataSource = value; }
        }

        public string initialCatalog
        {
            get { return _initialCatalog; }
            set { this._initialCatalog = value; }
        }

        public string userID
        {
            get { return _userID; }
            set { this._userID = value; }
        }

        public string password
        {
            get { return _password; }
            set { this._password = value; }
        }

        public bool useIPAdress
        {
            get { return _useIPAdress; }
            set { this._useIPAdress = value; }
        }
    }
}