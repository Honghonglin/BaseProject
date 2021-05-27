using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks; 
using MySql.Data.MySqlClient;
using UnityEngine;
class DBHelper:BaseManager<DBHelper>
{

    private MySqlConnection sqlconnect = new MySqlConnection();
    public string connectString = "Database=tut;Data Source = 127.0.0.1;User Id=账号;Password=密码;port=3306;Characterset=utf8";
    /// <summary>
    /// 连接
    /// </summary>
    public void Connect() 
    {
        sqlconnect = new MySqlConnection(connectString);
        try
        {
            sqlconnect.Open();
        }
        catch (Exception e)
        {
            Debug.LogError("[DBHelper]Connect" + e.Message);
            return;
        }
    }


    /// <summary>
    /// 关闭连接
    /// </summary>
    public void Close()
    {
        if (sqlconnect.State != System.Data.ConnectionState.Open)
            Debug.LogWarning("未连接数据库就关闭了");
        try
        {
            sqlconnect.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("[DBHelper]Close" + e.Message);
            return;
        }
    }

    /// <summary>
    /// 查询  Dataset.Tables[tablenName].Rows[index][string/index]
    /// </summary>
    /// <param name="mysqlString">sql语句</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public DataSet Query(string mysqlString,string tableName)
    {
        DataSet ds = new DataSet();
        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mysqlString, sqlconnect);
        mySqlDataAdapter.Fill(ds, tableName);
        return ds;
    }
    /// <summary>
    /// 根据表名获得该表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public DataSet GetTableByName(string tableName)
    {
        DataSet dataSet = GetInstance().Query("select * from "+tableName, tableName);//前面是语句后面是表名字
        return dataSet;//表名，返回行数，列名。返回0行,name列的值
    }

    ///<summary>
    ///检查表中是否存在该行(单条件查询)
    /// </summary>
    /// 
    public DataSet CheckExit(string tableName,string columName,string value)
    {
        DataSet dataSet = GetInstance().Query("SELECT * FROM " + tableName + " WHERE " + columName + "=" + value, tableName);
        if (dataSet.Tables[tableName].Rows.Count != 0)
        {
            return dataSet;
        }
            
        return dataSet;
    }



}
