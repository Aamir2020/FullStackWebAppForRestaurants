# Full Stack Web Application For Restaurants

A comprehensive full-stack web application built with ASP.NET MVC to streamline restaurant operations, offering features such as online ordering, menu management, and user authentication.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
  * [Frontend](#frontend)
  * [Backend](#backend)
  * [Database](#database)
    + [Entity Diagram](#entity-diagram)
- [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
- [Project Demo](#project-demo)

## Features

- **Online Ordering**: Customers can browse the menu, add items to their cart, and place orders seamlessly.
- **Menu Management**: Admins can add, update, or remove menu items.
- **User Authentication**: Secure login and registration system for both customers and administrators.
- **Order Tracking**: Customers can view their order history and track the status of current orders.
- **Responsive Design**: Optimized for various devices.

## Technologies Used

### Frontend

- ASP.NET MVC
- Bootstrap

### Backend

- ASP.NET MVC (C#)
- Entity Framework
- ASP.NET Identity

### Database

- SQL Server

#### Entity Diagram
![Diagram](https://github.com/user-attachments/assets/896f2e62-cf2a-4eba-a8ce-9342d937dd86)




## Getting Started

### Prerequisites

- Visual Studio 2022 or later
- SQL Server installed and running

### Installation

1. **Clone the repository**:

   ```sh
   git clone https://github.com/Aamir2020/FullStackWebAppForRestaurants.git
   ```

2. **Navigate to the project directory**:

   ```sh
   cd FullStackWebAppForRestaurants
   ```

3. **Open the solution file**: Open `FullStackWebAppForRestaurants.sln` in Visual Studio.

4. **Restore NuGet packages**: Go to `Tools > NuGet Package Manager > Manage NuGet Packages for Solution` and restore them manually if necessary.

5. **Update database connection string**: Modify `appsettings.json` to match your SQL Server configuration.

6. **Apply migrations and seed the database**:

   ```sh
   Update-Database
   ```

7. **Run the application**: Press `F5` in Visual Studio or click `Start`.

8. **Access the application**: Navigate to `http://localhost:5000` (or the port specified in your launch settings).

## Project Demo
![image](https://github.com/user-attachments/assets/8a06337f-ba8e-4597-87dd-1a3195d83be6)

![image](https://github.com/user-attachments/assets/ae54db2d-3fe4-4769-8b8f-4e70f6f0a828)



## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for details.

