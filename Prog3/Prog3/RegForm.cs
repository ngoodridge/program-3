// Program 3
// CIS 199-01
// Due: 11/9/2017
// By: A7272

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2018 Priority Registration Schedule

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "November 3";   // 1st day of registration
            const string DAY2 = "November 6";   // 2nd day of registration
            const string DAY3 = "November 7";   // 3rd day of registration
            const string DAY4 = "November 8";   // 4th day of registration
            const string DAY5 = "November 9";   // 5th day of registration
            const string DAY6 = "November 10";  // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            string[] upperClassTimes = { TIME2, TIME3, TIME4, TIME5, TIME1 };  // An array that holds the upperclass registration times
            string[] lowerClassTimes = { TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2 }; // An array that holds the lower class registration times
            char[] upperClassLimits = { 'D', 'I', 'O', 'S', 'Z' }; // An array that holds the upper letter limits for the upper class
            char[] lowerClassLimits = { 'B', 'D', 'F', 'I', 'L', 'O', 'Q', 'S', 'V', 'Z' }; // An array that holds the upper letter limits for the lower class

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            bool isUpperClass;        // Upperclass or not?
            int counter = 0;          //control variable for both for loops

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                {
                    isUpperClass = (seniorRBtn.Checked || juniorRBtn.Checked);

                    // Juniors and Seniors share same schedule but different days
                    if (isUpperClass)
                    {
                        if (seniorRBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;

                        for (counter = 0; counter < upperClassTimes.Length; counter++) //steps through the array
                        {
                            if (lastNameLetterCh <= upperClassLimits[counter])  //until it finds the upper limit that is greater than the Last initial
                                break;  //breaks the loop once correct upper limit is found
                        
                        }
                        timeStr = upperClassTimes[counter];  //stores the registration time into the time string

                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophomoreRBtn.Checked)
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // G-S on one day
                            if ((lastNameLetterCh >= 'G') && // >= G and
                                (lastNameLetterCh <= 'S'))   // <= S
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }

                        for (counter = 0; counter < lowerClassTimes.Length; counter++)
                        {
                            if (lastNameLetterCh <= lowerClassLimits[counter])
                                break;

                        }
                        timeStr = lowerClassTimes[counter];
                    }

                    // Output results
                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
                else // First char not a letter
                    MessageBox.Show("Make sure last name starts with a letter");
            }
            else // Empty textbox
                MessageBox.Show("Enter a last name!");
        }
    }
}
