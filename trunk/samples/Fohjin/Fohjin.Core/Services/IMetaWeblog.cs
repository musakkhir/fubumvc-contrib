using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Fohjin.Core.Services
{
    [ServiceContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
    public interface IMetaWeblog
    {
        [OperationContract(Action="metaWeblog.editPost")]
        bool editPost(string postid, string username, string password, mwPost mwPost, bool publish);

        [OperationContract(Action="metaWeblog.getCategories")]
        mwCategoryInfo[] getCategories(object blogid, string username, string password);

        [OperationContract(Action="metaWeblog.getPost")]
        mwPost getPost(string postid, string username, string password);

        [OperationContract(Action="metaWeblog.getRecentPosts")]
        mwPost[] getRecentPosts(object blogid, string username, string password, int numberOfPosts);

        [OperationContract(Action="metaWeblog.newPost")]
        string newPost(object blogid, string username, string password, mwPost mwPost, bool publish);

        [OperationContract(Action="metaWeblog.newMediaObject")]
        mediaObjectInfo newMediaObject(object blogid, string username, string password, mediaObject mediaobject);

        [OperationContract(Action="blogger.deletePost")]
        bool deletePost(string appKey, string postid, string username, string password, bool publish);

        [OperationContract(Action = "blogger.getUsersBlogs")]
        mwBlogInfo[] getUsersBlogs(string appKey, string username, string password);
    }

    [DataContract]
    public struct mwBlogInfo
    {
        [DataMember]
        public string blogid;
        [DataMember]
        public string url;
        [DataMember]
        public string blogName;
    }

    [DataContract]
    public struct mwEnclosure
    {
        [DataMember]
        public int length;
        [DataMember]
        public string type;
        [DataMember]
        public string url;
    }

    [DataContract]
    public struct mwSource
    {
        [DataMember]
        public string name;
        [DataMember]
        public string url;
    }

    [DataContract]
    public struct mwPost
    {
        [DataMember]
        public DateTime dateCreated;
        [DataMember]
        public string description;
        [DataMember]
        public string title;

        [DataMember]
        public string[] categories;
        [DataMember]
        public mwEnclosure enclosure;
        [DataMember]
        public string link;
        [DataMember]
        public string postabstract;
        [DataMember]
        public string permalink;

        [DataMember]
        public object postid;
        [DataMember]
        public mwSource source;
        [DataMember]
        public string userid;
    }

    [DataContract]
    public struct mwCategoryInfo
    {
        [DataMember]
        public string description;
        [DataMember]
        public string htmlUrl;
        [DataMember]
        public string rssUrl;
        [DataMember]
        public string title;
        [DataMember]
        public string categoryid;
    }

    [DataContract]
    public struct mwCategory
    {
        [DataMember]
        public string categoryId;
        [DataMember]
        public string categoryName;
    }

    [DataContract]
    public struct mediaObject
    {
        [DataMember]
        public string name;
        [DataMember]
        public string type;
        [DataMember]
        public byte[] bits;
    }

    [DataContract]
    public struct mediaObjectInfo
    {
        [DataMember]
        public string url;
    }
}