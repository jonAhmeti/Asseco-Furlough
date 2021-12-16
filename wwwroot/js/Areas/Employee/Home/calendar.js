var selectedDates = new Array();
var listShow = false;

const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];

dayNames = {
    full: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
    d: ['S', 'M', 'T', 'W', 'T', 'F', 'S'],
    dd: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
    ddd: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
};


(function (global) {


    "use strict";

    var
        //this will be used by the user.
        dycalendar = {},

        //window document
        document = global.document,

        //starting year
        START_YEAR = 2021,

        //end year
        END_YEAR = 9999,

        //name of the months
        monthName = {
            full: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            mmm: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },

        //name of the days
        dayName = {
            full: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
            d: ['S', 'M', 'T', 'W', 'T', 'F', 'S'],
            dd: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
            ddd: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat']
        };

    /**
     * this function will create month table.
     *
     * @param object data   this contains the calendar data
     * @param object option this is the settings object
     * @return html
     */
    function createMonthTable(data, option) {

        var
            table, tr, td,
            r, c, count;

        table = document.createElement("table");
        tr = document.createElement("tr");

        //create 1st row for the day letters
        for (c = 0; c <= 6; c = c + 1) {
            td = document.createElement("td");
            td.innerHTML = "SMTWTFS"[c];
            tr.appendChild(td);
        }
        table.appendChild(tr);

        //create 2nd row for dates
        tr = document.createElement("tr");

        //blank td
        for (c = 0; c <= 6; c = c + 1) {
            if (c === data.firstDayIndex) {
                break;
            }
            td = document.createElement("td");
            tr.appendChild(td);
        }

        //remaing td of dates for the 2nd row
        count = 1;
        while (c <= 6) {
            td = document.createElement("td");
            td.innerHTML = count;
            if (data.today.date === count && data.today.monthIndex === data.monthIndex && option.highlighttoday === true) {
                td.setAttribute("class", "dycalendar-today-date");
            }
            if (option.date === count && option.month === data.monthIndex && option.highlighttargetdate === true) {
                td.setAttribute("class", "dycalendar-target-date");
            }

            tr.appendChild(td);
            count = count + 1;
            c = c + 1;
        }
        table.appendChild(tr);

        //create remaining rows
        for (r = 3; r <= 7; r = r + 1) {
            tr = document.createElement("tr");
            for (c = 0; c <= 6; c = c + 1) {
                if (count > data.totaldays) {
                    table.appendChild(tr);
                    return table;
                }
                td = document.createElement('td');
                td.innerHTML = count;
                if (data.today.date === count && data.today.monthIndex === data.monthIndex && option.highlighttoday === true) {
                    td.setAttribute("class", "dycalendar-today-date");
                }
                //=============== this if statement sets specific class for today's date even if the month/year is different ===============
                //if (option.date === count && option.month === data.monthIndex && option.highlighttargetdate === true) {
                //    td.setAttribute("class", "dycalendar-target-date");
                //}
                count = count + 1;

                tr.appendChild(td);
            }
            table.appendChild(tr);
        }

        return table;
    }

    /**
     * this function will draw Calendar Month Table
     *
     * @param object data   this contains the calendar data
     * @param object option this is the settings object
     * @return html
     */
    function drawCalendarMonthTable(data, option) {

        var
            table,
            div, container, elem;

        //get table
        table = createMonthTable(data, option);

        //calendar container
        container = document.createElement("div");
        container.setAttribute("class", "dycalendar-month-container");

        //-------------------------- Header ------------------

        //header div
        div = document.createElement("div");
        div.setAttribute("class", "dycalendar-header");
        div.setAttribute("data-option", JSON.stringify(option));

        //prev button
        if (option.prevnextbutton === "show") {
            elem = document.createElement("span");
            elem.setAttribute("class", "dycalendar-prev-next-btn prev-btn");
            elem.setAttribute("data-date", option.date);
            elem.setAttribute("data-month", option.month);
            elem.setAttribute("data-year", option.year);
            elem.setAttribute("data-btn", "prev");
            elem.innerHTML = "&lt;";
            //add prev button span to header div
            div.appendChild(elem);
        }

        //month span
        elem = document.createElement("span");
        elem.setAttribute("class", "dycalendar-span-month-year");
        if (option.monthformat === "mmm") {
            elem.innerHTML = data.monthName + " " + data.year;
        } else if (option.monthformat === "full") {
            elem.innerHTML = data.monthNameFull + " " + data.year;
        }

        //add month span to header div
        div.appendChild(elem);

        //next button
        if (option.prevnextbutton === "show") {
            elem = document.createElement("span");
            elem.setAttribute("class", "dycalendar-prev-next-btn next-btn");
            elem.setAttribute("data-date", option.date);
            elem.setAttribute("data-month", option.month);
            elem.setAttribute("data-year", option.year);
            elem.setAttribute("data-btn", "next");
            elem.innerHTML = "&gt;";
            //add prev button span to header div
            div.appendChild(elem);
        }

        //add header div to container
        container.appendChild(div);

        //-------------------------- Body ------------------

        //body div
        div = document.createElement("div");
        div.setAttribute("class", "dycalendar-body");
        div.appendChild(table);

        //add body div to container div
        container.appendChild(div);

        //return container
        return container;
    }

    /**
     * this function will draw Calendar Day
     *
     * @param object data   this contains the calendar data
     * @param object option this is the settings object
     * @return html
     */
    function drawCalendarDay(data, option) {

        var
            div, container, elem;

        //calendar container
        container = document.createElement("div");
        container.setAttribute("class", "dycalendar-day-container");

        //-------------------------- Header ------------------

        //header div
        div = document.createElement("div");
        div.setAttribute("class", "dycalendar-header");

        //day span
        elem = document.createElement("span");
        elem.setAttribute("class", "dycalendar-span-day");
        if (option.dayformat === "ddd") {
            elem.innerHTML = dayName.ddd[data.targetedDayIndex];
        } else if (option.dayformat === "full") {
            elem.innerHTML = dayName.full[data.targetedDayIndex];
        }

        //add day span to footer div
        div.appendChild(elem);

        //add header div to container
        container.appendChild(div);

        //-------------------------- Body ------------------

        //body div
        div = document.createElement("div");
        div.setAttribute("class", "dycalendar-body");

        //date span
        elem = document.createElement("span");
        elem.setAttribute("class", "dycalendar-span-date");
        elem.innerHTML = data.date;

        //add date span to body div
        div.appendChild(elem);

        //add body div to container
        container.appendChild(div);

        //-------------------------- Footer ------------------

        //footer div
        div = document.createElement("div");
        div.setAttribute("class", "dycalendar-footer");

        //month span
        elem = document.createElement("span");
        elem.setAttribute("class", "dycalendar-span-month-year");
        if (option.monthformat === "mmm") {
            elem.innerHTML = data.monthName + " " + data.year;
        } else if (option.monthformat === "full") {
            elem.innerHTML = data.monthNameFull + " " + data.year;
        }

        //add month span to footer div
        div.appendChild(elem);

        //add footer div to container
        container.appendChild(div);

        //return container
        return container;
    }

    /**
     * this function will extend source object with defaults object.
     *
     * @param object source     this is the source object
     * @param object defaults   this is the default object
     * @return object
     */
    function extendSource(source, defaults) {
        var property;
        for (property in defaults) {
            if (source.hasOwnProperty(property) === false) {
                source[property] = defaults[property];
            }
        }
        return source;
    }

    /**
     * This function will return calendar detail.
     *
     * @param integer year        1900-9999 (optional) if not set will consider
     *                          the current year.
     * @param integer month        0-11 (optional) 0 = Jan, 1 = Feb, ... 11 = Dec,
     *                          if not set will consider the current month.
     * @param integer date      1-31 (optional)
     * @return boolean|object    if error return false, else calendar detail
     */
    function getCalendar(year, month, date) {

        var
            dateObj = new Date(),
            dateString,
            result = {},
            idx;

        if (year < START_YEAR || year > END_YEAR) {
            global.console.error("Invalid Year");
            return false;
        }
        if (month > 11 || month < 0) {
            global.console.error("Invalid Month");
            return false;
        }
        if (date > 31 || date < 1) {
            global.console.error("Invalid Date");
            return false;
        }

        result.year = year;
        result.month = month;
        result.date = date;

        //today
        result.today = {};
        dateString = dateObj.toString().split(" ");

        idx = dayName.ddd.indexOf(dateString[0]);
        result.today.dayIndex = idx;
        result.today.dayName = dateString[0];
        result.today.dayFullName = dayName.full[idx];

        idx = monthName.mmm.indexOf(dateString[1]);
        result.today.monthIndex = idx;
        result.today.monthName = dateString[1];
        result.today.monthNameFull = monthName.full[idx];

        result.today.date = dateObj.getDate();

        result.today.year = dateString[3];

        //get month-year first day
        dateObj.setDate(1);
        dateObj.setMonth(month);
        dateObj.setFullYear(year);
        dateString = dateObj.toString().split(" ");

        idx = dayName.ddd.indexOf(dateString[0]);
        result.firstDayIndex = idx;
        result.firstDayName = dateString[0];
        result.firstDayFullName = dayName.full[idx];

        idx = monthName.mmm.indexOf(dateString[1]);
        result.monthIndex = idx;
        result.monthName = dateString[1];
        result.monthNameFull = monthName.full[idx];

        //get total days for the month-year
        dateObj.setFullYear(year);
        dateObj.setMonth(month + 1);
        dateObj.setDate(0);
        result.totaldays = dateObj.getDate();

        //get month-year targeted date
        dateObj.setFullYear(year);
        dateObj.setMonth(month);
        dateObj.setDate(date);
        dateString = dateObj.toString().split(" ");

        idx = dayName.ddd.indexOf(dateString[0]);
        result.targetedDayIndex = idx;
        result.targetedDayName = dateString[0];
        result.targetedDayFullName = dayName.full[idx];

        return result;

    }

    /**
     * this function will handle the on click event.
     */
    function onClick() {

        document.body.onclick = function (e) {

            //get event object (window.event for IE compatibility)
            e = global.event || e;

            var
                //get target dom object reference
                targetDomObject = e.target || e.srcElement,

                //other variables
                date, month, year, btn, option, dateObj;

            //prev-next button click
            //extra checks to make sure object exists and contains the class of interest
            if ((targetDomObject) && (targetDomObject.classList) && (targetDomObject.classList.contains("dycalendar-prev-next-btn"))) {
                date = parseInt(targetDomObject.getAttribute("data-date"));
                month = parseInt(targetDomObject.getAttribute("data-month"));
                year = parseInt(targetDomObject.getAttribute("data-year"));
                btn = targetDomObject.getAttribute("data-btn");
                option = JSON.parse(targetDomObject.parentElement.getAttribute("data-option"));

                if (btn === "prev") {
                    month = month - 1;
                    if (month < 0) {
                        year = year - 1;
                        month = 11;
                    }
                }
                else if (btn === "next") {
                    month = month + 1;
                    if (month > 11) {
                        year = year + 1;
                        month = 0;
                    }
                }

                option.date = date;
                option.month = month;
                option.year = year;

                drawCalendar(option);
            }

            //month click
            //extra checks to make sure object exists and contains the class of interest
            if ((targetDomObject) && (targetDomObject.classList) && (targetDomObject.classList.contains("dycalendar-span-month-year"))) {
                option = JSON.parse(targetDomObject.parentElement.getAttribute("data-option"));
                dateObj = new Date();

                option.date = dateObj.getDate();
                option.month = dateObj.getMonth();
                option.year = dateObj.getFullYear();

                drawCalendar(option);
            }
        };
    }

    //------------------------------ dycalendar.draw() ----------------------

    /**
     * this function will draw the calendar based on user preferences.
     *
     * option = {
     *  target : "#id|.class"   //(mandatory) for id use #id | for class use .class
     *  type : "calendar-type"  //(optional) values: "day|month" (default "day")
     *  month : "integer"       //(optional) value 0-11, where 0 = January, ... 11 = December (default current month)
     *  year : "integer"        //(optional) example 1990. (default current year)
     *  date : "integer"        //(optional) example 1-31. (default current date)
     *  monthformat : "full"    //(optional) values: "mmm|full" (default "full")
     *  dayformat : "full"      //(optional) values: "ddd|full" (default "full")
     *  highlighttoday : boolean    //(optional) (default false) if true will highlight today's date
     *  highlighttargetdate : boolean   //(optional) (default false) if true will highlight targeted date of the month year
     *  prevnextbutton : "hide"         //(optional) (default "hide") (values: "show|hide") if set to "show" it will show the nav button (prev|next)
     * }
     *
     * @param object option     user preferences
     * @return boolean          true if success, false otherwise
     */
    dycalendar.draw = function (option) {

        //check if option is passed or not
        if (typeof option === "undefined") {
            global.console.error("Option missing");
            return false;
        }

        var
            self = this,    //pointing at dycalendar object

            dateObj = new Date(),

            //default settings
            defaults = {
                type: "day",
                month: dateObj.getMonth(),
                year: dateObj.getFullYear(),
                date: dateObj.getDate(),
                monthformat: "full",
                dayformat: "full",
                highlighttoday: false,
                highlighttargetdate: false,
                prevnextbutton: "hide"
            };

        //extend user options with predefined options
        option = extendSource(option, defaults);

        drawCalendar(option);

    };

    //------------------------------ dycalendar.draw() ends here ------------

    /**
     * this function will draw the calendar inside the target container.
     */
    function drawCalendar(option) {

        var
            //variables for creating calendar
            calendar,
            calendarHTML,
            targetedElementBy = "id",
            targetElem,

            //other variables
            i, len, elemArr;

        //find target element by
        if (option.target[0] === "#") {
            targetedElementBy = "id";
        } else if (option.target[0] === ".") {
            targetedElementBy = "class";
        }
        targetElem = option.target.substring(1);

        //get calendar HTML
        switch (option.type) {
            case "day":
                //get calendar detail
                calendar = getCalendar(option.year, option.month, option.date);
                //get calendar html
                calendarHTML = drawCalendarDay(calendar, option);
                break;

            case "month":
                //get calendar detail
                calendar = getCalendar(option.year, option.month, option.date);
                //get calendar html
                calendarHTML = drawCalendarMonthTable(calendar, option);
                break;

            default:
                global.console.error("Invalid type");
                return false;
        }

        //draw calendar
        if (targetedElementBy === "id") {

            document.getElementById(targetElem).innerHTML = calendarHTML.outerHTML;

        } else if (targetedElementBy === "class") {

            elemArr = document.getElementsByClassName(targetElem);
            for (i = 0, len = elemArr.length; i < len; i = i + 1) {
                elemArr[i].innerHTML = calendarHTML.outerHTML;
            }

        }

        //self explanatory
        setCalendarDayEvents();
        // ____________________________________ after calendar is drawn ____________________________________
        const toastMsg = $("#toastMsg");
        const toastBody = $("#toastBody");

        const toastClose = $("#toastMsg button");
        $(toastClose).on('click', function () {
            $(toastMsg).hide();
        });

        const daysSubmitBtn = $("#selectedDaysSubmit");
        const daysResetBtn = $("#selectedDaysReset");


        //Submit button
        $(daysSubmitBtn).on('click', function () {

            //Get paidDays and requestType values
            const requestType = $('select[name="requestType"]');
            const paidDays = $('input[name="paidDays"]');
            const paidDaysValidate = $('#paidDaysValidate');
            const requestTypeValidate = $('#requestTypeValidate');

            //Validation for requestType and paidDays inputs
            if (requestType.val() == null || paidDays.val() == null || $.trim(paidDays.val()) == '') {
                if (!$.isNumeric(paidDays.val())) {
                    console.log('paidDays is not Numeric');

                    $(paidDays).addClass("border-danger");
                    $(paidDaysValidate).removeClass("d-none");
                    $(paidDays).on('change', function () {
                        $(this).removeClass("border-danger");
                        $(paidDaysValidate).addClass("d-none");
                    });
                }
                if (requestType.val() == null) {
                    console.log('requestType is null');

                    $(requestType).addClass("border-danger");
                    $(requestTypeValidate).removeClass("d-none");
                    $(requestType).on('change', function () {
                        $(this).removeClass("border-danger");
                        $(requestTypeValidate).addClass("d-none");
                    });
                }
                return;
            }

            //create temp array to increase value by 1. backend checks months from 1-12, meanwhile JS from 0-11.
            //better to create a temporary array otherwise selectedDates will just keep increasing months and we will have to decrease it in a complete ajax statement
            let tempArray = new Array();
            for (var i = 0; i < selectedDates.length; i++) {
                tempArray.push(`${selectedDates[i].split('/')[0]}/${parseInt(selectedDates[i].split('/')[1]) + 1}/${selectedDates[i].split('/')[2]}`);
            }

            $.ajax({
                method: 'POST',
                url: 'Employee/Home/SubmitRequest',
                data: { Dates: tempArray, RequestTypeId: requestType.val(), PaidDays: paidDays.val() },
                success: function (result) {
                    $(daysSubmitBtn).animate({ color: '#4BB543' }, 500);
                    $(toastBody).html(`
                                ${result ? "Request submitted successfully." :
                            "Something went wrong submitting your request."}
                    `);
                    $(toastMsg).show();
                },
                error: function (error) {
                    $(daysSubmitBtn).animate({ color: '#CA0B00' }, 500);
                    $(toastBody).html(`${result} <br /> Something went wrong submitting your request."}
                    `);
                    $(toastMsg).show();
                },
                complete: function () {
                    $(this).finish();
                    $(daysSubmitBtn).animate({ color: '#000' }, 1000);
                }
            });
        });

        $(daysResetBtn).on('click', function () {
            selectedDates = new Array();
            selectedDaysList.innerHTML = '';
            $(".dycalendar-body td").removeAttr("style");
            if (listShow) {
                listShow = false;
                $("#selectedDaysWrapper").animate({ 'left': '-25%', 'opacity': '0' }, 500);
                $("#calendarWrapper").animate({ 'left': '25%' }, 500);
                $("#requestDetailsWrapper").animate({ 'left': '0%' }, 500);
            }
        });
    }

    //events
    onClick();

    //attach to global window object
    global.dycalendar = dycalendar;

}(typeof window !== "undefined" ? window : this));

// onClick events for clicking on days
function setCalendarDayEvents() {
    let calendarHeader = $(".dycalendar-header");
    let days = $("#calendar td");
    let day = undefined;
    let month = undefined;
    let year = undefined;
    for (var i = 0; i < days.length; i++)
    {
        if (!isNaN(days[i].innerText) && !isNaN(parseFloat(days[i].innerText)))
        {
            $(days[i]).on('click', function () {

                


                let dataOptions = JSON.parse(calendarHeader.attr('data-option'));

                day = this.innerText;
                month = dataOptions.month;
                year = dataOptions.year;

                let clickedDate = `${year}/${month}/${day}`;

                if (dayNames.d[new Date(year,month,day).getDay()] != "S" && new Date(year, month, day) >= new Date() && !containsStringDate(clickedDate, selectedDates)) {
                    if (!listShow) {
                        listShow = true;
                        $("#selectedDaysWrapper").animate({ 'left': 0, 'opacity': '1' }, 500);
                        $("#calendarWrapper").animate({ 'left': 0 }, 500);
                        $("#requestDetailsWrapper").animate({ 'left': '100%' }, 500);
                    }

                    $(this).css('background-color', '#0dcaf0');
                    console.log(`Date clicked: ${clickedDate}`);
                    selectedDates.push(clickedDate);
                    //display sorted date
                    selectedDates.sort();
                    //console.log(selectedDates);
                    selectedDaysList.innerHTML = selectedDates.map(element => {
                        return `<div class="row row-cols-6">
                                    <div class="col-4 text-end">
                                        <i class="fa-solid fa-circle-check text-success"></i>
                                    </div>
                                    <div class="col-8 text-start"> 
                                        ${dayNames.ddd[new Date(element.split('/')[0],
                                            element.split('/')[1],
                                            element.split('/')[2]).getDay()]}
                                        ${element.split('/')[2]} 
                                        ${monthNames[element.split('/')[1]]}
                                         ${element.split('/')[0]}
                                    </div>
                                </div>`
                    }).join('');

                }
            });
        }

    }
}

//function removeSelectedDay(removeFromArray, index) {
//    removeFromArray = removeFromArray.filter(item => item !== removeFromArray[index]);
//    selectedDates = removeFromArray;
//    console.log(removeFromArray);
//}

function containsDate(date, array) {
    let found = false;
    for (var i = 0; i < array.length; i++) {
        if (array[i].getDate() == date.getDate()
            && array[i].getMonth() == date.getMonth()
            && array[i].getFullYear() == date.getFullYear()) {
            found = true;
        }
    }
    return found;
}

function containsStringDate(date, array) {
    let found = false;
    for (var i = 0; i < array.length; i++) {
        if (array[i] === date) {
            found = true;
        }
    }

    return found;
}