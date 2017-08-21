using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class Test : MonoBehaviour 
{
    string[] col = new string[3] { "id", "name", "age" };
    string[] colType = new string[3] { "int(5) primary key", "varchar(8)", "int(5)" };
    string[] values = new string[3] { "1", "'a'", "28" };

    void Start()
    {
        var sql = new SqlAccess();
        sql.Create("tableTest1", col, colType);
        sql.Insert("tableTest1", values);
        sql.Update("tableTest1", new string[] { "age" }, new string[] { "18" }, "id", "1");
        //sql.Delete("tableTest1", new string[] { "id" }, new string[] { "1" });
        // 获取查询结果保存到DataSet变量
        DataSet ds = sql.Select("tableTest1", "age", "id = 1");
        if (ds != null)
        {
            // 创建临时表保存查询结果
            DataTable table = ds.Tables[0];
            // 遍历查询结果 并输出
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Debug.Log(row[column]);
                }
            }
        }
    }
}
