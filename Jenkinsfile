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
        dotnetRestore(project: './Ebttikar.sln')
        dotnetBuild(project: './Ebttikar.sln', configuration: 'Release')
      }
    }

    stage('Publish') {
      steps {
        dotnetPublish(project: './Ebttikar.sln', configuration: 'Release', selfContained: true, options: ['--os "win"', ' --arch "x64"'])
      }
    }
    stage('Generate Zip') {
      steps {
        zip(zipFile: './EmployeeManagementSystem.zip' dir: './EbttikarWeb/bin/Release/net6.0/win-x64/publish' overwrite: true archive: true)
        // zip archive: true, dir: '/pathToDirInWorkspace', glob: '', zipFile: 'nameOfFile'
    }

  }
}
