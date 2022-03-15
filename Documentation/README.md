# ACME Publication Scheduled Task

Project Description:

This is a .net 6 Console Application mockup

It contains a mock version of a dbcontext with no data with incomplete api implementation as they are meant to be placeholders.

The general idea behind is:

1 - Grab all active subscriptions and their related customer/address data
2 - Grab all print distributors and create a dictionary based on country/publication
3 - Loop through all active subscriptions and setup a http client to post to a certain endpoint with a defined credential and object setup. This would be added to a list of tasks.
4 - Finally run a multi-thread Task.Whenall with a certain number of tasks per loop (defaulted to 10 for now) which would be dependent on the number of subscriptions. Report back if either all tasks succeeded or if some tasks failed.

Within the documentation folder is the Database Diagram (Database Diagram.jpg).


How the task is run:

The task itself would be ideally published and deployed to a Windows Server and setup as a Windows Scheduled Task which would target the .exe generated. This would be setup to be triggered at the start of every month.




