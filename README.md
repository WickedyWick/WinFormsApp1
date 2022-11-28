# Ticketing service

Pretending that user is authenitacted and logged in

RETHINK - Possibly switch maxSeats data to the seatsAvailable since max seats doesnt make much sense?

Assuming that maxSeats data doesnt change we can check for those requests on the frontendd
We can also allow buying multiple tickets for the same event
No buying tickets for multiple events at the same time to avoid weird states in case one purchase is successful and one isn't
For the backend try to use EF core, expand schema of the Events to have seats avaiable and decrement that for the amount of tickets purchased.
Use transactions for purchasing tickets to avoid race condition and wrong states
Have data number field to set how many tickets (they cant be bigger than current tickets available)

Create order logs on the backend as well
Have my tickets button?

In case if high traffic is expected implement queues etc

current to do is to fix damn EF and MYSQL compatibility and things will be easier