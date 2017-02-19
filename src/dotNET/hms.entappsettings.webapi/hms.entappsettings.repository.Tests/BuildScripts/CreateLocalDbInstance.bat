REM == CREATE LocalDb 
REM "C:\Program Files\Microsoft SQL Server\120\Tools\Binn\SqlLocalDB.exe" create "V12.0" 12.0 -s
"C:\Program Files\Microsoft SQL Server\120\Tools\Binn\SqlLocalDB.exe" INFO "V12.0"
REM --- 5 Sec delay
REM ping 127.0.0.1 -n 5 > nul
REM "C:\Program Files\Microsoft SQL Server\120\Tools\Binn\SqlLocalDB.exe" info "V12.0"
REM sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "EXEC sys.sp_configure N'contained database authentication', N'1';"
REM sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "RECONFIGURE WITH OVERRIDE;"
REM --- 5 sec delay
REM ping 127.0.0.1 -n 5 > nul
