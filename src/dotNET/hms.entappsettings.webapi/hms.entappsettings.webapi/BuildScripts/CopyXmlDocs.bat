@ECHO OFF
REM === Post Build Script to bring XmlDocument.xmls together
IF "%~1"=="" GOTO noparampassed
set projectDir=%~1
ECHO ON
ECHO Project Path = %projectDir%
REM === Add additional xml docs below here ===
copy "D:\VSOGit\HMS KwizWar\src\dotNET\hms.entappsettings.webapi\hms.entappsettings.contracts\hms.entappsettings.contracts.xml" "%projectDir%\App_Data\hms.entappsettings.contracts.xml"
@ECHO OFF
EXIT /B
:noparampassed
ECHO Error *** The project path was not passed to this batch file. Ending!! ***
EXIT /B 1