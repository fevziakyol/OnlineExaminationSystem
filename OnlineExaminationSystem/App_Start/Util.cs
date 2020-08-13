using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineExaminationSystem.App_Start
{
    public static class Util
    {
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        public static UserInfo UserInfo
        {
            get
            {
                if (HttpContext.Current.Session["UserInfo"] == null)
                {
                    return null;
                }
                return (UserInfo)HttpContext.Current.Session["UserInfo"];
            }
            set
            {
                HttpContext.Current.Session["UserInfo"] = value;
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }
        public static void AddDashboardLogItem(long? deviceId, long? streamId, int eventType, string description)
        {
            //using (OESContext db = new OESContext())
            //{
            //    db.DashboardLogs.Add(new DashboardLog()
            //    {
            //        DeviceId = deviceId,
            //        StreamId = streamId,
            //        EventTime = DateTime.Now,
            //        EventType = eventType,
            //        Description = description
            //    });
            //    db.SaveChanges();
            //}
        }
    }

    public class UserInfo
    {
        public long UserID { get; set; }
        public string NameSurname { get; set; }
        public bool IsAdmin { get; set; }
        public string Department { get; set; }
        public long StudentNumber { get; set; }
    }
}