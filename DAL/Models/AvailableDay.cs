namespace Furlough.DAL.Models
{
    public partial class AvailableDay
    {
        // Most of these days are specified by law, check with HR for further details
        public int EmployeeId { get; set; }
        public int Medical { get; set; } //Medical days default to 20
        public int Yearly { get; set; } //Yearly days, 18 for the first job, 20 after first year, increases by 1 every 5 years
        public int Overtime { get; set; } //Starts at 0, increases depending on HR or manager and overtime worked
        public int Birth { get; set; } //Birth leave days, starts at 3
        public int Child { get; set; } //For children under 3 years old, 3 days a year for the female gender afaik.
        public int Marriage { get; set; } //Marriage leave days, defaults to 5
        public int Unpaid { get; set; } //Unpaid days start at 0, increase with every day when a request is made as this type
        public int BloodDonation { get; set; } //For donating blood you get 2 days of leave
        public int Confinement { get; set; } //Starts at 0, increases depending on the selected dates
        public int DeathOfRelative { get; set; } //Afaik 5 days a year,
    }
}
