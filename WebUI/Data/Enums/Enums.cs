using System.ComponentModel.DataAnnotations;

namespace WebUI.Data.Enums
{
    // define all enums used by EF models here

    public enum Frequency
    {
        Monthly = 1,
        Biweekly = 2,
        Weekly = 3
    }

    public enum BillingType
    {
        Hourly = 1,
        NonBillable = 2,
        FixedPrice = 3
    }
    
    public enum Duration
    {
        [Display(Name = "1 Hour")]
        OneHour = 1,
        [Display(Name = "1 Day")]
        OneDay = 2,
        [Display(Name = "1 Week")]
        OneWeek = 3,
        [Display(Name = "1 Month")]
        OneMonth = 4,
        [Display(Name = "1 Quarter")]
        OneQuarter = 5,
        [Display(Name = "1 Year")]
        OneYear = 6,
    }
    public enum Status
    {
        Approved = 1,
        Rejected = 2,
        Submitted_Pending = 3,
        Saved = 4
    }
}
