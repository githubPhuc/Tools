using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ToolsApp.Utilities
{
    public static class AppParameters
    {
        public static String FolderUpload { get { return ConfigurationManager.AppSettings["FolderUpload"].ToString(); } }
        public static String Domain { get { return ConfigurationManager.AppSettings["Domain"].ToString(); } }
        public static String AdminPass { get { return ConfigurationManager.AppSettings["AdminPass"].ToString(); } }
        public static String SourceEmail_AddedValues { get { return ConfigurationManager.AppSettings["SourceEmail_AddedValues"].ToString(); } }
        public static String AppName { get { return ConfigurationManager.AppSettings["AppName"].ToString(); } }
        public static String Protocol { get { return ConfigurationManager.AppSettings["Protocol"].ToString(); } }
        public static String SiteKey { get { return ConfigurationManager.AppSettings["SiteKey"].ToString(); } }
        public static String myBucketName { get { return ConfigurationManager.AppSettings["myBucketName"].ToString(); } }
        public static String myAccessKeyId { get { return ConfigurationManager.AppSettings["myAccessKeyId"].ToString(); } }
        public static String mySecretAccessKey { get { return ConfigurationManager.AppSettings["mySecretAccessKey"].ToString(); } }
        public static String myServiceUrl { get { return ConfigurationManager.AppSettings["myServiceUrl"].ToString(); } }
    }
}