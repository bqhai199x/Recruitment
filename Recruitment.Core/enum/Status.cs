using System.ComponentModel.DataAnnotations;

namespace Recruitment.Core
{
    public enum Status
    {
        [Display(Name = "Nhận CV")]
        NhanCV,
        [Display(Name = "Loại CV")]
        LoaiCV,
        [Display(Name = "Duyệt CV")]
        DuyetCV,
        [Display(Name = "Mời test")]
        MoiTest,
        [Display(Name = "Test OK")]
        TestOK,
        [Display(Name = "Mời PV V1")]
        PVV1,
        [Display(Name = "Pass V1")]
        PassV1,
        [Display(Name = "Mời PV V2")]
        PVV2,
        [Display(Name = "Pass V2")]
        PassV2,
        [Display(Name = "Đã gửi offer")]
        GuiOffer,
        [Display(Name = "Đã nhận offer")]
        NhanOffer,
        [Display(Name = "Từ chối offer")]
        TuChoiOffer,
        [Display(Name = "Đã gửi form")]
        GuiForm,
        [Display(Name = "All OK")]
        AllOK
    }
}