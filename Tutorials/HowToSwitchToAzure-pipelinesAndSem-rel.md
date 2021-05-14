# How to switch to azure-pipelines and sem-rel from appveyor

## Open project forlder

1. Delete the following files
    - build.cmd
    - build-configuration.cmd
    - appveyor
    - gitversion

2. Add the following files
    - ci folder with azure-pipelines.yml inside - https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema?view=azure-devops&tabs=schema%2Cparameter-schema
    - CHANGELOG.md empty file
    - package.json - https://docs.npmjs.com/about-semantic-versioning
    - release.config.js  - https://semantic-release.gitbook.io/semantic-release/usage/configuration

3. I put a tag on the latest version of the brunch and push to origin - example v1.0.0

## Open Azure DevOps

1. On Pipelines tab select New pipeline

2. Select GitHub

3. Select the repository of project

4. Select Existing Azure Pipelines YAML file option

5. Choose the branch that contains azure-pipelines.yml file and path to file

6. Then select dropdown next to Run and click Save

7. Select Pipelines tab and then select All pipelines

8. Find your pipeline and click on it

9. Click on Run pipeline

10. Select branch that you want to run and click Run (First run must be manual)

### Done!