using UnityEngine;  
using System;  
using System.Data;  
using System.Collections;   
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;

public class MySqlDBTest : MonoBehaviour {	
	string Error = null;
	void Start () 
	{
		try
		{
			MySqlDB mysql = new  MySqlDB();
			//mysql.CreateTableAutoID("momo",new string[]{"id","name","qq","email","blog"}, new string[]{"int","text","text","text","text"});
			//mysql.InsertInto("momo",new string[]{"name","qq","email","blog"},new string[]{"xuanyusong","289187120","xuanyusong@gmail.com","xuanyusong.com"});
			//mysql.InsertInto("momo",new string[]{"name","qq","email","blog"},new string[]{"ruoruo","34546546","ruoruo@gmail.com","xuanyusong.com"});

			//DataSet ds  = mysql.SelectWhere("momo",new string[]{"name","qq"},new string []{"id"},new string []{"="},new string []{"1"});
			DataTable dt = mysql.SelectLimitLines("momo",0,100);
			if(dt != null)
			{
				foreach (DataRow row in dt.Rows)
				{
				   foreach (DataColumn column in dt.Columns)
				   {
						Debug.Log(row[column]);
				   }
				}
			}

			 //mysql.UpdateInto("momo",new string[]{"name","qq"},new string[]{"'ruoruo'","'11111111'"}, "email", "'xuanyusong@gmail.com'"  );
			 //mysql.Delete("momo",new string[]{"id","email"}, new string[]{"1","'000@gmail.com'"}  );
			 mysql.Close();
		}catch(Exception e)
		{
			Error = e.Message;
		}	
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		
		if(Error != null)
		{
			GUILayout.Label(Error);
		}
		
	}
}

