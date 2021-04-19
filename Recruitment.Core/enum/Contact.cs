using System.ComponentModel.DataAnnotations;

namespace Recruitment.Core
{
    public enum Contact
    {
        [Display(Name = "Chưa liên hệ")]
        Waiting,
        [Display(Name = "Đã liên hệ")]
        Contacted,
        [Display(Name = "Không liên hệ được")]
        Error,
    }
}