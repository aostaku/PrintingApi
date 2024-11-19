pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        git(url: 'https://github.com/aostaku/PrintingApi', branch: 'master')
      }
    }

    stage('Docker build') {
      steps {
        sh '''sh \'docker-compose down\'
sh "docker-compose up -d --build"'''
      }
    }

  }
}
