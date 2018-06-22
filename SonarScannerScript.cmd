set opencover=C:\%homepath%\.nuget\packages\opencover\4.6.519\tools\OpenCover.Console.exe
set dotnet="C:\Program Files\dotnet\dotnet.exe"

dotnet-sonarscanner begin /k:"NetCore21" /n:"NetCore21" /v:"1.0" /d:sonar.cs.xunit.reportsPaths="SonarQubeResults\XUnitResultNetCore21Site.xml,SonarQubeResults\XUnitResultNetCore21Authentication.xml" /d:sonar.cs.opencover.reportsPaths="SonarQubeResults\OpenCover*.xml" /d:sonar.exclusions="**\wwwroot\**,**\*.js, **\*.ts, **\Program.cs, **\Startup.cs"

dotnet build NetCore21.sln /p:DebugType=Full

cd tests
REM GENERATION NetCore21.Site.Tests TEST REPORT
cd NetCore21.Site.Tests/
%opencover% -target:%dotnet% -targetargs:"test \"NetCore21.Site.Tests.csproj\" --configuration Debug --no-build" -filter:"+[*NetCore21*]* -[*.Test*]*" -oldStyle -register:user -output:"../../SonarQubeResults/OpenCoverNetCore21Site.xml"
dotnet xunit --fx-version 2.1.0 -xml ../../SonarQubeResults/XUnitResultNetCore21Site.xml
cd ..

REM GENERATION NetCore21.Authentication.Tests TEST REPORT
cd NetCore21.Authentication.Tests/
%opencover% -target:%dotnet% -targetargs:"test \"NetCore21.Authentication.Tests.csproj\" --configuration Debug --no-build" -filter:"+[*NetCore21*]* -[*.Test*]*" -oldStyle -register:user -output:"../../SonarQubeResults/OpenCoverNetCore21Authentication.xml"
dotnet xunit --fx-version 2.1.0 -xml ../../SonarQubeResults/XUnitResultNetCore21Authentication.xml

cd ../../


dotnet-sonarscanner end

taskkill /F /IM dotnet.exe
