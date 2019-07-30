#!/bin/sh

echo 'from environment ' $env_database_name $env_login_user $env_login_name $env_login_password

# wait for the SQL Server to come up

echo 'sleeping'
sleep 20

# run script

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i script.sql
# -v database_name="$database_name" -v user_name="$login_user"  -v login_name="$login_name" -v login_password="$login_password"
