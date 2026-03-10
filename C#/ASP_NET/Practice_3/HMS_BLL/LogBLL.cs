using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace HMS_BLL
{
    public class LogBLL
    {
        public void LogMaintain<T>(T logObject,string filePath)
        {
            StreamWriter st = new StreamWriter(filePath, append: true);
            st.Write("Added");
            string jsonString=JsonSerializer.Serialize(logObject);
            st.WriteLine(jsonString);
            st.Close();

        }
        public void LogDelete<T>(T logObject, string filePath)
        {
            StreamWriter st = new StreamWriter(filePath, append: true);
            st.Write("Deleted");
            string jsonString = JsonSerializer.Serialize(logObject);
            st.WriteLine(jsonString);
            st.Close();

        }

    }
}
