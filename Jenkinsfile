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
        sh 'dotnet restore .//Ebttikar.sln'
        sh 'dotnet build .//Ebttikar.sln'
      }
    }

  }
}
