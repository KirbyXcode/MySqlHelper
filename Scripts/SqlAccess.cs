﻿using UnityEngine; 
using System; 
using System.Data; 
using System.Collections;  
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;  

public class SqlAccess
{
    public static MySqlConnection dbConnection;
    
    //如果只是在本地的话，写localhost就可以。
    static string host = "localhost"; 

    ////如果是局域网，那么写上本机的局域网IP
    //static string host = "qdm16206948.my3w.com" ;
    static string id = "root" ;
    static string pwd = "root" ;
    static string database = "mygamedb" ;

    public SqlAccess()
    {
        Initialize();
    }
  
    /// <summary>
    /// 新建并打开连接
    /// </summary>
    public static void Initialize()
    {
        try 
        {
            string connectionString = string .Format("Server = {0};port={4};Database = {1}; User ID = {2}; Password = {3};",host,database,id,pwd, "3306");
            dbConnection = new MySqlConnection (connectionString);
            dbConnection.Open();
        }
        catch (Exception e)
        {
            throw new Exception( "服务器连接失败，请重新检查是否打开MySql服务。" + e.Message.ToString()); 
        }
    }

    /// <summary>
    /// 创建表
    /// 参数：表名称、属性列、 属性列类型
    /// </summary>
    public DataSet Create (string _TableName, string[] _ColsName, string [] _ColsType)
    {
        if (_ColsName.Length != _ColsType.Length) 
        {
            throw new Exception ( "columns.Length != colType.Length" );
        }
        try
        {
            string query = "CREATE TABLE " + _TableName + " (" + _ColsName[0] + " " + _ColsType[0];
  
            for (int i = 1; i < _ColsName.Length; ++i)
            {
                query += ", " + _ColsName[i] + " " + _ColsType[i];
            }
            query += ")";
            Debug.Log("~~创建成功~~" );
            return ExecuteQuery(query);
        }
        catch 
        {
            return null ;
        }
    }
  
    /// <summary>
    /// 创建表 自动ID
    /// 参数：表名称、属性列、 属性列类型
    /// </summary>
    public DataSet CreateAutoID (string _TableName, string[] _ColsName, string [] _ColsType)
    {
        if (_ColsName.Length != _ColsType.Length) 
        {
            throw new Exception ( "columns.Length != colType.Length" );
        }
        try 
        {
            string query = "CREATE TABLE " + _TableName + " (" + _ColsName[0] + " " + _ColsType[0] + " NOT NULL AUTO_INCREMENT";
            for (int i = 1; i < _ColsName.Length; ++i)
            {
                query += ", " + _ColsName[i] + " " + _ColsType[i];
            }
            query += ", PRIMARY KEY (" + _ColsName[0] + ")" + ")";
            Debug.Log("~~创建成功~~" );
            return ExecuteQuery(query);
        }
        catch 
        {
            return null ;
        }
    }
  
    /// <summary>
    /// 插入数据
    /// 参数：表名称、值
    /// 特点：不适用自动ID
    /// </summary>
    public DataSet Insert (string _TableName, string[] _Values)
    {
        try 
        {
            string query = "INSERT INTO " + _TableName + " VALUES (" + _Values[0];
  
            for (int i = 1; i < _Values.Length; ++i) 
            {
                query += ", " + _Values[i];
            }
            query += ")";
            Debug.Log("~~添加成功~~" );
            return ExecuteQuery(query);
        }
        catch
        {
            Debug.Log("添加失败，请检查错误后重新再试" );
            return null ;
        }
    }
  
    /// <summary>
    /// 插入数据
    /// 参数：表名称、属性列、值
    /// 特点：可选择性添加值
    /// </summary>
    public DataSet Insert (string _TableName, string[] _ColsName, string [] _Values)
    {
        try 
        {
            if (_ColsName.Length != _Values.Length) 
            {
                throw new Exception( "columns.Length != colType.Length" );
            }
            string query = "INSERT INTO " + _TableName + " (" + _ColsName[0];
            for (int i = 1; i < _ColsName.Length; ++i) 
            {
                query += ", " + _ColsName[i];
            }
  
            query += ") VALUES (" + _Values[0];
            for (int i = 1; i < _Values.Length; ++i)
            {
                query += ", " + _Values[i];
            }
            query += ")";
            Debug.Log("~~添加成功~~" );
            return ExecuteQuery(query);
        }
        catch 
        {
            Debug.Log("添加失败，请检查错误后重新再试" );
            return null ;
        }
    }
  
    /// <summary>
    /// 查询数据
    /// 参数：表名称、查询内容
    /// </summary>
    public DataSet Select(string _TableName, string _Select)
    {
        try
        {
            string query = "select " + _Select + " from " + _TableName;
            return ExecuteQuery(query);
        }
        catch
        {
            Debug.Log("查询失败，请检查错误后重新再试" );
            return null ;
        }
    }
  
    /// <summary>
    /// 查询数据
    /// 参数：表名称、查询内容、查询条件
    /// </summary>
    public DataSet Select(string _TableName, string _Select, string _Condition)
    {
        try 
        {
            string query = "select " + _Select + " from " + _TableName + " where " + _Condition;
            return ExecuteQuery(query);
        }
        catch
        {
            Debug.Log("查询失败，请检查错误后重新再试" );
            return null ;
        }
    }
  
    /// <summary>
    /// 修改数据
    /// 参数：表名称、准备修改的属性列、准备修改的属性列的值、条件信息属性列、条件信息属性列的值
    /// </summary>
    public DataSet Update(string _TableName, string[] _SetColsName, string[] _SetColsValues, string _ConditionColName, string _ConditionColValue)
    {
        try 
        {
            string query = "UPDATE " + _TableName + " SET " + _SetColsName[0] + " = " + _SetColsValues[0];
            for (int i = 1; i < _SetColsValues.Length; ++i) 
            {
                query += ", " + _SetColsName[i] + " =" + _SetColsValues[i];
            }
            query += " WHERE " + _ConditionColName + " = " + _ConditionColValue + " ";
            Debug.Log("~~修改成功~~" );
            return ExecuteQuery(query);
        }
        catch 
        {
            Debug.Log("修改失败，请检查错误后重新再试" );
            return null ;
        }
    }
  
    /// <summary>
    /// 删除数据
    /// 参数：表名称、属性列、值
    /// </summary>
    public DataSet Delete(string _TableName, string [] _ColsName,string [] _ColsValue)
    {
        try 
        {
            string query = "DELETE FROM " + _TableName + " WHERE " + _ColsName[0] + " = " + _ColsValue[0];
            for (int i = 1; i < _ColsValue.Length; ++i)
            {
                query += " or " + _ColsName[i] + " = " + _ColsValue[i];
            }
            Debug.Log("~~删除成功~~" );
            return ExecuteQuery(query);
        }
        catch 
        {
            Debug.Log("删除失败，请检查错误后重新再试" );
            return null ;
        }
    }
  
    public  void Close()
    {
        if(dbConnection != null ) 
        {
            dbConnection.Close();
            dbConnection.Dispose();
            dbConnection = null;
        }
    }
  
    public static DataSet ExecuteQuery(string sqlString)
    {
        if (dbConnection.State == ConnectionState .Open) 
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
                da.Fill(ds);
            }
            catch (Exception ee) 
            {
                throw new Exception( "SQL:" + sqlString + "/n" + ee.Message.ToString());
            }
            return ds;
        }
        return null ;
    }
}
