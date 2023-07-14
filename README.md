# ForecastWeatherApp
This application was developed by FlameRays using the Framework called .Net MAUI 
The purpose of the application is to get the weather forecast for a week using API .
The API give user their real time weather forecast by identifying the location given by user 
# Test Cases
Test Case: Valid Location

Input: "Piet Retief"
Expected Output: Weather forecast for the location "Piet Retief" is displayed in the collection list.
Test Case: Empty Location

Input: ""
Expected Output: No weather forecast is retrieved and displayed.
Test Case: Invalid Location

Input: "abc123"
Expected Output: No weather forecast is retrieved and displayed.
Test Case: Weather Item Tap

Input: Tap on a weather item in the collection list.
Expected Output: The app navigates to the details page showing the weather details for the selected day.
Test Case: Exception Handling - API Error

Input: Simulate an error response from the weather API.
Expected Output: The app displays an error message indicating the failure to retrieve the weather forecast.
Test Case: Exception Handling - Network Error

Input: Disable network connectivity.
Expected Output: The app displays an error message indicating the failure to connect to the weather API.
Test Case: Exception Handling - Null Response

Input: Simulate a null response from the weather API.
Expected Output: The app displays an error message indicating the failure to retrieve the weather forecast.
Test Case: Exception Handling - Deserialization Error

Input: Modify the JSON response to contain invalid data that cannot be deserialized.
Expected Output: The app displays an error message indicating the failure to parse the weather forecast data.
