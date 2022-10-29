pipeline {
  agent any
  stages {
    stage('Checkout Code') {
      steps {
        git(url: 'https://github.com/mharbi0/EmployeeManagementSystem.git', branch: 'main')
      }
    }

    stage('Build') {
      steps {
        bat 'dotnet restore .//Ebttikar.sln'
        bat 'dotnet build .//Ebttikar.sln'
      }
    }

    stage('Publish') {
      steps {
        bat 'dotnet publish .//Ebttikar.sln --configuration Release --self-contained  --os "win" --arch "x64"'
      }
    }
    stage('Generate Zip') {
      steps {
        // zip(zipFile: './EmployeeManagementSystem.zip', dir: './EbttikarWeb/bin/Release/net6.0/win-x64/publish', overwrite: true, archive: true)
        zip{
          zipFile './EmployeeManagementSystem.zip'
          dir './EbttikarWeb/bin/Release/net6.0/win-x64/publish'
          overwrite true 
          archive true
        }
      }
    }

  }
}
