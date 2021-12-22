# Furlough
Menaxhimi i pushimeve te punetoreve

SQL Table Description

	AvailableDays 
		Medical - medical days, 20 days allowed by default.
		Yearly - yearly days, 18 days allowed within the first year of starting, 20 days after the first year and then +1 day after every 5 years.
		Overtime - starts at 0, increases by overtime that was done ( by HR or manager maybe ).
		Birth - days for giving birth, 3 days given by default.
		ChildDays - days for having children < 3 years old, supposedly only for the female gender.
		Marriage - days for marriage, defaults to 5.
		Unpaid - starts at 0, increases for every day taken with requestType unpaid.
		BloodDonation - 2 days for donating blood.
		Confinmenet - starts at 0, increases for every day taken as requestType confinement
		DeathOfRelative - supposedly 5 days.
