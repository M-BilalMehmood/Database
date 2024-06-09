# Fast Day Care Management System

## Introduction

This project is a Day Care Management System developed using JavaFX. It provides a user-friendly interface for managing day-to-day operations of a day care center, including child registration, staff management, attendance tracking, and fee payments.

## Features

- **Child Registration:**  Register children with their personal information, parent/guardian details, and emergency contacts.
- **Staff Management:** Add and manage staff members, including their roles, contact information, and schedules.
- **Attendance Tracking:**  Record daily attendance for children and staff.
- **Fee Management:**  Manage fee structures, generate invoices, and track payments.
- **Reporting:** Generate reports on attendance, fees, and other relevant data.

## Technologies Used

- **WinForms:** For creating the user interface.
- **MSSQL:** For data storage.

## Project Structure

- **`src/main/java`:**
    - **`com.example.fastdaycaremanagementsystem`:**  Main package containing the application logic.
        - **`controllers`:** Controllers for handling user interactions and UI logic.
        - **`models`:** Domain models representing entities (e.g., `Child`, `Staff`, `Attendance`).
        - **`database`:** Classes for database connection and data access.
        - **`MainApplication`:** Main application class.
- **`src/main/resources`:**
    - **`com.example.fastdaycaremanagementsystem`:**  FXML files defining the UI layouts.

## Getting Started

1. **Clone the repository:** `git clone https://github.com/M-BilalMehmood/Fast-Day-Care-Managment-System.git`
2. **Import into Visual Studio:** Import the project as WinForm Project.
3. **Set up MsSQL Database:** Create a MySQL database and configure the connection details in the database connection class.
4. **Run the application:** Run the `MainApplication` class to start the application.

## Future Enhancements

- **Activity Scheduling:** Add functionality to schedule and manage daily activities for children.
- **Parent Communication:** Implement features for communication between staff and parents, such as messaging or notifications.
- **Meal Planning:**  Include a module for planning and tracking meals for children.
- **Reporting Enhancements:**  Expand reporting capabilities to provide more detailed insights into day care operations.
- **Mailing System:** Simple mailing system integrated that is not encrypted but can be done using libraries or self algorithms

## Contributing

Contributions to the project are welcome! Please follow the standard GitHub workflow for submitting pull requests.

## License

This project is licensed under the [MIT License](LICENSE).
