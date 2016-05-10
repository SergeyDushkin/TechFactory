using System;

namespace TF.Data.Systems
{
    /// <summary>
    /// Table which contains links to the files
    /// </summary>
    public class Link
    {
        public Guid Id { get; set; }
        public Guid ReferenceId { get; set; }
        public string Uri { get; set; }
    }
}
