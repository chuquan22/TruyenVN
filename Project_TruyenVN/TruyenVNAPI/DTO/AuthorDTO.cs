using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TruyenVNAPI.DTO
{
    public class AuthorDTO
    {
        public string author_name { get; set; }
        public string? author_description { get; set; }
    }
}
