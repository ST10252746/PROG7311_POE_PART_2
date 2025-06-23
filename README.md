<img src="logo.png" alt="Logo" width="200"/>

# Agri-Energy Connect: Vision for Sustainability
*master branch created for PART 3 - PART 2 web application prototype resubmission -*

South Africa's agriculture is evolving. Agri-Energy Connect is the next step forward - combining farming and clean energy. A digital bridge between farmers and green energy providers in South Africa.

> [![YouTube](https://img.shields.io/badge/Demo%20Video-Watch%20on%20YouTube-red?logo=youtube&logoColor=white)](https://youtu.be/aOyxKV9g2YE)

## 🌱 Our Mission
To empower farmers with:
- Access to green technology
- A platform for collaboration
- Tools to make farming eco-friendly and future-ready

## 🧭 1. Project Overview
Agri-Energy Connect is a web-based platform designed to help South African farmers connect with green energy solution providers, making agriculture more sustainable and innovative.

### 🔹 Who is it for?
- Farmers looking for clean energy solutions
- Employees managing agricultural data
- Green energy experts

### 🔹 What does it do?
- Farmers can add products (like crops) to their profiles
- Employees can add farmer details and view/filter their products
- Admins control the creation of employee and farmer roles

### 🔹 Design Pattern Used
MVC (Model-View-Controller) — organizes the application in a clear, maintainable way.

### 🔹 Architecture Chosen
Layered Architecture – separates the app into 4 main layers:
1. Database Layer
2. Data Access Layer (using Entity Framework Core)
3. Business Logic Layer
4. Presentation Layer (UI)

### 🔹 Database
SQL Server Management Studio (SSMS)  
✅ Chosen for its reliability, integration with .NET technologies, and user-friendly tools for database development.

## 🛠️ 2. Setup Instructions 

Follow these steps to set up Agri-Energy Connect **with the pre-configured database**:

### ✅ Step 1: Install Dependencies
Ensure these are installed:
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) (with **ASP.NET and .NET 8 SDK** workload)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)
- [GitHub](https://git-scm.com/downloads) (for cloning)

---

### ✅ Step 1: Clone the Project

You can use either of the two methods to clone the repository.

#### **Method 1: Using Visual Studio**

1. Open Visual Studio.
2. Under **Get Started**, select **Clone a repository**.
3. Paste this repository link:

   ```
   https://github.com/ST10252746/PROG7311_POE_PART_2.git
   ```
4. Choose your local folder location.
5. Click **Clone**.

#### **Method 2: Using Command Line**

1. Open your CLI or terminal.
2. Navigate to your desired folder.
3. Run:

   ```bash
   git clone https://github.com/ST10252746/PROG7311_POE_PART_2.git
   ```
4. Open the solution file `ST10252746_PROG7311_POE_PART_2.sln` in Visual Studio.
5. Restore NuGet packages and build the solution.

---

### ✅ Step 2: Restore the Database

You can restore the database using either a `.bak` backup file or a SQL script.

#### **Method 1: Using Backup File (`AgriEnergyConnectDB2.bak`)**

1. Open **SQL Server Management Studio (SSMS)**.
2. Connect to your SQL Server instance (e.g., `.\SQLEXPRESS` or `localhost`).
3. Right-click on **Databases** > **Restore Database...**.
4. Select **Device** > Click `...` > Add the `AgriEnergyConnectDB2.bak` file.
5. Click **OK** to restore.

#### **Method 2: Using the SQL Script**

1. In SSMS, connect to your server and click **New Query**.
2. Open the SQL script provided in the repo (e.g., `database script.sql`).
3. Execute the script to create and populate the database.
4. Confirm that the `AgriEnergyConnectDB2` database appears in your databases list.

---

### ✅ Step 3: Update the Connection String

1. Open the project in Visual Studio.
2. Navigate to the `appsettings.json` file.
3. Replace the existing connection string with your actual server name:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AgriEnergyConnectDB2;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
   }
   ```
4. For local setup, your server might be:

   * `.\SQLEXPRESS`
   * `localhost`
   * or your machine name (e.g., `DESKTOP-1234\SQLEXPRESS`)

---

### ✅ Step 4: Run the Application

1. In Visual Studio, press **F5** or click the green ▶️ **Start** button.
2. The application will run at:
   `https://localhost:7171` (port may vary)
   
---

### 🔍 Troubleshooting
- **Connection issues?** Verify:
  - SQL Server is running (check via SSMS)
  - Your connection string matches your server name
  - The database name is exactly `AgriEnergyConnectDB2`
- **Missing dependencies?** Ensure:
  - .NET 8 SDK is installed (`dotnet --list-sdks` in terminal)
  - NuGet packages are restored (right-click solution → "Restore NuGet Packages")

## 🧱 3. Architecture and Design Pattern
### 📐 Why MVC + Layered Architecture?
- ✅ MVC separates data, user interface, and logic – making the app easier to build and update
- ✅ Layered Architecture ensures each part (UI, logic, and data) has a clear job

### 🔍 Where it's implemented:
- **Controllers** (like EmployeeController, FarmerController) manage the app's flow
- **Views** show user interfaces (HTML pages)
- **Models** define the structure of data (e.g., Farmer, Product)
- **Services & Repositories** handle business logic and database access, following the layered pattern

## 🔐 4. Application Flow and User Roles
The application has three roles:

### Admin
- Creates Employee and Farmer roles
- Creates Employee accounts with login credentials
- Manage Employee profiles

### Employee
- Logs in with provided credentials
- Can add new farmers and manage their profiles
- Views and filters products added by farmers

### Farmer
- Logs in with credentials created by an Employee
- Can add products with details like:
  - Name
  - Description
  - Category
  - Production Date
  - Image
   
- Can view their own product list

## 🌐 5. Detailed Functional Features
### 🔧 Database Development (SSMS + EF Core)
- EF Core connects the web app to the SQL database
- Automatically updates the database structure using migrations

### 👤 User Authentication and Security
- Secure login for all users
- Password Hashing ensures passwords are encrypted and safe
- Access is role-specific (Farmer/Employee/Admin only see what they're allowed to)

### 🌾 Farmer Features
- Add new products with:
  - Name
  - Description
  - Category
  - Production Date
  - Image
    
- View their list of added products

### 🧑‍💼 Employee Features
- Create farmer profiles and accounts
- View and filter all farmer products using:
  - Date range
  - Product type
  - Specific farmer

### 👤 Admin
- Creates and assigns Employee and Farmer roles
- Adds new Employee accounts with login credentials
- Manages Employee access via the Admin Portal


