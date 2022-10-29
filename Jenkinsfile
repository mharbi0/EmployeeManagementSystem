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

  }
}
