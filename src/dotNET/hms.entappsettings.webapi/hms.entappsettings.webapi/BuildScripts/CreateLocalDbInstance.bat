REM == CREATE LocalDb 
"C:\Program Files\Microsoft SQL Server\120\Tools\Binn\SqlLocalDB.exe" create "V12.0" 12.0 -s
sqlcmd -S "(localdb)\V12.0" -Q "EXEC sys.sp_configure N'contained database authentication', N'1';"
sqlcmd -S "(localdb)\V12.0" -Q "RECONFIGURE WITH OVERRIDE;"
