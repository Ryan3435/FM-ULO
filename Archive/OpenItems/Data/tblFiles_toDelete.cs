//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenItems.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblFiles_toDelete
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
        public System.DateTime UploadDate { get; set; }
        public bool Active { get; set; }
    }
}
