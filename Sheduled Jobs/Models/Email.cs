using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Email
    {
        [Required]
        public string FromAddress { get; set; }

        [Required]
        public string ToAddress { get; set; }

        public string CcAddress { get; set; }

        public string BccAddress { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string GmailAppPassword { get; set; }
    }
}
