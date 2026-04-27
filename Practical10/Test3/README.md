## Task
- Create an MVC Project for a demo of OutputCache Filter. The output of the Controller action should be cached for 5 minutes, and the action should return the Current date time as a string. So, after implementing the filter, the same date-time will be displayed for an interval of 5 minutes.

## Note

- Currently the caching location is set at client side. So refreshing from browser, will generate new view due to browser sends request for new view complusory by setting cache-control: max-age = 0. So if wenavigate through app then only cached view is returned.
- If we set caching location to server then no matter how many request are made only cached view is sent up to particular time.