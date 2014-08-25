using UnityEngine;  
using System;  
using System.Data;  
using System.Collections;   
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;

public class MySqlDB
{
	public MySqlConnection dbConnection;
	public string host = "localhost";
	public string id = "root";
	public string pwd = "root";
	public string database = "simulation";
	
	public MySqlDB()
	{
		OpenSql();
	}
	
	public void OpenSql()
	{		
		try
		{
			string connectionString = string.Format("Server = {0};port={4};Database = {1}; User ID = {2}; Password = {3};",host,database,id,pwd,"3306");
			dbConnection = new MySqlConnection(connectionString);
			dbConnection.Open();
		}
		catch (Exception e)
		{
			throw new Exception("服务器连接失败，请重新检查是否打开MySql服务。" + e.Message.ToString());  			
		}
		
	}
	
	public DataTable CreateTable (string name, string[] col, string[] colType)
	{
		if (col.Length != colType.Length) {			
			throw new Exception ("columns.Length != colType.Length");			
		}		
		string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];		
		for (int i = 1; i < col.Length; ++i) {			
			query += ", " + col[i] + " " + colType[i];			
		}		
		query += ")";
		return  ExecuteQuery(query);
	}
	
	public DataTable CreateTableAutoID (string name, string[] col, string[] colType)
	{
		if (col.Length != colType.Length) {			
			throw new Exception ("columns.Length != colType.Length");			
		}
		
		string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0] +  " NOT NULL AUTO_INCREMENT";		
		for (int i = 1; i < col.Length; ++i) {			
			query += ", " + col[i] + " " + colType[i];			
		}		
		query += ", PRIMARY KEY ("+ col[0] +")" + ")";		
		Debug.Log(query);		
		return  ExecuteQuery(query);
	}
	
	//插入一条数据，包括所有，不适用自动累加ID。
	public DataTable InsertInto (string tableName, string[] values)
	{
		string query = "INSERT INTO " + tableName + " VALUES (" + "'"+ values[0]+ "'";		
		for (int i = 1; i < values.Length; ++i) {			
			query += ", " + "'"+values[i]+ "'";			
		}		
		query += ")";		
		Debug.Log(query);
		return ExecuteQuery(query);		
	}
	
	//插入部分ID
	public DataTable InsertInto (string tableName, string[] col,string[] values)
	{		
		if (col.Length != values.Length) 
		{			
			throw new Exception ("columns.Length != colType.Length");			
		}
		
		string query = "INSERT INTO " + tableName + " (" + col[0];
		for (int i = 1; i < col.Length; ++i) 
		{			
			query += ", "+col[i];			
		}
		
		query += ") VALUES (" + "'"+ values[0]+ "'";
		for (int i = 1; i < values.Length; ++i) 
		{			
			query += ", " + "'"+values[i]+ "'";			
		}		
		query += ")";		
		Debug.Log(query);
		return ExecuteQuery (query);		
	}

	public DataTable SelectLimitLines(string tableName, int startLine, int lineNumbers){
		string query = "SELECT * FROM " + tableName + " where id > " + startLine + " limit " + lineNumbers;
		return ExecuteQuery(query);
	}
	
	public DataTable SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
	{		
		if (col.Length != operation.Length || operation.Length != values.Length) {			
			throw new Exception ("col.Length != operation.Length != values.Length");			
		}
		
		string query = "SELECT " + items[0];		
		for (int i = 1; i < items.Length; ++i) {			
			query += ", " + items[i];			
		}		
		query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";		
		for (int i = 1; i < col.Length; ++i) {			
			query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";			
		}
		
		return ExecuteQuery(query);
		
	} 
	
	public DataTable UpdateInto (string tableName, string []cols,string []colsvalues,string selectkey,string selectvalue)
	{		
		string query = "UPDATE "+tableName+" SET "+cols[0]+" = "+colsvalues[0];		
		for (int i = 1; i < colsvalues.Length; ++i) {
			
			query += ", " +cols[i]+" ="+ colsvalues[i];
		}		
		query += " WHERE "+selectkey+" = "+selectvalue+" ";		
		return ExecuteQuery (query);
	}
	
	public DataTable Delete(string tableName,string []cols,string []colsvalues)
	{
		string query = "DELETE FROM "+tableName + " WHERE " +cols[0] +" = " + colsvalues[0];		
		for (int i = 1; i < colsvalues.Length; ++i) 
		{			
			query += " or " +cols[i]+" = "+ colsvalues[i];
		}
		Debug.Log(query);
		return ExecuteQuery (query);
	}
	
	public  void Close()
	{		
		if(dbConnection != null)
		{
			dbConnection.Close();
			dbConnection.Dispose();
			dbConnection = null;
		}		
	}
	
	public DataTable ExecuteQuery(string sqlString)  
	{  
		if(dbConnection.State == ConnectionState.Open)
		{
			DataSet ds = new DataSet();  
			try  
			{				
				MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection); 
				da.Fill(ds);				
			}  
			catch (Exception ee)  
			{
				throw new Exception("SQL:" + sqlString + "/n" + ee.Message.ToString());  
			}
			finally
			{
			}
			return ds.Tables[0];
		}
		return null;
	}
	
}