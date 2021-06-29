# Getting Started 

There are two project in this repository

## Web Admin

Step by step to running this project:
1. Change connection string in appsettings.json
2. Database migration, open package manager console and run the following command

    - Create a migration with the name "Initial"

        `Add-Migration Initial -Project SimpleSales.WebAdmin`

    - Run migration

        `Update-Database -Migration Initial -Project SimpleSales.WebAdmin`

3. Run the project, and login with email `maseric7@gmail.com` & password `Petrokimia@2021`

There are 3 main menu: 
- Product (Stock Items)
- User (Admin)
- Report (Table & Chart)

## API

Step by step to running this project:
1. Change connection string in appsettings.json
2. Run the project, and will be redirected to the API documentation page (using swagger)
3. API authorization, enter an API key `WcVbUa1kNG`
