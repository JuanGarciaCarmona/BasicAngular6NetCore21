set opencover=C:\%homepath%\.nuget\packages\opencover\4.6.519\tools\OpenCover.Console.exe
set dotnet="C:\Program Files\dotnet\dotnet.exe"

dotnet-sonarscanner begin /k:"NetCore21" /n:"NetCore21" /v:"1.0" /d:sonar.cs.xunit.reportsPaths="SonarQubeResults\XUnitResultNetCore21.xml" /d:sonar.cs.opencover.reportsPaths="SonarQubeResults\OpenCover*.xml" /d:sonar.exclusions="**\wwwroot\**,**\*.js, **\Program.cs, **\Startup.cs"

dotnet build NetCore21.sln /p:DebugType=Full

cd tests
REM GENERATION Com.Solera.Vin.Crosscutting.Tests TEST REPORT
cd NetCore21.Site.Tests/
%opencover% -target:%dotnet% -targetargs:"test \"NetCore21.Site.Tests.csproj\" --configuration Debug --no-build" -filter:"+[*NetCore21*]* -[*.Test*]*" -oldStyle -register:user -output:"../../SonarQubeResults/OpenCoverNetCore21.xml"
dotnet xunit --fx-version 2.0.0 -xml BasicAngular6NetCore21/SonarQubeResults/XUnitResultNetCore21.xml
cd ../../


dotnet-sonarscanner end