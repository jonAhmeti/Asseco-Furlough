namespace Furlough.DAL.Models
{
    public partial class AvailableDay
    {
        // Most of these days are specified by law, check with HR for further details
        public int EmployeeId { get; set; }
        public decimal Medical { get; set; } //Medical days default to 20
        public decimal Yearly { get; set; } //Yearly days, 18 for the first job, 20 after first year, increases by 1 every 5 years
        public decimal PrevYearly { get; set; }
        public decimal Overtime { get; set; } //Starts at 0, increases depending on HR or manager and overtime worked
        public decimal Birth { get; set; } //Birth leave days, starts at 3
        public decimal Child { get; set; } //For children under 3 years old, 3 days a year for the female gender afaik.
        public decimal Marriage { get; set; } //Marriage leave days, defaults to 5
        public decimal Unpaid { get; set; } //Unpaid days start at 0, increase with every day when a request is made as this type
        public decimal BloodDonation { get; set; } //For donating blood you get 2 days of leave
        public decimal Maternity { get; set; } //Starts at 0, increases depending on the selected dates
        public decimal DeathOfRelative { get; set; } //Afaik 5 days a year,
    }
}
