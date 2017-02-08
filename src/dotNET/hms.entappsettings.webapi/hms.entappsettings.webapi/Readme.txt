=========================================
Enterprise App Settings Readme.txt
=========================================
Last updated 2017-02-08

For more information please go to Github and the Wiki: https://github.com/nrogoff/EnterpriseAppSettings

The database connections strings in the web.config and Test project app.configs point to 
Integration Testing SQL Server in Azure kindly made available by Hard Medium Soft Ltd.

If you are going to contribute to this project and would like to debug and test on your 
own databases, please just change the connection strings. However as we have continuous integration
tests running, please do not include these changes in any Pull Requests.

Note: The Integration test database user must have db_owner role on the database. 
This is to allow the cleanup at the end and reset the identity columns.

Also note that the test projects require that the database be completely empty. This is
to ensure there is no way you can run this against a production database.

On GitHub you can find the clear out database script to help with this.

Thanks

