# RSSFeedAPI_

URL: https://localhost:44300/api/Company/api/Companies/FilterNotActive 
Purpose: Given a Dictionary keyed by Company and valued by RSS feed url, write a function that determines which companies had no activity for a given number of days 
Input:
•	noOfDays: int 
•	companies: Dictionary<string, IEnumerable> 
Output: 
Dictionary keyed by Company and valued by RSS feed url
Assumptions: 
•	As company may have multiple feed feed URLs, this code except list of URLs as a value of the Dictionary which will be pass as input 
•	If RSS Feed does not have last updated date in xml then this code consider its last updated on DateTime.MinValue.
•	This code compares time also to calculate no activity days.

