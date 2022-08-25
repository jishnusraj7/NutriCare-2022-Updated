
#region COPYRIGHTS 

/*	
    - - - - - - - - - - - - - - - - - - - - - - -
    File Name : DBHelper.cs
    - - - - - - - - - - - - - - - - - - - - - - -
    System				    :  	
    Module				    :  	DataLayer
    Author				    :	
    Date					:	1 July 2008
    Function				:	Define the Data Wrapper Class which provides the functionalities of DBProvider Factory.
    Desctiption	            :    
 */

#endregion

#region DIRECTIVES 

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
//using System.Data.SqlServerCe;
using System.Collections;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
//using MySql.Data.MySqlClient;


#endregion

namespace NutritionV1.Classes
{
    public class DBHelper
    {

        #region DECLARATIONS 

        private DbProviderFactory dbFactory;
        private DbConnection dbConnection;
        public DbCommand dbCommand;
        private DbParameter dbParameter;
        private DbTransaction dbTransaction;
        private bool isTransaction;

        //private static readonly string ConnectionString = "Data Source=" + Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["DBNAME"] + "Password = sql2005; Encrypt Database = TRUE";



        private static readonly string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["DBNAME"] + ";Jet OLEDB:Database Password=" + GetPassword();




        //private static readonly string ConnectionString = "Persist Security Info=False;database=nutrition;server=192.168.1.188;user id=orange; pwd=orangepwd";


        private static readonly string ProviderName = ConfigurationManager.AppSettings["SQLCEPROVIDER"];
        private static DBHelper instance; //Static Connection Object
         
        //private static string DBPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Nutrition.mdb";
        //conn = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + DBPath);
        //conn.Open();  



        #endregion

        #region ENUMERATORS 

        public enum TransactionType : uint
        {
            Open = 1,
            Commit = 2,
            Rollback = 3
        }

        #endregion

        #region STRUCTURES 

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	 
        ///Date				:	1 July 2008
        ///Input			:	
        ///OutPut			:	
        ///Comments			:	
        /// </summary>
        public struct Parameters
        {
            public string ParamName;
            public object ParamValue;
            public ParameterDirection ParamDirection;

            public Parameters(string name, object value, ParameterDirection direction)
            {
                ParamName = name;
                ParamValue = value;
                ParamDirection = direction;
            }

            public Parameters(string name, object value)
            {
                ParamName = name;
                ParamValue = value;
                ParamDirection = ParameterDirection.Input;
            }
        }

        #endregion

        #region CONSTRUCTOR 

        public DBHelper()
        {
            //dbFactory = SqlCeProviderFactory.Instance;
            dbFactory = OleDbFactory.Instance;
          //  dbFactory = MySqlClientFactory.Instance;
            EstablishFactoryConnection();
        }
        /// <summary>
        /// Date : 2008/10/23
        /// Make : DBHelper Instance
        /// </summary>
        public static DBHelper Instance
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        instance = new DBHelper();
                    }
                    else
                    {
                        if (instance.dbConnection.State == ConnectionState.Closed)
                        {
                            instance.dbConnection.ConnectionString = ConnectionString;
                            instance.dbConnection.Open();
                        }
                    }
                }
                catch (Exception ee)
                {
                    throw new Exception(ee.Message.ToString());
                }
                return instance;
            }
        }

        #endregion

        #region DESTRUCTOR 

        ~DBHelper()
        {
            //CloseFactoryConnection();
            try
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
                dbFactory = null;
            }
            catch
            {

            }

        }

        #endregion

        #region CONNECTIONS 

        ///<summary>
        ///Description	    :	This function is used to Open Database Connection
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	NA
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        public void EstablishFactoryConnection()
        {
            dbConnection = dbFactory.CreateConnection();

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
            }
        }

        private static string GetPassword()
        {
            return "!#%&(indo@$^*)";
        }            

        /// <summary>
        ///Description	    :	This function is used to Close Database Connection
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	NA
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        public void CloseFactoryConnection()
        {
            //check for an open connection            
            try
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            catch (DbException oDbErr)
            {
                //catch any SQL server data provider generated error message
                throw new Exception(oDbErr.Message);
            }
            catch (System.NullReferenceException oNullErr)
            {
                throw new Exception(oNullErr.Message);
            }
            finally
            {
                if (null != dbConnection)
                    dbConnection.Dispose();
            }
        }

        #endregion

        #region TRANSACTION 

        /// <summary>
        ///Description	    :	This function is used to Handle Transaction Events
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction Event Type
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        public void TransactionHandler(TransactionType transactionType)
        {
            switch (transactionType)
            {
                case TransactionType.Open:  //open a transaction
                    try
                    {
                        dbTransaction = dbConnection.BeginTransaction();
                        isTransaction = true;
                    }
                    catch (InvalidOperationException oErr)
                    {
                        throw new Exception("@TransactionHandler - " + oErr.Message);
                    }
                    break;

                case TransactionType.Commit:  //commit the transaction
                    if (null != dbTransaction.Connection)
                    {
                        try
                        {
                            dbTransaction.Commit();
                            isTransaction = false;
                        }
                        catch (InvalidOperationException oErr)
                        {
                            throw new Exception("@TransactionHandler - " + oErr.Message);
                        }
                    }
                    break;

                case TransactionType.Rollback:  //rollback the transaction
                    try
                    {
                        if (isTransaction)
                        {
                            dbTransaction.Rollback();
                        }
                        isTransaction = false;
                    }
                    catch (InvalidOperationException oErr)
                    {
                        throw new Exception("@TransactionHandler - " + oErr.Message);
                    }
                    break;
            }

        }

        #endregion

        #region COMMANDS 

        #region PARAMETERLESS METHODS 

        /// <summary>
        ///Description	    :	This function is used to Prepare Command For Execution
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	NA
        ///Comments			:	Has to be changed/removed if object based array concept is removed.
        /// </summary>
        private void PrepareCommand(bool blTransaction, CommandType cmdType, string cmdText)
        {

            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
            }

            //if (null == dbCommand)
            dbCommand = dbFactory.CreateCommand();

            dbCommand.Connection = dbConnection;
            dbCommand.CommandText = cmdText;
            dbCommand.CommandType = cmdType;

            if (blTransaction)
                dbCommand.Transaction = dbTransaction;
        }

        #endregion

        #region OBJECT BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to Prepare Command For Execution
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        private void PrepareCommand(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms)
        {

            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
            }

            //if (null == dbCommand)
            dbCommand = dbFactory.CreateCommand();

            dbCommand.Connection = dbConnection;
            dbCommand.CommandText = cmdText;
            dbCommand.CommandType = cmdType;

            if (blTransaction)
                dbCommand.Transaction = dbTransaction;

            if (null != cmdParms)
                CreateDBParameters(cmdParms);
        }

        #endregion

        #region STRUCTURE BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to Prepare Command For Execution
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        private void PrepareCommand(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {

            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.ConnectionString = ConnectionString;
                dbConnection.Open();
            }

            dbCommand = dbFactory.CreateCommand();
            dbCommand.Connection = dbConnection;
            dbCommand.CommandText = cmdText;
            dbCommand.CommandType = cmdType;

            if (blTransaction)
                dbCommand.Transaction = dbTransaction;

            if (null != cmdParms)
                CreateDBParameters(cmdParms);
        }

        #endregion

        #endregion

        #region PARAMETER METHODS 

        #region OBJECT BASED 

        /// <summary>
        ///Description	    :	This function is used to Create Parameters for the Command For Execution
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	2-Dimensional Parameter Array
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        private void CreateDBParameters(object[,] colParameters)
        {
            for (int i = 0; i < colParameters.Length / 2; i++)
            {
                dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = colParameters[i, 0].ToString();
                dbParameter.Value = colParameters[i, 1];
                dbCommand.Parameters.Add(dbParameter);
            }
        }

        #endregion

        #region STRUCTURE BASED 

        /// <summary>
        ///Description	    :	This function is used to Create Parameters for the Command For Execution
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	2-Dimensional Parameter Array
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        private void CreateDBParameters(Parameters[] colParameters)
        {
            for (int i = 0; i < colParameters.Length; i++)
            {
                Parameters param = (Parameters)colParameters[i];

                dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = param.ParamName;
                dbParameter.Value = param.ParamValue;
                dbParameter.Direction = param.ParamDirection;
                dbCommand.Parameters.Add(dbParameter);

            }
        }

        #endregion

        #endregion

        #region EXCEUTE METHODS 

        #region PARAMETERLESS METHODS 

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        ///                     Has to be changed/removed if object based array concept is removed.
        /// </summary>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            try
            {

                //EstablishFactoryConnection();
                PrepareCommand(false, cmdType, cmdText);
                return dbCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array, Clear Paramaeters
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        ///                     Has to be changed/removed if object based array concept is removed.
        /// </summary>
        public int ExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText)
        {
            try
            {
                PrepareCommand(blTransaction, cmdType, cmdText);
                int val = dbCommand.ExecuteNonQuery();

                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        #endregion

        #region OBJECT BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array, Clear Parameters
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        /// </summary>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {

                //EstablishFactoryConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();                
            }
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Count of Records Affected
        ///Comments			:	Overloaded method. 
        /// </summary>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return ExecuteNonQuery(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array, Clear Paramaeters
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        /// </summary>
        public int ExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {

                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Count of Records Affected
        ///Comments			:	Overloaded function. 
        /// </summary>
        public int ExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return ExecuteNonQuery(blTransaction, cmdType, cmdText, cmdParms, true);
        }

        #endregion

        #region STRUCTURE BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, Parameter Structure Array, Clear Parameters
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        /// </summary>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {

                //EstablishFactoryConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, Parameter Structure Array
        ///OutPut			:	Count of Records Affected
        ///Comments			:	Overloaded method. 
        /// </summary>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return ExecuteNonQuery(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, Parameter Structure Array, Clear Parameters
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        /// </summary>
        public int ExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {

                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Transaction, Command Type, Command Text, Parameter Structure Array
        ///OutPut			:	Count of Records Affected
        ///Comments			:	
        /// </summary>
        public int ExecuteNonQuery(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return ExecuteNonQuery(blTransaction, cmdType, cmdText, cmdParms, true);
        }

        #endregion

        #endregion

        #region READER METHODS 

        #region PARAMETERLESS METHODS 

        /// <summary>
        ///Description	    :	This function is used to fetch data using Data Reader	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Data Reader
        ///Comments			:	
        ///                     Has to be changed/removed if object based array concept is removed.
        /// </summary>
        public DbDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {

                //EstablishFactoryConnection();
                PrepareCommand(false, cmdType, cmdText);
                DbDataReader dr = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dbCommand.Parameters.Clear();
                return dr;

            }
            catch (Exception ex)
            {
                //CloseFactoryConnection();
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        #endregion

        #region OBJECT BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to fetch data using Data Reader	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Data Reader
        ///Comments			:	
        /// </summary>
        public DbDataReader ExecuteReader(CommandType cmdType, string cmdText, object[,] cmdParms)
        {

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work

            try
            {

                //EstablishFactoryConnection();                
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                DbDataReader dr = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dbCommand.Parameters.Clear();
                return dr;

            }
            catch (Exception ex)
            {
                //CloseFactoryConnection();
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        #endregion

        #region STRUCTURE BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to fetch data using Data Reader	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, Parameter AStructure Array
        ///OutPut			:	Data Reader
        ///Comments			:	
        /// </summary>
        public DbDataReader ExecuteReader(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {

                //EstablishFactoryConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                //CloseFactoryConnection();
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        #endregion

        #endregion

        #region ADAPTER METHODS 

        #region PARAMETERLESS METHODS 

        /// <summary>
        ///Description	    :	This function is used to fetch data using Data Adapter	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Data Set
        ///Comments			:	
        ///                     Has to be changed/removed if object based array concept is removed.
        /// </summary>
        public DataSet DataAdapter(CommandType cmdType, string cmdText)
        {

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            DbDataAdapter dda = null;
            try
            {
                //EstablishFactoryConnection();
                dda = dbFactory.CreateDataAdapter();
                PrepareCommand(false, cmdType, cmdText);

                dda.SelectCommand = dbCommand;
                DataSet ds = new DataSet();
                dda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        #endregion

        #region OBJECT BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to fetch data using Data Adapter	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Data Set
        ///Comments			:	
        /// </summary>
        public DataSet DataAdapter(CommandType cmdType, string cmdText, object[,] cmdParms)
        {

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            DbDataAdapter dda = null;
            try
            {
                //EstablishFactoryConnection();
                dda = dbFactory.CreateDataAdapter();
                PrepareCommand(false, cmdType, cmdText, cmdParms);

                dda.SelectCommand = dbCommand;
                DataSet ds = new DataSet();
                dda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        #endregion

        #region STRUCTURE BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to fetch data using Data Adapter	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Data Set
        ///Comments			:	
        /// </summary>
        public DataSet DataAdapter(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            DbDataAdapter dda = null;
            try
            {
                //EstablishFactoryConnection();
                dda = dbFactory.CreateDataAdapter();
                PrepareCommand(false, cmdType, cmdText, cmdParms);

                dda.SelectCommand = dbCommand;
                DataSet ds = new DataSet();
                dda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        #endregion

        #endregion

        #region SCALAR METHODS 

        #region PARAMETERLESS METHODS 

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	
        /// </summary>
        public object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            try
            {
                //EstablishFactoryConnection();

                PrepareCommand(false, cmdType, cmdText);

                object val = dbCommand.ExecuteScalar();

                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        #endregion

        #region OBJECT BASED PARAMETER ARRAY

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	
        /// </summary>
        public object ExecuteScalar(CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {

                //EstablishFactoryConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	Overloaded Method. 
        /// </summary>
        public object ExecuteScalar(CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return ExecuteScalar(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	
        /// </summary>
        public object ExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms, bool blDisposeCommand)
        {
            try
            {

                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	
        /// </summary>
        public object ExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, object[,] cmdParms)
        {
            return ExecuteScalar(blTransaction, cmdType, cmdText, cmdParms, true);
        }

        #endregion

        #region STRUCTURE BASED PARAMETER ARRAY 

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	
        /// </summary>
        public object ExecuteScalar(CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {
                //EstablishFactoryConnection();
                PrepareCommand(false, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
                //CloseFactoryConnection();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	Overloaded Method. 
        /// </summary>
        public object ExecuteScalar(CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return ExecuteScalar(cmdType, cmdText, cmdParms, true);
        }

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	
        /// </summary>
        public object ExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms, bool blDisposeCommand)
        {
            try
            {

                PrepareCommand(blTransaction, cmdType, cmdText, cmdParms);
                return dbCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (blDisposeCommand && null != dbCommand)
                    dbCommand.Dispose();
            }
        }

        /// <summary>
        ///Description	    :	This function is used to invoke Execute Scalar Method	
        ///Author			:	
        ///Date				:	1 July 2008
        ///Input			:	Command Type, Command Text, 2-Dimensional Parameter Array
        ///OutPut			:	Object
        ///Comments			:	
        /// </summary>
        public object ExecuteScalar(bool blTransaction, CommandType cmdType, string cmdText, Parameters[] cmdParms)
        {
            return ExecuteScalar(blTransaction, cmdType, cmdText, cmdParms, true);
        }

        public static string EncryptString(string p_sStr)
        {
            string sPwdRet = p_sStr.Trim();
            byte[] btPwd = GetCharArray(sPwdRet);
            ushort iUBArr = Convert.ToUInt16(btPwd.GetLength(0));
            sPwdRet = "";
            for (ushort iNo = 0; iNo < iUBArr; iNo++)
                sPwdRet += String.Format("{0:x2}", (btPwd[iNo] ^ 199)).PadLeft(2, '0');
            return (sPwdRet);
        }

        private static byte[] GetCharArray(string p_sStr)
        {
            Encoding ascii = Encoding.ASCII;
            char[] cStr = p_sStr.ToCharArray();
            byte[] btStr = ascii.GetBytes(cStr);
            return (btStr);
        }

        public static string DecryptString(string p_sStr)
        {
            string sPwdStr = p_sStr.Trim();
            byte[] btPwd = new byte[sPwdStr.Length / 2];
            ushort iUBArr = Convert.ToUInt16(sPwdStr.Length);
            for (ushort iNo = 0, iNum = 0; iNo < iUBArr; iNo += 2, iNum++)
            {
                btPwd[iNum] = Convert.ToByte(199 ^ Convert.ToByte(sPwdStr.Substring(iNo, 2), 16));
            }
            Encoding ascii = Encoding.ASCII;
            return (ascii.GetString(btPwd));
        }

        #endregion

        #endregion    

    }
}


